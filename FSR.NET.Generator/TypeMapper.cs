namespace FSR.NET.Generator;

internal class TypeMapper
{
    private static readonly HashSet<string> Acronyms = ["API", "FSR", "D3D12", "DX12", "VK", "UI", "GPU", "HDR", "SR", "RR", "UAV", "SRGB", "UNORM", "SNORM", "UINT", "SINT", "FLOAT", "TYPELESS"];
    private static readonly Dictionary<string, string> NativeWords = new(StringComparer.OrdinalIgnoreCase)
    {
        ["NATIVEAA"] = "NativeAA",
        ["FRAMEGENERATION"] = "FrameGeneration",
        ["FRAMEGENERATIONSWAPCHAIN"] = "FrameGenerationSwapChain",
        ["RADIANCECACHE"] = "RadianceCache",
        ["SWAPCHAIN"] = "SwapChain",
        ["SWAPCHAINS"] = "SwapChains",
        ["FGSWAPCHAIN"] = "FGSwapChain",
        ["BACKBUFFER"] = "BackBuffer",
        ["AUTOREACTIVE"] = "AutoReactive",
        ["RENDERTARGET"] = "RenderTarget",
        ["DEPTHTARGET"] = "DepthTarget",
        ["STENCILTARGET"] = "StencilTarget",
        ["ARRAYVIEW"] = "ArrayView",
        ["WAITCALLBACK"] = "WaitCallback",
        ["FRAMEPACINGTUNING"] = "FramePacingTuning",
        ["TONEMAP"] = "ToneMap",
        ["INVERSETONEMAP"] = "InverseToneMap",
        ["COLORSPACE"] = "ColorSpace",
        ["FVELOCITYFACTOR"] = "FVelocityFactor",
        ["FREACTIVENESSSCALE"] = "FReactivenessScale",
        ["FSHADINGCHANGESCALE"] = "FShadingChangeScale",
        ["FACCUMULATIONADDEDPERFRAME"] = "FAccumulationAddedPerFrame",
        ["FMINDISOCCLUSIONACCUMULATION"] = "FMinDisocclusionAccumulation",
        ["REACTIVEMASK"] = "ReactiveMask",
        ["TRANSPARENCYCOMPOSITION"] = "TransparencyComposition",
        ["DESCTYPE"] = "DescType",
        ["SHAREDEXP"] = "SharedExp",
        ["KEYVALUE"] = "KeyValue",
        ["REGISTERUIRESOURCE"] = "RegisterUIResource",
        ["REGISTERDISTORTIONRESOURCE"] = "RegisterDistortionResource",
        ["GLOBALDEBUG"] = "GlobalDebug",
        ["GLOBALDEBUG1"] = "GlobalDebug1",
        ["CAMERAINFO"] = "CameraInfo",
        ["GENERATEREACTIVEMASK"] = "GenerateReactiveMask",
        ["INTERPOLATIONCOMMANDLIST"] = "InterpolationCommandList",
        ["INTERPOLATIONTEXTURE"] = "InterpolationTexture",
        ["GETJITTEROFFSET"] = "GetJitterOffset",
        ["GETJITTERPHASECOUNT"] = "GetJitterPhaseCount",
        ["GETRENDERRESOLUTIONFROMQUALITYMODE"] = "GetRenderResolutionFromQualityMode",
        ["GETUPSCALERATIOFROMQUALITYMODE"] = "GetUpscaleRatioFromQualityMode"
    };

    private readonly AstModel model;
    private readonly Dictionary<string, string> recordNames = [];

    internal TypeMapper(AstModel model)
    {
        this.model = model;

        foreach ((string name, AstRecord record) in model.Records)
        {
            foreach (AstField field in record.Fields)
            {
                if (field.Type.Kind is NativeTypeKind.Record && field.Type.Name.StartsWith("Anonymous_", StringComparison.Ordinal))
                {
                    string managed = $"{GetRecordName(name)}{GetFieldName(field.Name)}";
                    if (recordNames.TryGetValue(field.Type.Name, out string? previous) && previous != managed)
                    {
                        throw new InvalidOperationException($"Ambiguous anonymous record name: {field.Type.Name}.");
                    }

                    recordNames[field.Type.Name] = managed;
                }
            }
        }

        ValidateTypeNames();
    }

    internal string GetRecordName(string name)
    {
        if (recordNames.TryGetValue(name, out string? managed))
        {
            return managed;
        }

        if (name.StartsWith("Anonymous_", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Anonymous record has no containing field: {name}.");
        }

        return GetTypeName(name);
    }

    internal string GetFieldType(AstField field)
    {
        return field.Type.Kind switch
        {
            NativeTypeKind.Bool => "Bool8",
            NativeTypeKind.ConstantArray => $"{GetFieldName(field.Name)}Buffer",
            _ => GetManagedType(field.Type)
        };
    }

    internal string GetManagedType(AstType type)
    {
        string cpp = type.Cpp.Replace("const ", string.Empty, StringComparison.Ordinal).Trim();
        if (cpp is "size_t")
        {
            return "nuint";
        }

        if (cpp is "ffxContext")
        {
            return "FSRContext";
        }

        if (cpp.Replace(" ", string.Empty, StringComparison.Ordinal) is "ffxContext*")
        {
            return "FSRContext*";
        }

        if (cpp is "ffxReturnCode_t")
        {
            return "FSRApiReturnCodes";
        }

        if (type.Kind is NativeTypeKind.Pointer or NativeTypeKind.LValueReference or NativeTypeKind.RValueReference)
        {
            AstType element = type.Element!;
            if (element.Kind is NativeTypeKind.FunctionProto or NativeTypeKind.FunctionNoProto)
            {
                string arguments = string.Join(", ", element.Arguments.Select(GetManagedType).Append(GetManagedType(element.Result!)));

                return $"delegate* unmanaged[{GetCallingConvention(element)}]<{arguments}>";
            }

            if (element.Kind is NativeTypeKind.Record && (!model.Records.TryGetValue(element.Name, out AstRecord? record) || record.IsOpaque))
            {
                return "nint";
            }

            return $"{GetManagedType(element)}*";
        }

        if (type.Kind is NativeTypeKind.Enum or NativeTypeKind.Record)
        {
            return type.Kind is NativeTypeKind.Record ? GetRecordName(type.Name) : GetTypeName(type.Name);
        }

        return type.Kind switch
        {
            NativeTypeKind.Void => "void",
            NativeTypeKind.Bool => "byte",
            NativeTypeKind.CharS or NativeTypeKind.CharU or NativeTypeKind.UChar => "byte",
            NativeTypeKind.SChar => "sbyte",
            NativeTypeKind.Short => "short",
            NativeTypeKind.UShort => "ushort",
            NativeTypeKind.Int => "int",
            NativeTypeKind.UInt => "uint",
            NativeTypeKind.Long => type.SizeInBytes is 8 ? "long" : "int",
            NativeTypeKind.ULong => type.SizeInBytes is 8 ? "ulong" : "uint",
            NativeTypeKind.LongLong => "long",
            NativeTypeKind.ULongLong => "ulong",
            NativeTypeKind.Float => "float",
            NativeTypeKind.Double => "double",
            NativeTypeKind.WChar when type.SizeInBytes is 2 => "char",
            _ => throw new InvalidOperationException($"Unsupported native type: {type.Cpp}.")
        };
    }

    internal static string GetName(string name)
    {
        return string.Concat(name.Split('_', StringSplitOptions.RemoveEmptyEntries).Select(static part =>
        {
            if (NativeWords.TryGetValue(part, out string? word))
            {
                return word;
            }

            string value = Acronyms.Contains(part) || part.Any(char.IsLower)
                ? $"{char.ToUpperInvariant(part[0])}{part[1..]}"
                : CultureInfo.InvariantCulture.TextInfo.ToTitleCase(part.ToLowerInvariant());

            return Regex.Replace(value, @"(?:Framegeneration|Swapchain|Backbuffer|Autoreactive)(?=[A-Z0-9]|$)", static match => NativeWords[match.Value]);
        }));
    }

    internal static string GetTypeName(string native)
    {
        string name = native.StartsWith("PfnFfx", StringComparison.Ordinal) ? native[6..]
            : native.StartsWith("PFN_", StringComparison.Ordinal) ? native[4..]
            : native.StartsWith("ffx", StringComparison.OrdinalIgnoreCase) ? native[3..] : native;

        return $"FSR{GetName(name)}";
    }

    internal static string GetConstantName(string native)
    {
        return GetName(native.StartsWith("FFX_", StringComparison.Ordinal) ? native[4..] : native);
    }

    internal static string GetParameterName(string native)
    {
        native = NormalizeName(native);
        string name = GetName(native);
        name = char.IsLower(native[0]) ? $"{char.ToLowerInvariant(name[0])}{name[1..]}" : JsonNamingPolicy.CamelCase.ConvertName(name);

        return name switch
        {
            "abstract" or "as" or "base" or "bool" or "break" or "byte" or "case" or "catch" or "char" or "checked" or "class" or "const" or "continue" or "decimal" or "default" or "delegate" or "do" or "double" or "else" or "enum" or "event" or "explicit" or "extern" or "false" or "finally" or "fixed" or "float" or "for" or "foreach" or "goto" or "if" or "implicit" or "in" or "int" or "interface" or "internal" or "is" or "lock" or "long" or "namespace" or "new" or "null" or "object" or "operator" or "out" or "override" or "params" or "private" or "protected" or "public" or "readonly" or "ref" or "return" or "sbyte" or "sealed" or "short" or "sizeof" or "stackalloc" or "static" or "string" or "struct" or "switch" or "this" or "throw" or "true" or "try" or "typeof" or "uint" or "ulong" or "unchecked" or "unsafe" or "ushort" or "using" or "virtual" or "void" or "volatile" or "while" => $"@{name}",
            _ => name
        };
    }

    internal static string GetFunctionName(string native)
    {
        return GetName(native[3..]);
    }

    internal static string GetFieldName(string field)
    {
        return GetName(NormalizeName(field));
    }

    internal static string GetEnumMemberName(string value)
    {
        string name = string.Concat(value.Split('_', StringSplitOptions.RemoveEmptyEntries).Select(static part =>
        {
            if (NativeWords.TryGetValue(part, out string? word))
            {
                return word;
            }

            if (Regex.IsMatch(part, @"^[RGBADESX0-9]+$") && part.Any(char.IsDigit))
            {
                return part;
            }

            return string.Concat(Regex.Matches(part, @"[A-Z]+(?=[A-Z][a-z]|[0-9]|$)|[A-Z]?[a-z]+|[0-9]+").Select(static match => $"{char.ToUpperInvariant(match.Value[0])}{match.Value[1..].ToLowerInvariant()}"));
        }));

        return char.IsDigit(name[0]) ? $"_{name}" : name;
    }

    internal static string[] GetEnumMemberNames(AstEnum value)
    {
        string[] first = value.Values[0].Name.Split('_');
        int prefixLength = first.Length - 1;
        foreach (AstEnumValue item in value.Values)
        {
            string[] parts = item.Name.Split('_');
            prefixLength = Math.Min(prefixLength, parts.Length - 1);
            while (prefixLength > 0 && !first.AsSpan(0, prefixLength).SequenceEqual(parts.AsSpan(0, prefixLength)))
            {
                prefixLength--;
            }
        }

        string[] nativeNames = [.. value.Values.Select(item => string.Join('_', item.Name.Split('_').Skip(prefixLength)))];
        string[] names = [.. nativeNames.Select(GetEnumMemberName)];
        HashSet<string> collisions = [.. names.GroupBy(static member => member, StringComparer.Ordinal).Where(static group => group.Count() > 1).Select(static group => group.Key)];
        for (int i = 0; i < names.Length; i++)
        {
            if (collisions.Contains(names[i]))
            {
                names[i] = string.Join('_', nativeNames[i].Split('_').Select(GetEnumMemberName));
            }
        }

        ValidateNames(value.Values.Zip(names, static (item, name) => (item.Name, name)), "enum members");

        return names;
    }

    internal static void ValidateNames(IEnumerable<(string Native, string Managed)> source, string scope)
    {
        Dictionary<string, string> names = new(StringComparer.Ordinal);
        foreach ((string native, string managed) in source)
        {
            if (names.TryGetValue(managed, out string? previous))
            {
                throw new InvalidOperationException($"Conflicting managed name in {scope}: {previous} and {native} both map to {managed}.");
            }

            names.Add(managed, native);
        }
    }

    internal static string GetCallingConvention(AstType type)
    {
        return type.CallingConvention switch
        {
            "Cdecl" => "Cdecl",
            "Stdcall" or "StdCall" => "Stdcall",
            _ => throw new InvalidOperationException($"Unsupported calling convention: {type.CallingConvention}.")
        };
    }

    private void ValidateTypeNames()
    {
        IEnumerable<(string Native, string Managed)> records = model.Records
            .Where(static pair => !pair.Value.IsOpaque)
            .Select(pair => (pair.Key, GetRecordName(pair.Key)));
        IEnumerable<(string Native, string Managed)> enums = model.Enums.Keys.Select(static name => (name, GetTypeName(name)));
        IEnumerable<(string Native, string Managed)> aliases = model.Aliases
            .Where(static pair => pair.Key is "ffxContext" || pair.Value is { Kind: NativeTypeKind.Pointer, Element.Kind: NativeTypeKind.FunctionProto })
            .Select(static pair => (pair.Key, GetTypeName(pair.Key)));
        ValidateNames(records.Concat(enums).Concat(aliases), "types");
    }

    private static string NormalizeName(string native)
    {
        string name = Regex.Replace(native, @"^[pP]+(?=[A-Z_])_?", string.Empty);
        name = Regex.Replace(name, @"^(?:In|in|Out|out)(?=[A-Z_]|$)_?", string.Empty);

        return Regex.Replace(name, @"(?:Params|Cmd|Dev(?=Ctx)|Attrib|Buf|Exts|Ext|Res)(?=[A-Z0-9_]|$)", static match => match.Value switch
        {
            "Params" => "Parameters",
            "Cmd" => "Command",
            "Dev" => "Device",
            "Attrib" => "Attributes",
            "Buf" => "Buffer",
            "Ext" => "Extension",
            "Exts" => "Extensions",
            "Res" => "Resolution",
            _ => match.Value
        });
    }
}
