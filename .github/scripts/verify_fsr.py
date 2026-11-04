#!/usr/bin/env python3
"""Verify committed bindings against Clang layouts and inspect the actual NuGet payload.

This check is CPU-only. It does not load the Windows DLLs or claim GPU validation.
"""
from __future__ import annotations

import argparse
import json
from pathlib import Path
import re
import subprocess
from tempfile import TemporaryDirectory
import zipfile
from xml.etree import ElementTree
from xml.sax.saxutils import quoteattr

from build_fsr import DLLS, pe_info
from parse_fsr_ast import EXPORTS


def normalize(name: str) -> str:
    return re.sub(r"[^a-z0-9]", "", name.lower())


def type_key(native: str) -> str:
    if native == "Anonymous_ffx_radiancecache_95_5":
        return normalize("DispatchDescRadianceCacheOverrides")
    return normalize(re.sub(r"^(PfnFfx|PFN_|ffx)", "", native, flags=re.IGNORECASE))


def field_key(native: str) -> str:
    name = re.sub(r"^[pP]+(?=[A-Z_])_?", "", native)
    name = re.sub(r"^(?:In|in|Out|out)(?=[A-Z_]|$)_?", "", name)
    return normalize(name)


def calling_conventions(native: dict) -> list[str]:
    if native["kind"] == "POINTER" and native["element"]["kind"] in ("FUNCTIONPROTO", "FUNCTIONNOPROTO"):
        function = native["element"]
        result = [function["callingConvention"]]
        for argument in function["arguments"]:
            result.extend(calling_conventions(argument))
        result.extend(calling_conventions(function["result"]))
        return result
    return calling_conventions(native["element"]) if "element" in native else []


def verify_calling_conventions(root: Path) -> None:
    platform = json.loads((root / "FSR.NET.Generator/ast.json").read_text(encoding="utf-8"))["platforms"]["win-x64"]
    sources = {}
    for path in (root / "FSR.NET/Structs").glob("*.g.cs"):
        key = normalize(path.name[3:-5])
        if key in sources:
            raise RuntimeError(f"Ambiguous managed record name: {path.stem}")
        sources[key] = path.read_text(encoding="utf-8-sig")

    count = 0
    for record in platform["records"]:
        if record["opaque"]:
            continue
        native = record["name"]
        source = sources[type_key(native)]
        declarations = {}
        for declaration, name in re.findall(r"^    public ([^\r\n;]+) (\w+);$", source, re.MULTILINE):
            key = normalize(name)
            if key in declarations:
                raise RuntimeError(f"Ambiguous managed field name: {native}.{name}")
            declarations[key] = declaration
        for field in record["fields"]:
            expected = calling_conventions(field["type"])
            if not expected:
                continue
            name = field["name"]
            declaration = declarations.get(field_key(name))
            if declaration is None:
                raise RuntimeError(f"Missing function pointer field {native}.{name}")
            actual = re.findall(r"unmanaged\[(Cdecl|Stdcall)\]", declaration)
            if actual != expected:
                raise RuntimeError(f"Function pointer convention mismatch {native}.{name}: {actual} != {expected}")
            count += len(expected)
    print(f"Verified {count} function pointer calling conventions, including nested signatures.")


def verify_native_assets(root: Path) -> None:
    native = root / "native/win-x64"
    ast = json.loads((root / "FSR.NET.Generator/ast.json").read_text(encoding="utf-8"))
    if {path.name for path in native.glob("*.dll")} != set(DLLS):
        raise RuntimeError("Native directory does not contain the expected official DLLs.")
    for name in DLLS:
        info = pe_info(native / name)
        if info["sha256"] != ast["source"]["files"]["Kits/FidelityFX/signedbin/" + name]:
            raise RuntimeError("Native asset differs from its SDK source checksum: " + name)
        if set(info["exports"]) != set(EXPORTS):
            raise RuntimeError("Official DLL exports differ from the public API: " + name)
    print(f"Verified {len(DLLS)} official Windows x64 DLLs against the pinned SDK checksums and exports.")


def verify_package(root: Path, package: Path) -> None:
    verify_native_assets(root)
    properties = ElementTree.parse(root / "Directory.Build.props")
    framework = properties.findtext("PropertyGroup/StandardTargetFramework")
    if not framework:
        raise RuntimeError("Directory.Build.props must specify StandardTargetFramework.")
    with zipfile.ZipFile(package) as archive:
        names = set(archive.namelist())
        expected = {f"runtimes/win-x64/native/{name}" for name in DLLS}
        actual = {name for name in names if name.startswith("runtimes/")}
        if actual != expected:
            raise RuntimeError(f"NuGet native payload mismatch: {actual ^ expected}")
        for name in expected:
            if archive.read(name) != (root / "native/win-x64" / Path(name).name).read_bytes():
                raise RuntimeError(f"NuGet changed the official DLL: {name}")
        assembly_path = f"lib/{framework}/FSR.NET.dll"
        for name in ("LICENSE", "README.md", assembly_path):
            if name not in names:
                raise RuntimeError(f"NuGet missing {name}")
        assembly = archive.read(assembly_path)
    verify_bindings(root, assembly)
    print(f"Verified NuGet package: {package.name}; managed bindings and {len(DLLS)} byte-identical official DLLs.")


def verify_bindings(root: Path, assembly: bytes | None = None) -> None:
    source = Path(__file__).with_name("VerifyBindings.cs").resolve()
    library = root / "FSR.NET/FSR.NET.csproj"
    reference = (f'<ProjectReference Include={quoteattr(str(library))} />' if assembly is None else
                 '<Reference Include="FSR.NET" HintPath="FSR.NET.dll" />')
    project = f'''<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>$(StandardTargetFramework)</TargetFramework>
        <RootNamespace>FSR.NET.Validation</RootNamespace>
        <IsPackable>false</IsPackable>
        <EnableDefaultCompileItems>false</EnableDefaultCompileItems>
    </PropertyGroup>
    <ItemGroup>
        <Compile Include={quoteattr(str(source))} Link="VerifyBindings.cs" />
        {reference}
    </ItemGroup>
</Project>
'''
    # Keep the transient project under the repository so shared props and analyzers apply.
    with TemporaryDirectory(prefix=".verify-fsr-", suffix=".tmp", dir=root) as directory:
        workspace = Path(directory)
        if assembly is not None:
            (workspace / "FSR.NET.dll").write_bytes(assembly)
        (workspace / "Verify.csproj").write_text(project, encoding="utf-8")
        subprocess.run([
            "dotnet", "run", "--project", str(workspace / "Verify.csproj"), "-c", "Release",
            "--property:TreatWarningsAsErrors=true", "--", str(root / "FSR.NET.Generator/ast.json")
        ], check=True)
        subprocess.run([
            "dotnet", "format", str(workspace / "Verify.csproj"), "--no-restore", "--verify-no-changes",
            "--include", str(source), "--severity", "warn"
        ], check=True)


def main() -> None:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", type=Path, default=Path(__file__).resolve().parents[2])
    parser.add_argument("--package", type=Path)
    args = parser.parse_args()
    root = args.root.resolve()
    subprocess.run(["dotnet", "run", "--project", str(root / "FSR.NET.Generator"), "-c", "Release", "--", str(root), "--verify"], check=True)
    verify_calling_conventions(root)
    if args.package:
        verify_package(root, args.package.resolve())
    else:
        verify_bindings(root)


if __name__ == "__main__":
    main()
