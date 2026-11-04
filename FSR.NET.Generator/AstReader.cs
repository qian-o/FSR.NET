namespace FSR.NET.Generator;

internal static class AstReader
{
    internal static AstModel Read(JsonElement ast)
    {
        JsonElement[] platforms = [.. ast.GetProperty("platforms").EnumerateObject().Select(static platform => platform.Value)];
        Dictionary<string, AstRecord> records = Merge(platforms, "records", ReadRecord, GetRecordSignature);
        Dictionary<string, AstEnum> enums = Merge(platforms, "enums", ReadEnum, GetEnumSignature);
        Dictionary<string, AstFunction> functions = Merge(platforms, "functions", ReadFunction, GetFunctionSignature);
        Dictionary<string, AstMacro> macros = Merge(platforms, "macros", ReadMacro, GetMacroSignature);
        Dictionary<string, AstType> aliases = [];
        foreach (JsonElement platform in platforms)
        {
            foreach (JsonProperty alias in platform.GetProperty("aliases").EnumerateObject())
            {
                AstType type = ReadType(alias.Value);
                if (aliases.TryGetValue(alias.Name, out AstType? previous) && GetTypeSignature(previous) != GetTypeSignature(type))
                {
                    throw new InvalidOperationException($"Incompatible native alias: {alias.Name}.");
                }

                aliases[alias.Name] = type;
            }
        }

        return new(records, enums, functions, macros, aliases);
    }

    private static Dictionary<string, T> Merge<T>(ReadOnlySpan<JsonElement> platforms, string collection, Func<JsonElement, T> read, Func<T, string> signature)
    {
        Dictionary<string, T> result = [];
        foreach (JsonElement platform in platforms)
        {
            foreach (JsonElement value in platform.GetProperty(collection).EnumerateArray())
            {
                string name = value.GetProperty("name").GetString()!;
                T declaration = read(value);
                if (result.TryGetValue(name, out T? previous))
                {
                    if (signature(previous) != signature(declaration))
                    {
                        throw new InvalidOperationException($"Incompatible native {collection} declaration: {name}.");
                    }
                }
                else
                {
                    result.Add(name, declaration);
                }
            }
        }

        return result;
    }

    private static string GetRecordSignature(AstRecord record)
    {
        string fields = string.Join(";", record.Fields.Select(static field => $"{field.Name}:{field.OffsetInBits}:{GetTypeSignature(field.Type)}:{GetInitializer(field.Declaration)}"));

        return $"{record.IsUnion}:{record.IsOpaque}:{record.SizeInBytes}:{fields}";
    }

    private static string GetFunctionSignature(AstFunction function)
    {
        string parameters = string.Join(";", function.Parameters.Select(static parameter => $"{parameter.Name}:{GetTypeSignature(parameter.Type)}"));

        return $"{function.Export}:{GetTypeSignature(function.Result)}:{parameters}";
    }

    private static string GetEnumSignature(AstEnum value)
    {
        return string.Join(";", value.Values.Select(static member => $"{member.Name}:{unchecked((uint)member.Value)}"));
    }

    private static string GetMacroSignature(AstMacro macro)
    {
        return $"{macro.IsFunctionLike}:{string.Join(' ', macro.Tokens)}";
    }

    private static string GetTypeSignature(AstType type)
    {
        if (type.Cpp.Replace("const ", string.Empty, StringComparison.Ordinal).Trim() is "size_t")
        {
            return $"size_t:{type.SizeInBytes}";
        }

        if (type.Kind is NativeTypeKind.Pointer or NativeTypeKind.LValueReference or NativeTypeKind.RValueReference)
        {
            return $"{type.Kind}:{type.SizeInBytes}:{GetTypeSignature(type.Element!)}";
        }

        return type.Kind switch
        {
            NativeTypeKind.CharS or NativeTypeKind.CharU => $"char:{type.SizeInBytes}",
            NativeTypeKind.Long or NativeTypeKind.LongLong => $"signed:{type.SizeInBytes}",
            NativeTypeKind.ULong or NativeTypeKind.ULongLong => $"unsigned:{type.SizeInBytes}",
            NativeTypeKind.FunctionProto or NativeTypeKind.FunctionNoProto => $"function:{type.CallingConvention}:{GetTypeSignature(type.Result!)}({string.Join(';', type.Arguments.Select(GetTypeSignature))})",
            NativeTypeKind.ConstantArray => $"array:{type.Count}:{type.SizeInBytes}:{GetTypeSignature(type.Element!)}",
            _ => $"{type.Kind}:{type.SizeInBytes}:{type.Name}"
        };
    }

    private static string GetInitializer(string? declaration)
    {
        return declaration is not null && declaration.Contains('=') ? declaration.Split('=', 2)[1].Trim() : string.Empty;
    }

    private static AstType ReadType(JsonElement value)
    {
        string cpp = value.GetProperty("cpp").GetString()!;
        string nativeKind = value.GetProperty("kind").GetString()!;
        NativeTypeKind kind = nativeKind switch
        {
            "VOID" => NativeTypeKind.Void,
            "BOOL" => NativeTypeKind.Bool,
            "CHAR_S" => NativeTypeKind.CharS,
            "CHAR_U" => NativeTypeKind.CharU,
            "SCHAR" => NativeTypeKind.SChar,
            "UCHAR" => NativeTypeKind.UChar,
            "SHORT" => NativeTypeKind.Short,
            "USHORT" => NativeTypeKind.UShort,
            "INT" => NativeTypeKind.Int,
            "UINT" => NativeTypeKind.UInt,
            "LONG" => NativeTypeKind.Long,
            "ULONG" => NativeTypeKind.ULong,
            "LONGLONG" => NativeTypeKind.LongLong,
            "ULONGLONG" => NativeTypeKind.ULongLong,
            "FLOAT" => NativeTypeKind.Float,
            "DOUBLE" => NativeTypeKind.Double,
            "WCHAR" => NativeTypeKind.WChar,
            "POINTER" => NativeTypeKind.Pointer,
            "LVALUEREFERENCE" => NativeTypeKind.LValueReference,
            "RVALUEREFERENCE" => NativeTypeKind.RValueReference,
            "RECORD" => NativeTypeKind.Record,
            "ENUM" => NativeTypeKind.Enum,
            "CONSTANTARRAY" => NativeTypeKind.ConstantArray,
            "FUNCTIONPROTO" => NativeTypeKind.FunctionProto,
            "FUNCTIONNOPROTO" => NativeTypeKind.FunctionNoProto,
            _ => throw new InvalidOperationException($"Unsupported native type kind: {nativeKind}.")
        };
        int sizeInBytes = value.GetProperty("size").GetInt32();
        string name = value.TryGetProperty("name", out JsonElement named) ? named.GetString()! : string.Empty;
        AstType? element = value.TryGetProperty("element", out JsonElement nested) ? ReadType(nested) : null;
        AstType? result = value.TryGetProperty("result", out JsonElement returned) ? ReadType(returned) : null;
        AstType[] arguments = value.TryGetProperty("arguments", out JsonElement parameters) ? [.. parameters.EnumerateArray().Select(ReadType)] : [];
        int count = value.TryGetProperty("count", out JsonElement length) ? length.GetInt32() : 0;
        string callingConvention = value.TryGetProperty("callingConvention", out JsonElement convention) ? convention.GetString()! : "Cdecl";

        return new(cpp, kind, sizeInBytes, name, element, result, arguments, count, callingConvention);
    }

    private static AstFunction ReadFunction(JsonElement value)
    {
        string name = value.GetProperty("name").GetString()!;
        string export = value.GetProperty("export").GetString()!;
        AstType result = ReadType(value.GetProperty("result"));
        AstParameter[] parameters = [.. value.GetProperty("parameters").EnumerateArray().Select(ReadParameter)];

        return new(name, export, result, parameters);
    }

    private static AstParameter ReadParameter(JsonElement value)
    {
        string name = value.GetProperty("name").GetString()!;
        AstType type = ReadType(value.GetProperty("type"));

        return new(name, type);
    }

    private static AstRecord ReadRecord(JsonElement value)
    {
        bool isUnion = value.GetProperty("kind").GetString() is "UNION_DECL";
        bool isOpaque = value.GetProperty("opaque").GetBoolean();
        int sizeInBytes = value.GetProperty("size").GetInt32();
        AstField[] fields = [.. value.GetProperty("fields").EnumerateArray().Select(ReadField)];
        string? descriptorType = value.TryGetProperty("descriptorType", out JsonElement descriptor) ? descriptor.GetString() : null;

        return new(isUnion, isOpaque, sizeInBytes, fields, descriptorType);
    }

    private static AstField ReadField(JsonElement value)
    {
        string name = value.GetProperty("name").GetString()!;
        int offsetInBits = value.GetProperty("offset").GetInt32();
        AstType type = ReadType(value.GetProperty("type"));
        string? declaration = value.TryGetProperty("declaration", out JsonElement declared) ? declared.GetString() : null;

        return new(name, offsetInBits, type, declaration);
    }

    private static AstEnum ReadEnum(JsonElement value)
    {
        AstEnumValue[] values = [.. value.GetProperty("values").EnumerateArray().Select(static member => new AstEnumValue(member.GetProperty("name").GetString()!, member.GetProperty("value").GetInt64()))];

        return new(values);
    }

    private static AstMacro ReadMacro(JsonElement value)
    {
        string name = value.GetProperty("name").GetString()!;
        string[] tokens = [.. value.GetProperty("tokens").EnumerateArray().Select(static token => token.GetString()!)];
        bool isFunctionLike = value.GetProperty("functionLike").GetBoolean();
        string? numeric = value.TryGetProperty("value", out JsonElement number) ? number.ToString() : null;

        return new(name, tokens, isFunctionLike, numeric);
    }
}
