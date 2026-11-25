"""Prepare one SDK, validate FSR loaders, merge targets, and verify release artifacts."""

from __future__ import annotations

import argparse
from concurrent.futures import ThreadPoolExecutor
import hashlib
import json
import os
from pathlib import Path
import re
import shutil
import struct
import subprocess
import sys
import tarfile
import urllib.request
from xml.etree import ElementTree
import zipfile

from parse_ast import COMPONENTS, EXPORTS, extract, public_headers

REPOSITORY = "GPUOpen-LibrariesAndSDKs/FidelityFX-SDK"
VULKAN_REPOSITORY = "KhronosGroup/Vulkan-Headers"
PACKAGE = "FSR.NET"
PREFIX = "Fsr"

PLATFORMS = ("win-x64",)

LLVM_VERSION = "21.1.7"
LLVM_ARCHIVES = {
    "windows": ("clang+llvm-21.1.7-x86_64-pc-windows-msvc.tar.xz", "70a2b73f2f14f787557f90abf380e7170b54e97b893218999144de5284b4f8f8"),
}

LOADER_DLLS = sorted("amd_fidelityfx_" + name + ".dll" for name in ("loader_dx12", "loader_vk"))
RUNTIME_DLLS = sorted("amd_fidelityfx_" + name + ".dll" for name in (
    "upscaler_dx12", "upscaler_vk", "framegeneration_dx12", "framegeneration_vk",
    "denoiser_dx12", "radiancecache_dx12"))


def file_sha256(path: Path) -> str:
    digest = hashlib.sha256()
    with path.open("rb") as stream:
        while True:
            chunk = stream.read(1024 * 1024)
            if not chunk:
                break
            digest.update(chunk)
    return digest.hexdigest()


def pe_info(path: Path) -> dict:
    """Read architecture and actual named exports without loading Windows code."""
    data = path.read_bytes()
    if data[:2] != b"MZ":
        raise RuntimeError("Not a PE file: " + str(path))
    pe = struct.unpack_from("<I", data, 0x3C)[0]
    if data[pe:pe + 4] != b"PE\0\0":
        raise RuntimeError("Invalid PE header: " + str(path))
    machine, sections = struct.unpack_from("<HH", data, pe + 4)
    opt_size = struct.unpack_from("<H", data, pe + 20)[0]
    opt = pe + 24
    magic = struct.unpack_from("<H", data, opt)[0]
    if machine != 0x8664 or magic != 0x20B:
        raise RuntimeError("Expected ordinary Windows x64 PE32+: " + str(path))
    section_start = opt + opt_size

    def offset(rva: int) -> int:
        for i in range(sections):
            _, vsize, va, rawsize, raw = struct.unpack_from("<8sIIII", data, section_start + i * 40)
            if va <= rva < va + max(vsize, rawsize):
                return raw + rva - va
        raise RuntimeError("Invalid PE RVA in " + str(path))

    export_rva = struct.unpack_from("<I", data, opt + 112)[0]
    exports = []
    if export_rva:
        export = offset(export_rva)
        names = struct.unpack_from("<I", data, export + 24)[0]
        name_table = offset(struct.unpack_from("<I", data, export + 32)[0])
        for i in range(names):
            n = offset(struct.unpack_from("<I", data, name_table + i * 4)[0])
            exports.append(data[n:data.index(0, n)].decode("ascii"))
    return {"machine": "AMD64", "machineValue": machine, "sha256": file_sha256(path), "size": len(data), "exports": sorted(exports)}


def request(url: str) -> bytes:
    headers = {"User-Agent": PACKAGE}
    if url.startswith("https://api.github.com/") and os.environ.get("GH_TOKEN"):
        headers["Authorization"] = "Bearer " + os.environ["GH_TOKEN"]
    with urllib.request.urlopen(urllib.request.Request(url, headers=headers), timeout=180) as response:
        return response.read()


def api(repository: str, path: str) -> dict:
    return json.loads(request(f"https://api.github.com/repos/{repository}/{path}"))


def write_text(path: Path, value: str, encoding: str = "utf-8") -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    with path.open("w", encoding=encoding, newline="\n") as stream:
        stream.write(value)


def save(path: Path, data: dict) -> None:
    write_text(path, json.dumps(data, indent=2, ensure_ascii=False) + "\n")


def download(repository: str, commit: str, paths: list[str], root: Path) -> None:
    def copy(path: str) -> None:
        destination = root / path
        destination.parent.mkdir(parents=True, exist_ok=True)
        destination.write_bytes(request(f"https://raw.githubusercontent.com/{repository}/{commit}/{path}"))

    with ThreadPoolExecutor(max_workers=8) as pool:
        list(pool.map(copy, paths))


def verify_runtimes(source: dict) -> None:
    runtimes = source.get("runtimes")
    if not isinstance(runtimes, dict) or set(runtimes) != set(PLATFORMS):
        raise RuntimeError(f"SDK runtimes manifest must target {sorted(PLATFORMS)}.")
    expected = {"Kits/FidelityFX/signedbin/" + name for name in RUNTIME_DLLS}
    actual = runtimes["win-x64"]
    if not isinstance(actual, dict) or set(actual) != expected:
        raise RuntimeError("SDK runtimes manifest does not match the expected runtime DLLs.")
    for path, digest in actual.items():
        if not isinstance(digest, str) or not re.fullmatch(r"[0-9a-f]{64}", digest):
            raise RuntimeError(f"Invalid runtime SHA-256 digest for win-x64/{path}.")


def prepare(root: Path) -> None:
    release = api(REPOSITORY, "releases/latest")["tag_name"]
    commit = api(REPOSITORY, "commits/" + release)["sha"]
    tree = api(REPOSITORY, f"git/trees/{commit}?recursive=1")
    if tree.get("truncated"):
        raise RuntimeError("SDK tree response is truncated.")
    paths = [item["path"] for item in tree["tree"] if item["type"] == "blob"]
    found_components = {match[1] for path in paths if (match := re.match(r"Kits/FidelityFX/([^/]+)/include/.*\.(?:h|hpp)$", path))}
    if found_components != set(COMPONENTS):
        raise RuntimeError(f"Unexpected SDK component set: {sorted(found_components)}")

    prefixes = tuple(f"Kits/FidelityFX/{c}/include/" for c in COMPONENTS)
    headers = [p for p in paths if p.startswith(prefixes) and p.endswith((".h", ".hpp"))]
    loaders = ["Kits/FidelityFX/signedbin/" + name for name in LOADER_DLLS]
    runtime_paths = ["Kits/FidelityFX/signedbin/" + name for name in RUNTIME_DLLS]
    download(REPOSITORY, commit, headers + loaders + runtime_paths, root)

    for name in LOADER_DLLS:
        info = pe_info(root / "Kits/FidelityFX/signedbin" / name)
        if set(info["exports"]) != set(EXPORTS):
            raise RuntimeError("Official loader DLL exports differ from the public API: " + name)

    runtimes = {"win-x64": {}}
    for name in RUNTIME_DLLS:
        relative = "Kits/FidelityFX/signedbin/" + name
        path = root / relative
        info = pe_info(path)
        if set(info["exports"]) != set(EXPORTS):
            raise RuntimeError("Official runtime DLL exports differ from the public API: " + name)
        runtimes["win-x64"][relative] = info["sha256"]
        path.unlink()

    vulkan_commit = api(VULKAN_REPOSITORY, "tags?per_page=1")[0]["commit"]["sha"]
    vulkan_tree = api(VULKAN_REPOSITORY, f"git/trees/{vulkan_commit}?recursive=1")
    if vulkan_tree.get("truncated"):
        raise RuntimeError("Vulkan headers tree response is truncated.")
    vulkan = [item["path"] for item in vulkan_tree["tree"] if item["type"] == "blob" and item["path"].startswith("include/")]
    download(VULKAN_REPOSITORY, vulkan_commit, vulkan, root / "vulkan")
    save(
        root / "source.json",
        {
            "repository": f"https://github.com/{REPOSITORY}",
            "release": release,
            "commit": commit,
            "vulkanCommit": vulkan_commit,
            "runtimes": runtimes,
        },
    )
    print(f"Using {release} at {commit}", flush=True)


def install_llvm(scratch: Path) -> Path:
    name, checksum = LLVM_ARCHIVES["windows"]
    archive = scratch / name
    print("Using " + name, flush=True)
    url = f"https://github.com/llvm/llvm-project/releases/download/llvmorg-{LLVM_VERSION}/{name}"
    with urllib.request.urlopen(url, timeout=180) as response, archive.open("wb") as output:
        shutil.copyfileobj(response, output)
    if file_sha256(archive) != checksum:
        raise RuntimeError("LLVM archive checksum does not match: " + name)
    destination = scratch / "llvm"
    with tarfile.open(archive) as package:
        if sys.version_info >= (3, 12):
            package.extractall(destination, filter="data")
        else:
            package.extractall(destination)
    archive.unlink()
    return next(destination.glob("*/bin/clang.exe")).parents[1]


def check_msvc_layouts(root: Path, scratch: Path, ast: dict) -> None:
    """Independently confirm Clang layouts with the runner's production compiler."""
    if os.name != "nt":
        return
    for backend in ("dx12", "vk"):
        other = "/vk/" if backend == "dx12" else "/dx12/"
        headers = [p for p in public_headers(root) if other not in p.as_posix()]
        lines = ["#include <stddef.h>", "#include <stdint.h>", "#include <string.h>"]
        lines += ['#include "' + p.as_posix() + '"' for p in headers]
        for record in ast["records"]:
            if record["external"] or record["opaque"] or record["name"].startswith("Anonymous_") or other in record["header"]:
                continue
            name = record["name"]
            lines += [
                f'static_assert(sizeof({name}) == {record["size"]}, "{name} size");',
                f'static_assert(alignof({name}) == {record["alignment"]}, "{name} alignment");',
            ]
            for field in record["fields"]:
                lines += [
                    f'static_assert(offsetof({name}, {field["name"]}) == {field["offset"] // 8}, "{name}.{field["name"]} offset");',
                    f'static_assert(sizeof((({name}*)0)->{field["name"]}) == {field["type"]["size"]}, "{name}.{field["name"]} size");',
                ]
        path = scratch / ("abi_" + backend + ".cpp")
        write_text(path, "\n".join(lines) + "\n")
        subprocess.run(["cl", "/nologo", "/c", "/std:c++20", "/DNOMINMAX", "/DWIN32_LEAN_AND_MEAN", "/D_WINDOWS", "/I" + str(root / "vulkan/include"), str(path), "/Fo" + str(scratch / ("abi_" + backend + ".obj"))], check=True)


def build(root: Path, scratch: Path, output: Path, rid: str) -> None:
    if rid != "win-x64":
        raise RuntimeError("Unsupported target RID: " + rid)
    root, scratch, output = root.resolve(), scratch.resolve(), output.resolve()
    scratch.mkdir(parents=True, exist_ok=True)
    output.mkdir(parents=True, exist_ok=True)
    source = json.loads((root / "source.json").read_text(encoding="utf-8"))
    verify_runtimes(source)
    llvm = install_llvm(scratch)
    ast = extract(root, scratch, llvm)
    check_msvc_layouts(root, scratch, ast)
    native = output / "native" / rid
    native.mkdir(parents=True, exist_ok=True)
    natives = {}
    exports = {}
    for name in LOADER_DLLS:
        path = root / "Kits/FidelityFX/signedbin" / name
        info = pe_info(path)
        if set(info["exports"]) != set(EXPORTS):
            raise RuntimeError("Official DLL exports differ from the public API: " + name)
        natives[name] = info["sha256"]
        exports[name] = info["exports"]
        shutil.copy2(path, native / name)
    save(output / "ast.json", {"source": source, "rid": rid, "platform": ast, "natives": natives, "exports": exports})
    print(f"Extracted {len(ast['records'])} records, {len(ast['enums'])} enums, {len(ast['functions'])} exports; validated {len(natives)} official loader DLLs.", flush=True)


def merge(root: Path, output: Path) -> None:
    artifacts = {}
    platforms = {}
    natives = {}
    source = None
    for rid in PLATFORMS:
        artifact = root / f"target-{rid}"
        if not artifact.is_dir() and len(PLATFORMS) == 1:
            artifact = root
        artifacts[rid] = artifact
        fragment = json.loads((artifact / "ast.json").read_text(encoding="utf-8"))
        if fragment["rid"] != rid:
            raise RuntimeError("Target artifact RID does not match: " + rid)

        if source is None:
            source = fragment["source"]
        elif fragment["source"] != source:
            raise RuntimeError("SDK sources differ between target artifacts: " + rid)
        verify_runtimes(source)

        if set(fragment["natives"]) != set(LOADER_DLLS) or set(fragment["exports"]) != set(LOADER_DLLS):
            raise RuntimeError("Target artifact does not contain the expected official loader DLLs.")
        for name in LOADER_DLLS:
            info = pe_info(artifact / "native" / rid / name)
            if info["sha256"] != fragment["natives"][name] or info["exports"] != fragment["exports"][name]:
                raise RuntimeError("Native asset differs from its export check: " + name)
            if set(info["exports"]) != set(EXPORTS):
                raise RuntimeError("Official DLL exports differ from the public API: " + name)

        natives[rid] = fragment["natives"]
        platforms[rid] = fragment["platform"]

    packaging = output / "NuGet.Packaging.props"
    metadata = packaging.read_text(encoding="utf-8-sig")
    version = source["release"].removeprefix("v")
    metadata, replacements = re.subn(r"(?<=<Version>)[^<]*(?=</Version>)", lambda _: version, metadata)
    if replacements != 1:
        raise RuntimeError("NuGet.Packaging.props must contain one package Version.")

    destination = output / "native"
    if destination.exists():
        shutil.rmtree(destination)
    for rid, artifact in artifacts.items():
        shutil.copytree(artifact / "native" / rid, destination / rid)

    merged_source = {
        "repository": source["repository"],
        "release": source["release"],
        "commit": source["commit"],
        "vulkanCommit": source["vulkanCommit"],
        "natives": natives,
        "runtimes": source["runtimes"],
    }
    save(output / f"{PACKAGE}.Generator" / "ast.json", {"source": merged_source, "platforms": platforms})
    write_text(packaging, metadata, encoding="utf-8-sig")


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


def verify_calling_conventions(root: Path, ast: dict) -> None:
    platform = ast["platforms"]["win-x64"]
    sources = {}
    for path in (root / PACKAGE / "Structs").glob("*.g.cs"):
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


def verify_natives(root: Path, source: dict) -> None:
    natives = source.get("natives")
    if not isinstance(natives, dict) or set(natives) != set(PLATFORMS):
        raise RuntimeError(f"ast.json source.natives must target {sorted(PLATFORMS)}.")
    native_root = root / "native"
    actual_rids = {p.name for p in native_root.iterdir() if p.is_dir()} if native_root.is_dir() else set()
    if actual_rids != set(natives):
        raise RuntimeError(f"Native RID directories mismatch: {actual_rids ^ set(natives)}")
    for rid, expected_files in natives.items():
        rid_dir = native_root / rid
        actual_files = {p.name for p in rid_dir.iterdir() if p.is_file()}
        if actual_files != set(expected_files) or actual_files != set(LOADER_DLLS):
            raise RuntimeError(f"Native files mismatch for {rid}: {actual_files ^ set(LOADER_DLLS)}")
        for name, expected_sha in expected_files.items():
            info = pe_info(rid_dir / name)
            if info["sha256"] != expected_sha:
                raise RuntimeError(f"Native checksum mismatch for {rid}/{name}.")
            if set(info["exports"]) != set(EXPORTS):
                raise RuntimeError(f"Official DLL exports differ from the public API: {name}")
    print(f"Verified {sum(len(files) for files in natives.values())} core native binaries across {len(natives)} RID(s).")


def verify_manifest(root: Path, source: dict) -> None:
    verify_runtimes(source)
    props_path = root / PACKAGE / "buildTransitive" / f"{PACKAGE}.props"
    targets_path = root / PACKAGE / "buildTransitive" / f"{PACKAGE}.targets"
    props = ElementTree.parse(props_path).getroot()
    release = props.findtext(f"PropertyGroup/_{PREFIX}SdkRelease")
    commit = props.findtext(f"PropertyGroup/_{PREFIX}SdkCommit")
    base_url = props.findtext(f"PropertyGroup/_{PREFIX}DefaultBaseUrl")
    repo_path = source["repository"].removeprefix("https://github.com/").rstrip("/")
    expected_base_url = f"https://raw.githubusercontent.com/{repo_path}/{source['commit']}"
    if release != source["release"] or commit != source["commit"] or base_url != expected_base_url:
        raise RuntimeError(f"buildTransitive/{PACKAGE}.props metadata does not match ast.json source.")

    actual_entries = {
        (element.attrib["Rid"], element.attrib["AssetName"], element.attrib["RelativeUrl"], element.attrib["Sha256"])
        for element in props.findall(f"ItemGroup/_{PREFIX}ManifestEntry")
    }
    expected_entries = {
        (rid, Path(relative_url).name, relative_url, digest.upper())
        for rid, assets in source["runtimes"].items()
        for relative_url, digest in assets.items()
    }
    if actual_entries != expected_entries:
        raise RuntimeError(f"buildTransitive/{PACKAGE}.props runtime entries do not match ast.json.")

    runtime_item = f"{PREFIX.upper()}Runtime"
    if props.find(f"ItemDefinitionGroup/{runtime_item}/Path") is None:
        raise RuntimeError(f"buildTransitive/{PACKAGE}.props missing ItemDefinitionGroup/{runtime_item}/Path.")

    ElementTree.parse(targets_path)
    targets_text = targets_path.read_text(encoding="utf-8-sig")
    for token in (runtime_item, f"{PREFIX}ResolveRuntime", f"{PACKAGE}.props", "System.Net.Http", "GetTempPath", "ExcludeFromSingleFile"):
        if token not in targets_text:
            raise RuntimeError(f"buildTransitive/{PACKAGE}.targets missing expected symbol: {token}")
    for forbidden in ("<Reference", "LocalApplicationData", "DownloadRuntime"):
        if forbidden in targets_text:
            raise RuntimeError(f"buildTransitive/{PACKAGE}.targets contains forbidden token: {forbidden}")
    print(f"Verified buildTransitive runtime manifest ({len(expected_entries)} entries) and targets.")


def verify_package(root: Path, source: dict, package: Path | None) -> None:
    framework = ElementTree.parse(root / "Directory.Build.props").findtext("PropertyGroup/StandardTargetFramework")
    if not framework:
        raise RuntimeError("Directory.Build.props must specify StandardTargetFramework.")
    packaging = ElementTree.fromstring((root / "NuGet.Packaging.props").read_text(encoding="utf-8-sig"))
    version = packaging.findtext("PropertyGroup/Version")
    if not version:
        raise RuntimeError("NuGet.Packaging.props must specify Version.")

    nupkg = package.resolve() if package else root / PACKAGE / "bin/Release" / f"{PACKAGE}.{version}.nupkg"
    if not nupkg.is_file():
        raise RuntimeError(f"NuGet package not found: {nupkg}")
    snupkg = nupkg.with_suffix(".snupkg")
    if snupkg.exists():
        raise RuntimeError(f"Unexpected symbol package: {snupkg}")

    with zipfile.ZipFile(nupkg) as archive:
        names = set(archive.namelist())
        expected_native = {
            f"runtimes/{rid}/native/{name}"
            for rid, files in source["natives"].items()
            for name in files
        }
        actual_native = {name for name in names if name.startswith("runtimes/")}
        if actual_native != expected_native:
            raise RuntimeError(f"NuGet native payload mismatch: {actual_native ^ expected_native}")
        for entry in expected_native:
            parts = entry.split("/")
            if archive.read(entry) != (root / "native" / parts[1] / parts[3]).read_bytes():
                raise RuntimeError(f"NuGet changed native binary: {entry}")

        for build_file in (f"buildTransitive/{PACKAGE}.props", f"buildTransitive/{PACKAGE}.targets"):
            if build_file not in names:
                raise RuntimeError(f"NuGet missing {build_file}")
            if archive.read(build_file) != (root / PACKAGE / build_file).read_bytes():
                raise RuntimeError(f"NuGet changed {build_file}")

        assembly_path = f"lib/{framework}/{PACKAGE}.dll"
        nuspec_name = f"{PACKAGE}.nuspec"
        for required in ("LICENSE", "README.md", assembly_path, nuspec_name):
            if required not in names:
                raise RuntimeError(f"NuGet missing {required}")

        nuspec = ElementTree.fromstring(archive.read(nuspec_name).decode("utf-8"))
        ns = {"n": "http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd"}
        pkg_version = nuspec.findtext("n:metadata/n:version", namespaces=ns)
        if pkg_version != version:
            raise RuntimeError(f"Nuspec version mismatch: {pkg_version} != {version}")

    print(f"Verified NuGet package: {nupkg.name} ({len(expected_native)} core native files, 0 runtime files).")


def verify(root: Path, package: Path | None = None) -> None:
    root = root.resolve()
    subprocess.run(["dotnet", "run", "--project", str(root / f"{PACKAGE}.Generator"), "-c", "Release", "--", str(root), "--verify"], check=True)
    ast = json.loads((root / f"{PACKAGE}.Generator" / "ast.json").read_text(encoding="utf-8"))
    source = ast["source"]
    verify_calling_conventions(root, ast)
    verify_natives(root, source)
    verify_manifest(root, source)
    verify_package(root, source, package)


def main() -> None:
    parser = argparse.ArgumentParser(description=__doc__)
    operations = parser.add_subparsers(dest="operation", required=True)

    prepare_parser = operations.add_parser("prepare", help="Download and pin one official SDK.")
    prepare_parser.add_argument("--root", required=True, type=Path)

    build_parser = operations.add_parser("build", help="Extract declarations and validate native target.")
    build_parser.add_argument("--root", required=True, type=Path)
    build_parser.add_argument("--scratch", required=True, type=Path)
    build_parser.add_argument("--output", required=True, type=Path)
    build_parser.add_argument("--rid", required=True, choices=list(PLATFORMS))

    merge_parser = operations.add_parser("merge", help="Merge validated targets to the repository.")
    merge_parser.add_argument("--root", required=True, type=Path)
    merge_parser.add_argument("--output", required=True, type=Path)

    verify_parser = operations.add_parser("verify", help="Verify generated bindings, native assets, and NuGet package.")
    verify_parser.add_argument("--root", type=Path, default=Path(__file__).resolve().parents[2])
    verify_parser.add_argument("--package", type=Path)

    args = parser.parse_args()
    if args.operation == "prepare":
        prepare(args.root)
    elif args.operation == "build":
        build(args.root, args.scratch, args.output, args.rid)
    elif args.operation == "merge":
        merge(args.root, args.output)
    else:
        verify(args.root, args.package)


if __name__ == "__main__":
    main()
