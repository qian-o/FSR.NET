"""Extract the public FSR C API and its Windows x64 ABI on the Actions runner.

Each backend is parsed independently: AMD uses the same callback typedef names
for different D3D12 and Vulkan signatures. Header-only functions are recorded
separately and never presented as DLL exports.
"""

from __future__ import annotations

from ctypes import c_uint
import hashlib
import os
from pathlib import Path
import re
import subprocess

COMPONENTS = ("api", "upscalers", "framegeneration", "denoisers", "radiancecache")
EXPORTS = ("ffxCreateContext", "ffxDestroyContext", "ffxConfigure", "ffxQuery", "ffxDispatch")
BACKEND_ALIASES = {
    "PfnFfxResourceAllocatorFunc", "PfnFfxResourceDeallocatorFunc",
    "PfnFfxHeapAllocatorFunc", "PfnFfxHeapDeallocatorFunc",
}

# These C descriptors intentionally have no C++ InitHelper specialization.
DESCRIPTOR_TYPE_OVERRIDES = {
    "ffxCreateBackendVKDesc_DEPRECATED": "FFX_API_CREATE_CONTEXT_DESC_TYPE_BACKEND_VK_DEPRECATED",
    "ffxCallbackDescFrameGenerationPresent": "FFX_API_CALLBACK_DESC_TYPE_FRAMEGENERATION_PRESENT",
    "FfxFrameGenerationConfig": "FFX_API_FRAME_GENERATION_CONFIG",
}


def public_headers(sdk):
    base = sdk / "Kits/FidelityFX"
    return sorted(path for component in COMPONENTS for path in (base / component / "include").rglob("*.h"))


def add_descriptor_types(sdk, records):
    base = sdk / "Kits/FidelityFX"
    for component in COMPONENTS:
        for path in (base / component / "include").rglob("*.hpp"):
            text = path.read_text(encoding="utf-8-sig")
            mappings = re.findall(
                r"struct_type\s*<\s*(\w+)\s*>\s*:\s*std::integral_constant\s*<\s*uint64_t\s*,\s*(\w+)\s*>", text
            )
            mappings += re.findall(r"FFX_DEFINE_DESC_MAPPING\(\s*\w+\s*,\s*(\w+)\s*,\s*(\w+)\s*\)", text)
            for name, macro in mappings:
                if name in records:
                    records[name]["descriptorType"] = macro

    for name, macro in DESCRIPTOR_TYPE_OVERRIDES.items():
        records[name]["descriptorType"] = macro


def extract(sdk: Path, scratch: Path, compiler: Path, library: Path, include_paths=()):
    from clang import cindex as cx

    cx.Config.set_library_file(str(library))
    convention = cx.conf.lib.clang_getFunctionTypeCallingConv
    convention.argtypes = [cx.Type]
    convention.restype = c_uint
    resource = subprocess.check_output([str(compiler), "-print-resource-dir"], text=True).strip()
    flags = [
        "-x", "c++", "-std=c++20", "--target=x86_64-pc-windows-msvc", "-resource-dir=" + resource,
        "-fms-extensions", "-fms-compatibility", "-fms-compatibility-version=19.40",
        "-DNOMINMAX", "-DWIN32_LEAN_AND_MEAN", "-D_WINDOWS",
        "-Wno-deprecated-declarations", "-I" + str(sdk / "vulkan/include"),
    ]
    for path in include_paths or os.environ.get("INCLUDE", "").split(";"):
        if path:
            flags += ["-isystem", str(path)]
    records, enums, functions, aliases, macros, helpers = {}, {}, {}, {}, {}, {}
    headers = public_headers(sdk)
    owned = {str(path.resolve()) for path in headers}
    backend = ""

    def source(cursor):
        return Path(cursor.location.file.name).resolve() if cursor.location.file else None

    def public(cursor):
        path = source(cursor)
        return path is not None and str(path) in owned

    def header(cursor):
        path = source(cursor)
        if path is None:
            return ""
        if path.is_relative_to(sdk):
            return path.relative_to(sdk).as_posix()
        return path.name

    def renamed(name):
        return name + backend if name in BACKEND_ALIASES else name

    def type_info(native_type):
        canonical = native_type.get_canonical()
        structural = native_type
        while structural.kind in (cx.TypeKind.TYPEDEF, cx.TypeKind.ELABORATED):
            if structural.kind == cx.TypeKind.TYPEDEF:
                structural = structural.get_declaration().underlying_typedef_type
            else:
                structural = structural.get_named_type()
        kind = canonical.kind.name
        cpp = native_type.spelling
        for name in BACKEND_ALIASES:
            cpp = re.sub(r"\b" + name + r"\b", renamed(name), cpp)
        info = {"cpp": cpp, "kind": kind, "size": canonical.get_size()}
        if kind in ("POINTER", "LVALUEREFERENCE", "RVALUEREFERENCE"):
            info["element"] = type_info(structural.get_pointee())
        elif kind == "CONSTANTARRAY":
            info.update(element=type_info(structural.element_type), count=structural.element_count)
        elif kind in ("FUNCTIONPROTO", "FUNCTIONNOPROTO"):
            native_convention = convention(canonical)
            if native_convention not in (1, 2, 10):
                raise RuntimeError("Unsupported native calling convention: " + str(native_convention))
            info.update(
                result=type_info(structural.get_result()),
                arguments=[type_info(argument) for argument in structural.argument_types()],
                callingConvention="Stdcall" if native_convention == 2 else "Cdecl",
                clangCallingConvention=native_convention,
            )
        elif kind in ("RECORD", "ENUM"):
            decl = canonical.get_declaration()
            info["name"] = decl.spelling
            if decl.is_anonymous():
                info["name"] = "Anonymous_" + Path(header(decl)).stem + "_" + str(decl.location.line) + "_" + str(decl.location.column)
            collect(decl, info["name"])
        declaration = native_type.get_declaration()
        if declaration.kind == cx.CursorKind.TYPEDEF_DECL:
            tokens = {token.spelling for token in declaration.get_tokens()}
            # Clang normalizes __stdcall to the common ABI on x64. Preserve the
            # declared Windows convention of Vulkan/platform PFNs as well.
            if tokens.intersection({"VKAPI_PTR", "__stdcall", "WINAPI", "CALLBACK"}):
                function = info
                while function.get("element"):
                    function = function["element"]
                if function["kind"] in ("FUNCTIONPROTO", "FUNCTIONNOPROTO"):
                    function["callingConvention"] = "Stdcall"
        return info

    def record_fields(parent):
        for child in parent.get_children():
            if child.kind == cx.CursorKind.FIELD_DECL:
                yield child
            elif child.kind in (cx.CursorKind.UNION_DECL, cx.CursorKind.STRUCT_DECL) and child.is_anonymous():
                yield from record_fields(child)

    def collect_referenced_enums(cursor):
        if (cursor.kind == cx.CursorKind.DECL_REF_EXPR and cursor.referenced
                and cursor.referenced.kind == cx.CursorKind.ENUM_CONSTANT_DECL):
            collect(cursor.referenced.semantic_parent)
        for child in cursor.get_children():
            collect_referenced_enums(child)

    def collect(cursor, forced_name=None):
        name = forced_name or cursor.spelling
        if not name:
            return
        kind = cursor.kind.name
        if kind in ("STRUCT_DECL", "UNION_DECL", "CLASS_DECL"):
            if name in records and (records[name].get("fields") or not cursor.is_definition()):
                return
            external = not public(cursor)
            # Platform COM objects are consumed only through pointers, never by value.
            opaque = not cursor.is_definition() or external and (name.startswith(("ID3D", "IDXGI")) or name.endswith("_T"))
            record = {
                "name": name, "kind": kind, "header": header(cursor),
                "size": cursor.type.get_size(), "alignment": cursor.type.get_align(),
                "opaque": opaque, "external": external, "fields": [],
            }
            records[name] = record
            if not opaque:
                for field in record_fields(cursor):
                    offset = cursor.type.get_offset(field.spelling)
                    # Named members of a nested anonymous type belong to that type,
                    # unlike injected members of an unnamed anonymous union.
                    if offset >= 0:
                        record["fields"].append({
                            "name": field.spelling, "offset": offset, "type": type_info(field.type),
                            "declaration": " ".join(token.spelling for token in field.get_tokens()),
                        })
            if not opaque and record["size"] < 0:
                raise RuntimeError("Incomplete record layout: " + name)
        elif kind == "ENUM_DECL" and name not in enums:
            enums[name] = {
                "name": name, "header": header(cursor), "external": not public(cursor),
                "size": cursor.enum_type.get_size(), "unsigned": cursor.enum_type.kind.name.startswith("U"),
                "values": [
                    {"name": value.spelling, "value": value.enum_value}
                    for value in cursor.get_children() if value.kind == cx.CursorKind.ENUM_CONSTANT_DECL
                ],
            }

    def walk(cursor):
        if public(cursor):
            if cursor.kind in (cx.CursorKind.STRUCT_DECL, cx.CursorKind.UNION_DECL, cx.CursorKind.ENUM_DECL, cx.CursorKind.CLASS_DECL):
                collect(cursor)
            elif cursor.kind == cx.CursorKind.TYPEDEF_DECL:
                aliases[renamed(cursor.spelling)] = type_info(cursor.underlying_typedef_type)
            elif cursor.kind == cx.CursorKind.FUNCTION_DECL:
                name = cursor.spelling
                function = {
                    "name": name, "header": header(cursor), "line": cursor.location.line,
                    "result": type_info(cursor.result_type),
                    "parameters": [
                        {"name": parameter.spelling or f"arg{index}", "type": type_info(parameter.type)}
                        for index, parameter in enumerate(cursor.get_arguments())
                    ],
                    "inline": cursor.is_definition(), "callingConvention": "Cdecl",
                }
                if cursor.is_definition():
                    # Track header-only helper behavior without exporting C++ code.
                    implementation = " ".join(token.spelling for token in cursor.get_tokens())
                    function["implementationSha256"] = hashlib.sha256(implementation.encode("utf-8")).hexdigest()
                    helpers[name] = function
                    # Constants used by the public helper implementations are
                    # part of their managed port even when parameters use flags.
                    collect_referenced_enums(cursor)
                else:
                    if name not in EXPORTS:
                        raise RuntimeError("Unexpected public non-inline function: " + name)
                    function["export"] = name
                    functions[name] = function
            elif cursor.kind == cx.CursorKind.MACRO_DEFINITION:
                tokens = list(cursor.get_tokens())
                macros[cursor.spelling] = {
                    "name": cursor.spelling, "tokens": [token.spelling for token in tokens[1:]],
                    "functionLike": (len(tokens) > 1 and tokens[1].spelling == "("
                                     and tokens[0].extent.end.offset == tokens[1].extent.start.offset),
                    "header": header(cursor),
                }
        if cursor.kind in (cx.CursorKind.TRANSLATION_UNIT, cx.CursorKind.LINKAGE_SPEC, cx.CursorKind.UNEXPOSED_DECL, cx.CursorKind.NAMESPACE):
            for child in cursor.get_children():
                walk(child)

    def parse(unit):
        translation_unit = cx.Index.create().parse(str(unit), args=flags, options=cx.TranslationUnit.PARSE_DETAILED_PROCESSING_RECORD)
        errors = [str(diagnostic) for diagnostic in translation_unit.diagnostics if diagnostic.severity >= cx.Diagnostic.Error]
        if errors:
            raise RuntimeError(str(unit) + "\n" + "\n".join(errors))
        return translation_unit

    scratch.mkdir(parents=True, exist_ok=True)
    for backend in ("DX12", "VK"):
        other = "/vk/" if backend == "DX12" else "/dx12/"
        selected = [path for path in headers if other not in path.as_posix()]
        unit = scratch / (backend + ".cpp")
        prologue = "#include <stddef.h>\n#include <stdint.h>\n#include <string.h>\n"
        prologue += "\n".join('#include "' + path.as_posix() + '"' for path in selected) + "\n"
        unit.write_text(prologue, encoding="utf-8")
        translation_unit = parse(unit)
        walk(translation_unit.cursor)
        # Let the compiler evaluate numeric object macros rather than guessing C expressions.
        selected_paths = {path.relative_to(sdk).as_posix() for path in selected}
        candidates = [
            macro for macro in macros.values()
            if not macro["functionLike"] and macro["tokens"] and macro["name"].startswith("FFX_")
            and not macro["name"].startswith(("FFX_PRAGMA", "FFX_API_ENTRY"))
            and not any('"' in token for token in macro["tokens"])
            and macro["header"] in selected_paths
        ]
        declarations = [
            f'enum : unsigned long long {{ FSRNET_MACRO_{macro["name"]} = (unsigned long long)({macro["name"]}) }};'
            for macro in candidates
        ]
        unit.write_text(prologue + "\n".join(declarations), encoding="utf-8")
        evaluated = parse(unit)
        for cursor in evaluated.cursor.get_children():
            if cursor.kind == cx.CursorKind.ENUM_DECL:
                for value in cursor.get_children():
                    if value.kind == cx.CursorKind.ENUM_CONSTANT_DECL and value.spelling.startswith("FSRNET_MACRO_"):
                        macros[value.spelling.removeprefix("FSRNET_MACRO_")]["value"] = value.enum_value

    add_descriptor_types(sdk, records)
    if set(functions) != set(EXPORTS):
        raise RuntimeError("Public loader API differs: " + repr(sorted(functions)))
    return {
        "functions": sorted(functions.values(), key=lambda function: function["name"]),
        "records": sorted(records.values(), key=lambda record: record["name"]),
        "enums": sorted(enums.values(), key=lambda enum: enum["name"]),
        "aliases": dict(sorted(aliases.items())),
        "macros": sorted(macros.values(), key=lambda macro: macro["name"]),
        "helpers": sorted(helpers.values(), key=lambda function: function["name"]),
        "headers": [path.relative_to(sdk).as_posix() for path in headers],
    }
