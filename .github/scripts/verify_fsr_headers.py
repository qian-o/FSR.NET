"""Check the reviewed FSR public-header grammar independently of Clang extraction.

This is an inventory checker for the SDK's C headers, not a C++ compiler.
C++ convenience wrappers are classified separately from the loader exports.
"""

from __future__ import annotations

import argparse
import ast
from collections import Counter
import hashlib
import json
import operator
from pathlib import Path
import re

COMPONENTS = {"api", "upscalers", "framegeneration", "denoisers", "radiancecache"}
BACKEND_CALLBACKS = {
    "PfnFfxResourceAllocatorFunc", "PfnFfxResourceDeallocatorFunc",
    "PfnFfxHeapAllocatorFunc", "PfnFfxHeapDeallocatorFunc",
}
COMPILE_MACROS = {
    "FFX_API_ENTRY", "FFX_DEPRECATION", "FFX_PRAGMA_WARNING_PUSH", "FFX_PRAGMA_WARNING_POP",
    "FFX_PRAGMA_WARNING_DISABLE_DEPRECATIONS", "FFX_PRAGMA_WARNING_WARN_DEPRECATIONS",
}
# Reviewed C++ syntax sugar in SDK v2.4.0. Any semantic/source change requires review.
CPP_HEADER_HASHES = {
    "Kits/FidelityFX/api/include/dx12/ffx_api_dx12.hpp": "70af69c46c67971ce0f67e89deea6770bf056d9c5184d9d1ab7dfccdc98497df",
    "Kits/FidelityFX/api/include/ffx_api.hpp": "f57eb62e8de59f50f230336874c77daee0e43dafd5feb31f400a97b3c8bd18c6",
    "Kits/FidelityFX/api/include/ffx_api_matrix_helpers.hpp": "ce9e5b42942472ffe73ac1d9da9624dacfd99f98e1f555dc0a158632c62c0949",
    "Kits/FidelityFX/api/include/vk/ffx_api_vk.hpp": "b6164a603141972b0a023347659579cba8daf18a63563d46b4a0bc9b669ab0a2",
    "Kits/FidelityFX/denoisers/include/ffx_denoiser.hpp": "887cac2d2b37cf88992b871da7e1fec0561b0d1843fbaf01281480e92df74c5d",
    "Kits/FidelityFX/framegeneration/include/dx12/ffx_api_framegeneration_dx12.hpp": "acceb7d6689f307588eca8898e2f2906af5b632d28c63f26be4d107618b58cc8",
    "Kits/FidelityFX/framegeneration/include/ffx_framegeneration.hpp": "9d2ebdbb92d5343738577f6693d1ba2d1808d683c945167dc0d4682542cca27e",
    "Kits/FidelityFX/framegeneration/include/vk/ffx_api_framegeneration_vk.hpp": "39992d1c1fe6b47bc27f1045b3ad900dc7e39016360fce705a668c3a1ac267a8",
    "Kits/FidelityFX/radiancecache/include/ffx_radiancecache.hpp": "f0dab9e4152161c3f478e0c962d7c531a534f4c7e36141b927d58137b64596e2",
    "Kits/FidelityFX/upscalers/include/ffx_upscale.hpp": "e670015dd1babe0a61069deeebfa188c2a653b0bf624f412636df424c2686d39",
}
CPP_WRAPPERS = Counter({
    "operator!": 1, "ConvertReturnCode": 1, "LinkHeaders": 3, "CreateContext": 1,
    "DestroyContext": 1, "Configure": 2, "Query": 2, "Dispatch": 1,
    "InitHelper": 1, "DynamicCast": 2, "ffxConvertToMatrix4x4": 1,
})
DESCRIPTOR_OVERRIDES = {
    "ffxCreateBackendVKDesc_DEPRECATED": "FFX_API_CREATE_CONTEXT_DESC_TYPE_BACKEND_VK_DEPRECATED",
    "ffxCallbackDescFrameGenerationPresent": "FFX_API_CALLBACK_DESC_TYPE_FRAMEGENERATION_PRESENT",
    "FfxFrameGenerationConfig": "FFX_API_FRAME_GENERATION_CONFIG",
}
# Scalar spellings in the public win-x64 declarations and their Clang kind/byte width.
SCALAR_TYPES = {
    "bool": ("BOOL", 1),
    "char": ("CHAR_S", 1),
    "wchar_t": ("WCHAR", 2),
    "int32_t": ("INT", 4),
    "uint32_t": ("UINT", 4),
    "uint64_t": ("ULONGLONG", 8),
    "float": ("FLOAT", 4),
}


def require(condition, message):
    if not condition:
        raise RuntimeError(message)


def compare(actual, expected, label):
    require(actual == expected, f"{label} differ: header={actual!r}, AST={expected!r}")


def clean(text):
    pattern = r'"(?:\\.|[^"\\])*"|/\*.*?\*/|//[^\n]*'
    return re.sub(pattern, lambda match: match[0] if match[0].startswith('"') else "\n" * match[0].count("\n"), text, flags=re.S)


def tokens(text):
    return re.findall(r'"(?:\\.|[^"\\])*"|\w+|<<|>>|->|[^\s]', text)


def closing(text, start, left="{", right="}"):
    depth = 0
    for index in range(start, len(text)):
        depth += (text[index] == left) - (text[index] == right)
        if depth == 0:
            return index
    raise RuntimeError("Unclosed public declaration: " + text[start:start + 80])


def declarations(body):
    start = braces = parentheses = 0
    for index, character in enumerate(body):
        braces += (character == "{") - (character == "}")
        parentheses += (character == "(") - (character == ")")
        if character == ";" and braces == 0 and parentheses == 0:
            declaration = body[start:index].strip()
            start = index + 1
            if declaration:
                yield declaration
    require(not body[start:].strip(), "Unrecognized declaration tail: " + body[start:start + 80])


def statement_end(text, start):
    braces = parentheses = 0
    for index in range(start, len(text)):
        character = text[index]
        braces += (character == "{") - (character == "}")
        parentheses += (character == "(") - (character == ")")
        if character == ";" and braces == 0 and parentheses == 0:
            return index
    raise RuntimeError("Unclosed typedef declaration: " + text[start:start + 80])


def fields(body):
    for declaration in declarations(body):
        if re.fullmatch(r"(?:struct|union)\s*\{.*\}", declaration, flags=re.S):
            yield from fields(declaration[declaration.index("{") + 1:declaration.rindex("}")])
        else:
            yield declaration


def field_name(declaration):
    callback = re.search(r"\(\s*\*\s*(\w+)\s*\)", declaration)
    if callback:
        return callback[1]
    match = re.search(r"(\w+)\s*(?:\[[^\]]*\]\s*)*$", declaration.split("=", 1)[0].strip())
    require(match is not None, "Unrecognized public field: " + declaration)
    return match[1]


def verify_field_type(declaration, field, header, aliases, tagged_types, integers, label):
    match = re.fullmatch(r"(.+?)\b" + re.escape(field["name"]) + r"\b((?:\s*\[[^]]+\])*)", declaration, re.S)
    require(match is not None, "Review unsupported field declarator: " + declaration)
    spelling = match[1].strip()
    if spelling in BACKEND_CALLBACKS:
        spelling += "DX12" if "/dx12/" in header else "VK"
    dimensions = [integers.expression(length) for length in re.findall(r"\[([^]]+)\]", match[2])]
    native = field["type"]
    for index, length in enumerate(dimensions):
        suffix = "".join(f"[{n}]" for n in dimensions[index:])
        compare(tokens(spelling + suffix), tokens(native["cpp"]), label + " array spelling")
        compare("CONSTANTARRAY", native["kind"], label + " array kind")
        compare(length, native["count"], label + " array length")
        compare(length * native["element"]["size"], native["size"], label + " array size")
        native = native["element"]

    while spelling.endswith("*"):
        compare(tokens(spelling), tokens(native["cpp"]), label + " pointer spelling")
        compare(("POINTER", 8), (native["kind"], native["size"]), label + " pointer")
        spelling = spelling[:-1].rstrip()
        native = native["element"]

    compare(tokens(spelling), tokens(native["cpp"]), label + " element spelling")
    name = re.sub(r"^(?:(?:const|struct|enum)\s+)+", "", spelling)
    alias = aliases.get(name)
    scalar = alias["cpp"] if alias is not None and alias["cpp"] in SCALAR_TYPES else name
    if scalar in SCALAR_TYPES:
        compare(SCALAR_TYPES[scalar], (native["kind"], native["size"]), label + " scalar type")
    elif name == "void":
        compare("VOID", native["kind"], label + " void type")
    elif alias is not None:
        # The field spells the typedef, whereas the alias spells its underlying type.
        target = {key: value for key, value in alias.items() if key != "cpp"}
        field_type = {key: value for key, value in native.items() if key != "cpp"}
        compare(target, field_type, label + " typedef type")
    elif name in tagged_types:
        compare((*tagged_types[name], name), (native["kind"], native["size"], native.get("name")), label + " named type")


class IntegerExpressions:
    """Only the integer operators and named macros used by the public SDK."""

    operations = {
        ast.BitOr: operator.or_, ast.BitAnd: operator.and_, ast.BitXor: operator.xor,
        ast.LShift: operator.lshift, ast.RShift: operator.rshift,
        ast.Add: operator.add, ast.Sub: operator.sub, ast.Mult: operator.mul,
    }

    def __init__(self, macros):
        self.macros = macros
        self.cache = {}

    def expression(self, text, variables=None):
        text = re.sub(r"\b(0[xX][0-9a-fA-F]+|\d+)[uUlL]+\b", r"\1", text)
        return self.value(ast.parse(text.strip(), mode="eval").body, variables or {})

    def macro(self, name, arguments=None):
        macro = self.macros[name]
        body = macro["tokens"]
        if arguments is None:
            require(not macro["functionLike"], "Function macro requires arguments: " + name)
            if name not in self.cache:
                self.cache[name] = self.expression(" ".join(body))
            return self.cache[name]
        require(macro["functionLike"], "Object macro called as a function: " + name)
        end = body.index(")")
        parameters = [token for token in body[1:end] if token != ","]
        compare(len(arguments), len(parameters), name + " argument count")
        return self.expression(" ".join(body[end + 1:]), dict(zip(parameters, arguments)))

    def value(self, node, variables):
        if isinstance(node, ast.Constant) and type(node.value) is int:
            return node.value
        if isinstance(node, ast.Name):
            return variables[node.id] if node.id in variables else self.macro(node.id)
        if isinstance(node, ast.UnaryOp) and type(node.op) in (ast.Invert, ast.USub, ast.UAdd):
            operation = {ast.Invert: operator.invert, ast.USub: operator.neg, ast.UAdd: operator.pos}[type(node.op)]
            return operation(self.value(node.operand, variables))
        if isinstance(node, ast.BinOp) and type(node.op) in self.operations:
            return self.operations[type(node.op)](self.value(node.left, variables), self.value(node.right, variables))
        if isinstance(node, ast.Call) and isinstance(node.func, ast.Name) and not node.keywords:
            return self.macro(node.func.id, [self.value(argument, variables) for argument in node.args])
        raise RuntimeError("Review unsupported integer expression: " + ast.dump(node))


def verify_components(paths):
    found = {match[1] for path in paths if (match := re.match(r"Kits/FidelityFX/([^/]+)/include/.*\.(?:h|hpp)$", path))}
    require(found == COMPONENTS, f"Review the changed SDK component set: {sorted(found)}")


def inventory(sdk):
    paths = sorted((sdk / "Kits/FidelityFX").glob("*/include/**/*"))
    paths = [path for path in paths if path.suffix in (".h", ".hpp")]
    verify_components([path.relative_to(sdk).as_posix() for path in paths])
    result = {key: {} for key in ("records", "enums", "aliases", "functions", "macros", "descriptors")}
    result["headers"] = []
    result["cppHeaders"] = []
    wrappers = Counter()
    for path in paths:
        header = path.relative_to(sdk).as_posix()
        raw = clean(path.read_text(encoding="utf-8-sig"))
        if path.suffix == ".hpp":
            fingerprint = hashlib.sha256(" ".join(tokens(raw)).encode("utf-8")).hexdigest()
            require(CPP_HEADER_HASHES.get(header) == fingerprint, "Review changed C++ convenience header: " + header)
        raw = raw.replace("\\\n", "")
        text = re.sub(r"^\s*#.*$", "", raw, flags=re.M)
        while match := re.search(r"\bFFX_DEPRECATION\s*\(", text):
            end = closing(text, text.index("(", match.start()), "(", ")")
            text = text[:match.start()] + " " * (end + 1 - match.start()) + text[end + 1:]
        if path.suffix == ".hpp":
            result["cppHeaders"].append(header)
            mappings = re.findall(r"struct_type\s*<\s*(\w+)\s*>\s*:\s*std::integral_constant\s*<\s*uint64_t\s*,\s*(\w+)\s*>", text)
            mappings += re.findall(r"FFX_DEFINE_DESC_MAPPING\(\s*\w+\s*,\s*(\w+)\s*,\s*(\w+)\s*\)", text)
            for name, value in mappings:
                require(name not in result["descriptors"], "Duplicate C++ descriptor mapping: " + name)
                result["descriptors"][name] = value
            functions = re.findall(r"^\s*(?:(?:static|inline|constexpr|const)\s+)*[\w:<>]+\s*[*&]?\s+(\w+|operator!)\s*\([^;{}]*\)\s*\{", text, re.M)
            wrappers.update(functions)
            wrappers["InitHelper"] += len(re.findall(r"\bInitHelper\(\)\s*:\s*Inner\(\)\s*\{", text))
            continue
        result["headers"].append(header)
        recognized = []
        for match in re.finditer(r"\b(struct|union|enum)\s+(\w+)\s*\{", text):
            kind, name = match.groups()
            start = text.index("{", match.start())
            end = closing(text, start)
            recognized.append((match.start(), end + 1))
            body = text[start + 1:end]
            category = "enums" if kind == "enum" else "records"
            require(name not in result[category], "Duplicate public declaration: " + name)
            if kind == "enum":
                values = [value.strip() for value in body.split(",") if value.strip()]
            else:
                values = list(fields(body))
            result[category][name] = {"header": header, "values": values}
        for match in re.finditer(r"\btypedef\b", text):
            end = statement_end(text, match.end())
            recognized.append((match.start(), end + 1))
            declaration = text[match.end():end].strip()
            callback = re.search(r"\(\s*\*\s*(\w+)\s*\)", declaration)
            alias = re.search(r"(\w+)\s*$", declaration)
            require(callback is not None or alias is not None, "Unrecognized public typedef: " + declaration)
            name = callback[1] if callback else alias[1]
            if name in BACKEND_CALLBACKS:
                name += "DX12" if "/dx12/" in header else "VK"
            require(name not in result["aliases"], "Duplicate public typedef: " + name)
            result["aliases"][name] = {"callback": bool(callback), "declaration": declaration}
        for match in re.finditer(r"^\s*(FFX_API_ENTRY|static\s+inline)\s+([^;{}]*?)\b(ffx\w+)\s*\(([^{}]*?)\)\s*([;{])", text, re.M):
            prefix, return_type, name, parameters, ending = match.groups()
            require(name not in result["functions"], "Duplicate public function: " + name)
            result["functions"][name] = {"header": header, "inline": ending == "{", "result": return_type.strip(), "parameters": parameters}
            end = closing(text, match.end() - 1) + 1 if ending == "{" else match.end()
            recognized.append((match.start(), end))
        remaining = list(text)
        for start, end in recognized:
            remaining[start:end] = " " * (end - start)
        remainder = re.sub(r'extern\s*"C"\s*\{', "", "".join(remaining))
        require(not re.sub(r"[\s};]", "", remainder), "Review unsupported public C declaration in " + header + ": " + remainder.strip()[:120])
        for match in re.finditer(r"^\s*#\s*define\s+(\w+)([^\n]*)", raw, re.M):
            name, body = match.groups()
            if name == "WIN32_LEAN_AND_MEAN":
                continue  # Windows include configuration, already supplied by the extractor.
            require(name.startswith("FFX_"), "Review new public header macro: " + name)
            if name not in COMPILE_MACROS:
                require(name not in result["macros"], "Duplicate runtime macro: " + name)
            result["macros"][name] = {"tokens": tokens(body), "functionLike": body.startswith("(")}
    compare(wrappers, CPP_WRAPPERS, "Reviewed C++ convenience wrappers")
    compare(set(result["cppHeaders"]), set(CPP_HEADER_HASHES), "Reviewed C++ header set")
    return result


def verify_inventory(source, platform):
    compare(source["headers"], platform["headers"], "Public C headers")
    records = {record["name"]: record for record in platform["records"] if not record["external"]}
    named = {name for name in records if not name.startswith("Anonymous_")}
    compare(set(source["records"]), named, "Public record names")
    tagged_types = {
        item["name"]: (kind, item["size"])
        for group, kind in (("records", "RECORD"), ("enums", "ENUM"))
        for item in platform[group]
    }
    integers = IntegerExpressions(source["macros"])
    anonymous = set()
    field_count = 0

    def verify_fields(declarations, record):
        nonlocal field_count
        compare([field_name(declaration) for declaration in declarations], [field["name"] for field in record["fields"]], record["name"] + " fields")
        field_count += len(declarations)
        for declaration, field in zip(declarations, record["fields"]):
            label = record["name"] + "." + field["name"]
            if re.match(r"struct\s*\{", declaration):
                name = field["type"]["name"]
                anonymous.add(name)
                compare(("RECORD", records[name]["size"]), (field["type"]["kind"], field["type"]["size"]), label + " anonymous type")
                verify_fields(list(fields(declaration[declaration.index("{") + 1:declaration.rindex("}")])), records[name])
            else:
                compare(tokens(declaration), tokens(field["declaration"]), label)
                verify_field_type(declaration, field, record["header"], platform["aliases"], tagged_types, integers, label)

    for name, record in source["records"].items():
        compare(record["header"], records[name]["header"], name + " header")
        verify_fields(record["values"], records[name])
    compare(anonymous, set(records) - named, "Anonymous public records")
    enums = {value["name"]: value for value in platform["enums"] if not value["external"]}
    compare(set(source["enums"]), set(enums), "Public enum names")
    for name, value in source["enums"].items():
        actual = []
        variables = {}
        previous = -1
        for declaration in value["values"]:
            parts = declaration.split("=", 1)
            symbol = parts[0].strip()
            require(re.fullmatch(r"\w+", symbol) is not None, "Unrecognized enum member: " + declaration)
            number = integers.expression(parts[1], variables) if len(parts) == 2 else previous + 1
            actual.append({"name": symbol, "value": number})
            variables[symbol] = previous = number
        compare(actual, enums[name]["values"], name + " members and values")
    compare(set(source["aliases"]), set(platform["aliases"]), "Public typedefs")
    for name, entry in source["aliases"].items():
        alias = platform["aliases"][name]
        compare(entry["callback"], alias["kind"] == "POINTER" and alias["element"]["kind"] == "FUNCTIONPROTO", name + " callback classification")
        declaration = entry["declaration"]
        if entry["callback"]:
            callback = re.search(r"\(\s*\*\s*(\w+)\s*\)", declaration)
            function = alias["element"]
            compare(tokens(declaration[:callback.start()]), tokens(function["result"]["cpp"]), name + " callback result")
            parameters = declaration[callback.end():].strip()[1:-1].strip()
            arguments = parameters.split(",") if parameters and parameters != "void" else []
            compare(len(arguments), len(function["arguments"]), name + " callback arguments")
            for argument, native in zip(arguments, function["arguments"]):
                actual_type = tokens(argument)
                expected_type = tokens(native["cpp"])
                compare(actual_type[:len(expected_type)], expected_type, name + " callback parameter type")
                rest = actual_type[len(expected_type):]
                require(not rest or len(rest) == 1 and re.fullmatch(r"[A-Za-z_]\w*", rest[0]), "Unsupported callback parameter: " + argument)
        else:
            underlying = declaration[:declaration.index("{")] if "{" in declaration else re.sub(r"\b\w+\s*$", "", declaration)
            compare(tokens(underlying), tokens(alias["cpp"]), name + " typedef target")
    functions = {function["name"]: function for function in platform["functions"] + platform["helpers"]}
    compare(set(source["functions"]), set(functions), "Public exports and inline helpers")
    for name, function in source["functions"].items():
        native = functions[name]
        compare(function["inline"], native["inline"], name + " inline/export classification")
        compare(tokens(function["result"]), tokens(native["result"]["cpp"]), name + " result declaration")
        parameters = [parameter.split("=", 1)[0].strip() for parameter in function["parameters"].split(",") if parameter.strip()]
        compare(len(parameters), len(native["parameters"]), name + " parameter count")
        for declaration, parameter in zip(parameters, native["parameters"]):
            compare(field_name(declaration), parameter["name"], name + " parameter name")
            compare(tokens(declaration[:declaration.rfind(parameter["name"])].strip()), tokens(parameter["type"]["cpp"]), name + " parameter type")
    macros = {macro["name"]: macro for macro in platform["macros"]}
    compare(set(source["macros"]), set(macros), "Public macros")
    for name, macro in source["macros"].items():
        if name not in COMPILE_MACROS:
            compare(macro["tokens"], macros[name]["tokens"], name + " body")
            compare(macro["functionLike"], macros[name]["functionLike"], name + " function classification")
            if not macro["functionLike"] and not all(token.startswith('"') for token in macro["tokens"]):
                require("value" in macros[name], "Numeric public macro has no AST value: " + name)
                compare(integers.macro(name), macros[name]["value"], name + " numeric value")
    expected_descriptors = {name: record["descriptorType"] for name, record in records.items() if "descriptorType" in record}
    compare(set(source["descriptors"]) | set(DESCRIPTOR_OVERRIDES), set(expected_descriptors), "C++ descriptor coverage")
    for name, value in source["descriptors"].items():
        require(name in records, "C++ descriptor has no public C record: " + name)
        compare(value, records[name].get("descriptorType"), name + " C++ descriptor type")
    for name, value in DESCRIPTOR_OVERRIDES.items():
        compare(value, expected_descriptors[name], name + " reviewed C-only descriptor type")
    print(f"Verified header inventory: {len(source['headers'])} C and {len(source['cppHeaders'])} C++ headers; {len(records)} SDK records/{field_count} fields, {len(enums)} enums/{sum(len(value['values']) for value in source['enums'].values())} members, {len(source['aliases'])} typedefs/{sum(alias['callback'] for alias in source['aliases'].values())} callbacks, {len(macros)} macros, {len(platform['functions'])} exports/{len(platform['helpers'])} inline helpers, {len(source['descriptors'])} C++ descriptor mappings. C++ source fingerprints require manual review; wrappers are not DLL imports.")


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--sdk", required=True, type=Path)
    parser.add_argument("--ast", required=True, type=Path)
    args = parser.parse_args()
    ast = json.loads(args.ast.read_text(encoding="utf-8"))
    platform = ast["platforms"]["win-x64"] if "platforms" in ast else ast["platform"]
    verify_inventory(inventory(args.sdk.resolve()), platform)


if __name__ == "__main__":
    main()
