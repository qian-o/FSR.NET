"""Prepare one official SDK, extract its C ABI, and publish validated assets.

SDK downloads and Clang extraction stay in Actions. AMD already supplies the
loader C ABI, so this workflow does not compile or ship a native bridge.
"""

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
import tarfile
import urllib.request

from parse_fsr_ast import COMPONENTS, EXPORTS, extract, public_headers
from verify_fsr_headers import inventory, verify_components, verify_inventory

REPOSITORY = "GPUOpen-LibrariesAndSDKs/FidelityFX-SDK"
SDK_RELEASE = "v2.4.0"
SDK_COMMIT = "7539ae8b6568ad29ed78a753404351daf3b5cca7"
VULKAN_RELEASE = "v1.4.321"
VULKAN_COMMIT = "2cd90f9d20df57eac214c148f3aed885372ddcfe"
LLVM_VERSION = "21.1.7"
LLVM_ARCHIVE = "clang+llvm-21.1.7-x86_64-pc-windows-msvc.tar.xz"
LLVM_SHA256 = "70a2b73f2f14f787557f90abf380e7170b54e97b893218999144de5284b4f8f8"
DLLS = sorted("amd_fidelityfx_" + name + ".dll" for name in (
    "loader_dx12", "loader_vk", "upscaler_dx12", "upscaler_vk",
    "framegeneration_dx12", "framegeneration_vk", "denoiser_dx12", "radiancecache_dx12"))


def request(url):
    headers = {"User-Agent": "FSR.NET"}
    if url.startswith("https://api.github.com/") and os.environ.get("GH_TOKEN"):
        headers["Authorization"] = "Bearer " + os.environ["GH_TOKEN"]
    with urllib.request.urlopen(urllib.request.Request(url, headers=headers), timeout=180) as response:
        return response.read()


def api(repository, path):
    return json.loads(request("https://api.github.com/repos/" + repository + "/" + path))


def write_text(path, value, encoding="utf-8"):
    path.parent.mkdir(parents=True, exist_ok=True)
    with path.open("w", encoding=encoding, newline="\n") as stream:
        stream.write(value)


def save(path, data):
    write_text(path, json.dumps(data, indent=2, ensure_ascii=False) + "\n")


def sha256(path):
    return hashlib.sha256(path.read_bytes()).hexdigest()


def download(repository, commit, paths, root):
    def copy(path):
        data = request(f"https://raw.githubusercontent.com/{repository}/{commit}/{path}")
        destination = root / path
        destination.parent.mkdir(parents=True, exist_ok=True)
        destination.write_bytes(data)

    with ThreadPoolExecutor(max_workers=8) as pool:
        list(pool.map(copy, paths))


def prepare(root, release):
    commit = api(REPOSITORY, "commits/" + release)["sha"]
    if release == SDK_RELEASE and commit != SDK_COMMIT:
        raise RuntimeError("The pinned SDK tag has moved.")
    tree = api(REPOSITORY, f"git/trees/{commit}?recursive=1")
    if tree.get("truncated"):
        raise RuntimeError("SDK tree response is truncated.")
    paths = [item["path"] for item in tree["tree"] if item["type"] == "blob"]
    verify_components(paths)
    prefixes = tuple(f"Kits/FidelityFX/{c}/include/" for c in COMPONENTS)
    selected = [p for p in paths if p.startswith(prefixes) and p.endswith((".h", ".hpp"))]
    selected += ["Kits/FidelityFX/signedbin/" + name for name in DLLS]
    download(REPOSITORY, commit, selected, root)
    vulkan_tree = api("KhronosGroup/Vulkan-Headers", f"git/trees/{VULKAN_COMMIT}?recursive=1")
    vulkan = [item["path"] for item in vulkan_tree["tree"] if item["type"] == "blob" and item["path"].startswith("include/")]
    download("KhronosGroup/Vulkan-Headers", VULKAN_COMMIT, vulkan, root / "vulkan")
    save(root / "source.json", {"repository": "https://github.com/" + REPOSITORY, "release": release, "commit": commit,
         "vulkanRelease": VULKAN_RELEASE, "vulkanCommit": VULKAN_COMMIT,
         "files": {p: sha256(root / p) for p in sorted(selected)},
         "vulkanFiles": {p: sha256(root / "vulkan" / p) for p in sorted(vulkan)}})
    print(f"Using {release} at {commit}", flush=True)


def install_llvm(scratch):
    archive = scratch / LLVM_ARCHIVE
    print("Using " + LLVM_ARCHIVE, flush=True)
    url = f"https://github.com/llvm/llvm-project/releases/download/llvmorg-{LLVM_VERSION}/{LLVM_ARCHIVE}"
    with urllib.request.urlopen(url, timeout=180) as response, archive.open("wb") as output:
        shutil.copyfileobj(response, output)
    if sha256(archive) != LLVM_SHA256:
        raise RuntimeError("LLVM archive checksum does not match.")
    destination = scratch / "llvm"
    with tarfile.open(archive) as package:
        package.extractall(destination, filter="data")
    archive.unlink()
    return next(destination.glob("*/bin/clang.exe")).parents[1]


def pe_info(path):
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

    def offset(rva):
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
    return {"machine": "AMD64", "machineValue": machine, "sha256": sha256(path), "size": len(data), "exports": sorted(exports)}


def check_sources(root, source):
    for path, expected in source["files"].items():
        if sha256(root / path) != expected:
            raise RuntimeError("SDK source checksum mismatch: " + path)
    for path, expected in source["vulkanFiles"].items():
        if sha256(root / "vulkan" / path) != expected:
            raise RuntimeError("Vulkan source checksum mismatch: " + path)


def check_msvc_layouts(root, scratch, ast):
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
            lines += [f'static_assert(sizeof({name}) == {record["size"]}, "{name} size");',
                      f'static_assert(alignof({name}) == {record["alignment"]}, "{name} alignment");']
            for field in record["fields"]:
                lines += [f'static_assert(offsetof({name}, {field["name"]}) == {field["offset"] // 8}, "{name}.{field["name"]} offset");',
                          f'static_assert(sizeof((({name}*)0)->{field["name"]}) == {field["type"]["size"]}, "{name}.{field["name"]} size");']
        path = scratch / ("abi_" + backend + ".cpp")
        write_text(path, "\n".join(lines) + "\n")
        subprocess.run(["cl", "/nologo", "/c", "/std:c++20", "/DNOMINMAX", "/DWIN32_LEAN_AND_MEAN", "/D_WINDOWS", "/I" + str(root / "vulkan/include"), str(path), "/Fo" + str(scratch / ("abi_" + backend + ".obj"))], check=True)


def build(root, scratch, output, compiler=None, library=None, include_paths=()):
    root, scratch, output = root.resolve(), scratch.resolve(), output.resolve()
    scratch.mkdir(parents=True, exist_ok=True)
    source = json.loads((root / "source.json").read_text(encoding="utf-8"))
    check_sources(root, source)
    headers = inventory(root)
    if compiler is None:
        llvm = install_llvm(scratch)
        compiler, library = llvm / "bin/clang.exe", llvm / "bin/libclang.dll"
    ast = extract(root, scratch, compiler, library, include_paths)
    verify_inventory(headers, ast)
    check_msvc_layouts(root, scratch, ast)
    native = output / "native/win-x64"
    native.mkdir(parents=True, exist_ok=True)
    binaries = {}
    for name in DLLS:
        path = root / "Kits/FidelityFX/signedbin" / name
        info = pe_info(path)
        if set(info["exports"]) != set(EXPORTS):
            raise RuntimeError("Official DLL exports differ from the public API: " + name)
        binaries[name] = info
        shutil.copy2(path, native / name)
    save(output / "ast.json", {"source": source, "rid": "win-x64", "platform": ast, "binaries": binaries})
    print(f"Extracted {len(ast['records'])} records, {len(ast['enums'])} enums, {len(ast['functions'])} exports; validated {len(binaries)} official DLLs.", flush=True)


def merge(root, output):
    artifact = root / "fsr-win-x64"
    fragment = json.loads((artifact / "ast.json").read_text(encoding="utf-8"))
    if fragment["rid"] != "win-x64":
        raise RuntimeError("Target artifact RID does not match.")
    if set(fragment["binaries"]) != set(DLLS):
        raise RuntimeError("Target artifact does not contain the expected official DLLs.")
    for name in DLLS:
        info = pe_info(artifact / "native/win-x64" / name)
        if info != fragment["binaries"][name]:
            raise RuntimeError("Native asset differs from its export check: " + name)
        if info["sha256"] != fragment["source"]["files"]["Kits/FidelityFX/signedbin/" + name]:
            raise RuntimeError("Native asset differs from its SDK source checksum: " + name)
        if set(info["exports"]) != set(EXPORTS):
            raise RuntimeError("Official DLL exports differ from the public API: " + name)
    packaging = output / "NuGet.Packaging.props"
    metadata = packaging.read_text(encoding="utf-8-sig")
    version = fragment["source"]["release"].removeprefix("v")
    metadata, replacements = re.subn(r"(?<=<Version>)[^<]*(?=</Version>)", lambda match: version, metadata)
    if replacements != 1:
        raise RuntimeError("NuGet.Packaging.props must contain one package Version.")
    destination = output / "native"
    if destination.exists():
        shutil.rmtree(destination)
    native = destination / "win-x64"
    native.mkdir(parents=True)
    for name in DLLS:
        shutil.copy2(artifact / "native/win-x64" / name, native / name)
    save(output / "FSR.NET.Generator/ast.json", {"source": fragment["source"], "platforms": {"win-x64": fragment["platform"]}})
    write_text(packaging, metadata, encoding="utf-8-sig")


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    operations = parser.add_subparsers(dest="operation", required=True)

    prepare_parser = operations.add_parser("prepare", help="Download and pin one official SDK.")
    prepare_parser.add_argument("--root", required=True, type=Path)
    prepare_parser.add_argument("--release", default=SDK_RELEASE)

    build_parser = operations.add_parser("build", help="Extract declarations and validate native assets.")
    build_parser.add_argument("--root", required=True, type=Path)
    build_parser.add_argument("--scratch", required=True, type=Path)
    build_parser.add_argument("--output", required=True, type=Path)
    build_parser.add_argument("--compiler", type=Path)
    build_parser.add_argument("--libclang", type=Path)
    build_parser.add_argument("--include", action="append", default=[])

    merge_parser = operations.add_parser("merge", help="Publish validated assets to the repository.")
    merge_parser.add_argument("--root", required=True, type=Path)
    merge_parser.add_argument("--output", required=True, type=Path)

    args = parser.parse_args()
    if args.operation == "prepare":
        prepare(args.root, args.release)
    elif args.operation == "build":
        if (args.compiler is None) != (args.libclang is None):
            build_parser.error("--compiler and --libclang must be specified together")
        build(args.root, args.scratch, args.output, args.compiler, args.libclang, args.include)
    else:
        merge(args.root, args.output)


if __name__ == "__main__":
    main()
