using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace FSR.NET.Validation;

internal static class VerifyBindings
{
    private static readonly Assembly Assembly = typeof(Fsr).Assembly;
    private static readonly Dictionary<string, Type> Types = Assembly.GetExportedTypes()
        .Where(static type => type.Namespace == "FSR.NET" && !type.IsNested)
        .ToDictionary(static type => Normalize(type.Name[3..]), StringComparer.Ordinal);
    private static readonly Dictionary<string, FieldInfo> Constants = typeof(Fsr)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Where(static field => field.IsLiteral)
        .ToDictionary(static field => Normalize(field.Name), StringComparer.Ordinal);

    private static Dictionary<string, JsonElement> records = [];
    private static int checks;

    private static int Main(string[] args)
    {
        try
        {
            using JsonDocument document = JsonDocument.Parse(File.ReadAllText(args[0]));
            JsonElement platform = document.RootElement.GetProperty("platforms").GetProperty("win-x64");
            records = platform.GetProperty("records").EnumerateArray().ToDictionary(static value => value.GetProperty("name").GetString()!);
            (int recordCount, int fieldCount, int descriptorCount) = VerifyRecords();
            int enumCount = VerifyEnums(platform);
            (int macroCount, int stringCount, int functionMacroCount) = VerifyConstants(platform);
            VerifyImports(platform);
            int callbacks = VerifyCallbacks(platform);
            int helpers = VerifyHelperSignatures(platform);

            VerifyHelpers();
            Console.WriteLine($"Verified {recordCount} record layouts, {fieldCount} fields, {descriptorCount} descriptor defaults, {enumCount} enums, {macroCount} numeric/{stringCount} string/{functionMacroCount} function macros, {callbacks} callbacks, {helpers} inline helper signatures and 10 loader imports ({checks} checks). No native DLL was loaded.");

            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception);

            return 1;
        }
    }

    private static (int Records, int Fields, int Descriptors) VerifyRecords()
    {
        int recordCount = 0;
        int fieldCount = 0;
        int descriptorCount = 0;
        foreach ((string name, JsonElement record) in records)
        {
            if (record.GetProperty("opaque").GetBoolean())
            {
                continue;
            }

            Type type = ManagedType(name);
            Equal(SizeOf(type), record.GetProperty("size").GetInt32(), name + " sizeof");
            Equal(AlignmentOf(type), record.GetProperty("alignment").GetInt32(), name + " alignment");
            Equal(type.GetFields(BindingFlags.Public | BindingFlags.Instance).Length, record.GetProperty("fields").GetArrayLength(), name + " field count");
            foreach (JsonElement field in record.GetProperty("fields").EnumerateArray())
            {
                string nativeField = field.GetProperty("name").GetString()!;
                FieldInfo info = ManagedField(type, nativeField);
                string fieldName = info.Name;
                Equal(Marshal.OffsetOf(type, fieldName).ToInt64() * 8, field.GetProperty("offset").GetInt64(), name + "." + fieldName + " offset");
                CheckType(field.GetProperty("type"), info.FieldType, name + "." + fieldName, true);
                fieldCount++;
            }

            if (record.TryGetProperty("descriptorType", out JsonElement descriptor))
            {
                object value = Activator.CreateInstance(type)!;
                FSRApiHeader header = (FSRApiHeader)type.GetField("Header")!.GetValue(value)!;
                ulong expected = ConstantValue(descriptor.GetString()!);
                Equal(header.Type, expected, name + " default header type");
                unsafe
                {
                    Equal((nuint)header.Next, 0UL, name + " default pNext");
                }

                descriptorCount++;
            }

            recordCount++;
        }

        return (recordCount, fieldCount, descriptorCount);
    }

    private static int VerifyEnums(JsonElement platform)
    {
        int enumCount = 0;
        foreach (JsonElement native in platform.GetProperty("enums").EnumerateArray())
        {
            string name = native.GetProperty("name").GetString()!;
            Type type = ManagedType(name);
            Equal(SizeOf(type), native.GetProperty("size").GetInt32(), name + " enum size");
            FieldInfo[] members = [.. type.GetFields(BindingFlags.Public | BindingFlags.Static).OrderBy(static field => field.MetadataToken)];
            JsonElement[] values = [.. native.GetProperty("values").EnumerateArray()];
            Equal(members.Length, values.Length, name + " member count");
            for (int i = 0; i < values.Length; i++)
            {
                Equal(unchecked((uint)Convert.ToInt64(members[i].GetRawConstantValue())), unchecked((uint)values[i].GetProperty("value").GetInt64()), name + "." + members[i].Name);
            }

            enumCount++;
        }

        return enumCount;
    }

    private static (int Numeric, int Strings, int Functions) VerifyConstants(JsonElement platform)
    {
        int macroCount = 0;
        int stringCount = 0;
        int functionCount = 0;
        foreach (JsonElement macro in platform.GetProperty("macros").EnumerateArray())
        {
            string name = macro.GetProperty("name").GetString()!;
            if (macro.TryGetProperty("value", out JsonElement value))
            {
                Equal(ConstantValue(name), value.GetUInt64(), name);
                macroCount++;

                continue;
            }

            if (name is "FFX_API_ENTRY" or "FFX_DEPRECATION" or "FFX_PRAGMA_WARNING_PUSH" or "FFX_PRAGMA_WARNING_POP" or "FFX_PRAGMA_WARNING_DISABLE_DEPRECATIONS" or "FFX_PRAGMA_WARNING_WARN_DEPRECATIONS")
            {
                continue;
            }

            string[] tokens = [.. macro.GetProperty("tokens").EnumerateArray().Select(static token => token.GetString()!)];
            if (macro.GetProperty("functionLike").GetBoolean())
            {
                VerifyFunctionMacro(name, tokens, platform);
                functionCount++;

                continue;
            }

            Require(tokens.Length > 0 && tokens.All(static token => token.StartsWith('"') && token.EndsWith('"')), name + " supported string constant");
            string expected = string.Concat(tokens.Select(static token => JsonSerializer.Deserialize<string>(token))) + '\0';
            PropertyInfo property = typeof(Fsr).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Single(property => Normalize(property.Name) == Normalize(name[4..]));
            Require(property.PropertyType == typeof(ReadOnlySpan<byte>), name + " UTF-8 property type");
            Utf8ConstantGetter getter = property.GetMethod!.CreateDelegate<Utf8ConstantGetter>();
            Require(getter().SequenceEqual(Encoding.UTF8.GetBytes(expected)), name + " UTF-8 bytes and terminator");
            stringCount++;
        }

        return (macroCount, stringCount, functionCount);
    }

    private static void VerifyFunctionMacro(string name, string[] tokens, JsonElement platform)
    {
        int parameterCount = tokens.TakeWhile(static token => token != ")").Count(static token => token is not ("(" or ","));
        MethodInfo method = typeof(Fsr).GetMethod(name, BindingFlags.Public | BindingFlags.Static)!;
        Require(method is not null, name + " callable macro");
        Require(method!.ReturnType == typeof(uint), name + " return type");
        Require(method.GetCustomAttribute<LibraryImportAttribute>() is null && method.GetCustomAttribute<DllImportAttribute>() is null, name + " managed implementation");
        Require(method.GetParameters().Select(static parameter => parameter.ParameterType).SequenceEqual(Enumerable.Repeat(typeof(uint), parameterCount)), name + " parameter types");
        Dictionary<string, uint> constants = platform.GetProperty("macros").EnumerateArray()
            .Where(static macro => macro.TryGetProperty("value", out _))
            .ToDictionary(static macro => macro.GetProperty("name").GetString()!, static macro => unchecked((uint)macro.GetProperty("value").GetUInt64()));
        uint[][] cases = [[0, 0, 0], [1, 2, 3], [uint.MaxValue, uint.MaxValue, uint.MaxValue], [0x12345678, 0x87654321, 0x5A5AA5A5]];
        foreach (uint[] arguments in cases)
        {
            uint expected;
            if (name.EndsWith("_MAKE_VERSION", StringComparison.Ordinal))
            {
                Equal(parameterCount, 3, name + " version parameter count");
                expected = unchecked((uint)(((ulong)arguments[0] * (1UL << 22)) | ((ulong)arguments[1] * (1UL << 12)) | arguments[2]));
            }
            else
            {
                uint mask = name == "FFX_API_MAKE_BACKEND_SUB_ID" ? constants["FFX_API_BACKEND_MASK"] : constants["FFX_API_EFFECT_MASK"];
                bool combined = name == "FFX_API_MAKE_BACKEND_EFFECT_SUB_ID";
                Require(combined || name is "FFX_API_MAKE_BACKEND_SUB_ID" or "FFX_API_MAKE_EFFECT_SUB_ID", name + " reviewed ID macro");
                Equal(parameterCount, combined ? 3 : 2, name + " ID parameter count");
                expected = 0;
                for (int bit = 0; bit < 32; bit++)
                {
                    uint flag = 1U << bit;
                    uint selected = (mask & flag) != 0 ? arguments[combined ? 1 : 0] : arguments[combined ? 2 : 1];
                    if (combined && (constants["FFX_API_BACKEND_MASK"] & flag) != 0)
                    {
                        selected |= arguments[0];
                    }

                    expected |= selected & flag;
                }
            }

            object[] values = [.. arguments.Take(parameterCount).Select(static value => (object)value)];
            Equal((uint)method.Invoke(null, values)!, expected, name + " value/overflow behavior");
        }
    }

    private static void VerifyImports(JsonElement platform)
    {
        JsonElement[] functions = [.. platform.GetProperty("functions").EnumerateArray()];
        Equal(functions.Length, 5, "public exported functions");
        foreach ((Type api, string library) in new[] { (typeof(Fsr.D3D12), "amd_fidelityfx_loader_dx12"), (typeof(Fsr.Vulkan), "amd_fidelityfx_loader_vk") })
        {
            MethodInfo[] imports = [.. api.GetMethods(BindingFlags.Public | BindingFlags.Static).Where(static method => method.GetCustomAttribute<LibraryImportAttribute>() is not null)];
            Equal(imports.Length, 5, api.Name + " import count");
            foreach (JsonElement native in functions)
            {
                string entry = native.GetProperty("export").GetString()!;
                MethodInfo method = imports.Single(method => method.GetCustomAttribute<LibraryImportAttribute>()!.EntryPoint == entry);
                Require(method.GetCustomAttribute<LibraryImportAttribute>()!.LibraryName == library, api.Name + "." + entry + " library");
                Require(method.GetCustomAttribute<UnmanagedCallConvAttribute>()!.CallConvs!.SequenceEqual([typeof(CallConvCdecl)]), api.Name + "." + entry + " calling convention");
                CheckType(native.GetProperty("result"), method.ReturnType, entry + " result");
                JsonElement[] parameters = [.. native.GetProperty("parameters").EnumerateArray()];
                Equal(method.GetParameters().Length, parameters.Length, entry + " parameter count");
                for (int i = 0; i < parameters.Length; i++)
                {
                    CheckType(parameters[i].GetProperty("type"), method.GetParameters()[i].ParameterType, entry + " parameter " + i);
                }
            }
        }
    }

    private static int VerifyCallbacks(JsonElement platform)
    {
        int callbacks = 0;
        foreach (JsonProperty alias in platform.GetProperty("aliases").EnumerateObject())
        {
            if (alias.Value.GetProperty("kind").GetString() != "POINTER" || alias.Value.GetProperty("element").GetProperty("kind").GetString() != "FUNCTIONPROTO")
            {
                continue;
            }

            Type type = ManagedType(alias.Name);
            JsonElement function = alias.Value.GetProperty("element");
            MethodInfo method = type.GetMethod("Invoke")!;
            CheckType(function.GetProperty("result"), method.ReturnType, alias.Name + " result");
            JsonElement[] arguments = [.. function.GetProperty("arguments").EnumerateArray()];
            Equal(method.GetParameters().Length, arguments.Length, alias.Name + " argument count");
            for (int i = 0; i < arguments.Length; i++)
            {
                CheckType(arguments[i], method.GetParameters()[i].ParameterType, alias.Name + " argument " + i);
            }

            CallingConvention expected = function.GetProperty("callingConvention").GetString() == "Stdcall" ? CallingConvention.StdCall : CallingConvention.Cdecl;
            Require(type.GetCustomAttribute<UnmanagedFunctionPointerAttribute>()!.CallingConvention == expected, alias.Name + " delegate calling convention");
            callbacks++;
        }

        return callbacks;
    }

    private static unsafe int VerifyHelperSignatures(JsonElement platform)
    {
        // The managed adaptations use nint handles and typed flags. GetResourceDX12
        // receives GetDesc's result from its caller; LoadFunctions returns the table.
        (string Native, Type Api, string Managed, Type Result, Type[] Parameters)[] expected =
        [
            ("ffxApiGetSurfaceFormatDX12", typeof(Fsr.D3D12), "GetSurfaceFormat", typeof(FSRApiSurfaceFormat), [typeof(FSRDxgiFormat)]),
            ("ffxApiGetResourceDX12", typeof(Fsr.D3D12), "GetResource", typeof(FSRApiResource), [typeof(nint), typeof(FSRD3D12ResourceDesc), typeof(FSRApiResourceState), typeof(FSRApiResourceUsage)]),
            ("ffxApiGetSurfaceFormatVK", typeof(Fsr.Vulkan), "GetSurfaceFormat", typeof(FSRApiSurfaceFormat), [typeof(FSRVkFormat)]),
            ("ffxApiGetSurfaceFormatToGamma", typeof(Fsr.Vulkan), "GetSurfaceFormatToGamma", typeof(FSRApiSurfaceFormat), [typeof(FSRApiSurfaceFormat)]),
            ("ffxApiIsDepthFormat", typeof(Fsr.Vulkan), "IsDepthFormat", typeof(bool), [typeof(FSRVkFormat)]),
            ("ffxApiIsStencilFormat", typeof(Fsr.Vulkan), "IsStencilFormat", typeof(bool), [typeof(FSRVkFormat)]),
            ("ffxApiGetBufferResourceDescriptionVK", typeof(Fsr.Vulkan), "GetBufferResourceDescription", typeof(FSRApiResourceDescription), [typeof(nint), typeof(FSRVkBufferCreateInfo), typeof(FSRApiResourceUsage)]),
            ("ffxApiGetImageResourceDescriptionVK", typeof(Fsr.Vulkan), "GetImageResourceDescription", typeof(FSRApiResourceDescription), [typeof(nint), typeof(FSRVkImageCreateInfo), typeof(FSRApiResourceUsage)]),
            ("ffxApiGetResourceVK", typeof(Fsr.Vulkan), "GetResource", typeof(FSRApiResource), [typeof(nint), typeof(FSRApiResourceDescription), typeof(FSRApiResourceState)]),
            ("ffxConvertToMatrix4x4", typeof(Fsr), "ConvertToMatrix4x4", typeof(FSRApiMatrix4x4), [typeof(float*), typeof(FSRApiMatrixLayout), typeof(FSRApiMatrixVectorConvention)]),
            ("ffxLoadFunctions", typeof(Fsr), "LoadFunctions", typeof(FSRFunctions), [typeof(nint)])
        ];
        Dictionary<string, JsonElement> helpers = platform.GetProperty("helpers").EnumerateArray()
            .ToDictionary(static helper => helper.GetProperty("name").GetString()!);
        Require(helpers.Keys.ToHashSet(StringComparer.Ordinal).SetEquals(expected.Select(static helper => helper.Native)), "complete reviewed inline helper set");
        foreach ((string native, Type api, string managed, Type result, Type[] parameters) in expected)
        {
            MethodInfo method = api.GetMethods(BindingFlags.Public | BindingFlags.Static).Single(method => method.Name == managed);
            Require(method.ReturnType == result, native + " managed result");
            Require(method.GetParameters().Select(static parameter => parameter.ParameterType).SequenceEqual(parameters), native + " managed parameters");
            Require(method.GetCustomAttribute<LibraryImportAttribute>() is null && method.GetCustomAttribute<DllImportAttribute>() is null, native + " is a managed inline helper, not a DLL import");
            int adaptation = native switch { "ffxLoadFunctions" => 1, "ffxApiGetResourceDX12" => -1, _ => 0 };
            Equal(helpers[native].GetProperty("parameters").GetArrayLength(), parameters.Length + adaptation, native + " native/managed parameter adaptation");
        }

        return expected.Length;
    }

    private static unsafe void VerifyHelpers()
    {
        float* source = stackalloc float[16];
        for (int i = 0; i < 16; i++)
        {
            source[i] = i + 1;
        }

        foreach (FSRApiMatrixLayout layout in Enum.GetValues<FSRApiMatrixLayout>())
        {
            foreach (FSRApiMatrixVectorConvention convention in Enum.GetValues<FSRApiMatrixVectorConvention>())
            {
                FSRApiMatrix4x4 matrix = Fsr.ConvertToMatrix4x4(source, layout, convention);
                float* values = (float*)&matrix;
                bool transpose = (int)layout != (int)convention;
                for (int row = 0; row < 4; row++)
                {
                    for (int column = 0; column < 4; column++)
                    {
                        Require(values[(row * 4) + column] == source[transpose ? (column * 4) + row : (row * 4) + column], "matrix layout/convention conversion");
                    }
                }
            }
        }

        Require(Fsr.LoadFunctions(0).CreateContext == null, "null loader module table");
        FSRD3D12ResourceDesc d3d = new()
        {
            Dimension = FSRD3D12ResourceDimension.Texture2D,
            Width = 64,
            Height = 32,
            DepthOrArraySize = 6,
            MipLevels = 2,
            Format = FSRDxgiFormat.D32Float,
            Flags = FSRD3D12ResourceFlags.AllowUnorderedAccess
        };
        FSRApiResource resource = Fsr.D3D12.GetResource(1, d3d);
        Equal(resource.Description.Type, (uint)FSRApiResourceType.TextureCube, "D3D12 six-slice resource mapping");
        Equal(resource.Description.Usage, (uint)(FSRApiResourceUsage.DepthTarget | FSRApiResourceUsage.Uav), "D3D12 depth/UAV resource usage");
        Equal(resource.Description.MipCount, 2, "D3D12 mip count");
        Equal(Fsr.D3D12.GetResource(0, d3d).Description.Width, 0, "D3D12 null resource description");
        FSRVkBufferCreateInfo buffer = new() { Size = 512, Usage = (uint)(FSRVkBufferUsageFlagBits.StorageBufferBit | FSRVkBufferUsageFlagBits.IndirectBufferBit) };
        FSRApiResourceDescription description = Fsr.Vulkan.GetBufferResourceDescription(1, buffer);
        Equal(description.Usage, (uint)(FSRApiResourceUsage.Uav | FSRApiResourceUsage.Indirect), "Vulkan buffer usage");
        Equal(description.Size, 512, "Vulkan buffer size");
        FSRVkImageCreateInfo image = new()
        {
            ImageType = FSRVkImageType._2D,
            Format = FSRVkFormat.R8G8B8A8Unorm,
            Extent = new() { Width = 64, Height = 32, Depth = 1 },
            ArrayLayers = 6,
            MipLevels = 3,
            Flags = (uint)(FSRVkImageCreateFlagBits.CubeCompatibleBit | FSRVkImageCreateFlagBits.MutableFormatBit),
            Usage = (uint)FSRVkImageUsageFlagBits.StorageBit
        };
        description = Fsr.Vulkan.GetImageResourceDescription(1, image);
        Equal(description.Type, (uint)FSRApiResourceType.TextureCube, "Vulkan cube mapping");
        Equal(description.Format, (uint)FSRApiSurfaceFormat.R8G8B8A8Srgb, "Vulkan mutable sRGB mapping");
        Equal(description.MipCount, 3, "Vulkan mip count");
        Equal(Fsr.Vulkan.GetImageResourceDescription(1, image, FSRApiResourceUsage.ArrayView).Type, (uint)FSRApiResourceType.Texture2D, "Vulkan array-view mapping");
        Require(Fsr.Vulkan.IsDepthFormat(FSRVkFormat.D32SfloatS8Uint) && Fsr.Vulkan.IsStencilFormat(FSRVkFormat.D32SfloatS8Uint), "Vulkan depth/stencil classification");
        Equal((uint)Fsr.D3D12.GetSurfaceFormat((FSRDxgiFormat)(-1)), (uint)FSRApiSurfaceFormat.Unknown, "D3D12 unknown format");
        Equal((uint)Fsr.Vulkan.GetSurfaceFormat((FSRVkFormat)(-1)), (uint)FSRApiSurfaceFormat.Unknown, "Vulkan unknown format");
        Console.WriteLine("Verified managed matrix, loader-table, format and resource helper behavior without a GPU.");
    }

    private static string Normalize(string value)
    {
        return string.Concat(value.Where(char.IsLetterOrDigit)).ToLowerInvariant();
    }

    private static ulong ConstantValue(string native)
    {
        string name = native.StartsWith("FFX_", StringComparison.Ordinal) ? native[4..] : native;

        return Convert.ToUInt64(Constants[Normalize(name)].GetRawConstantValue());
    }

    private static Type ManagedType(string name)
    {
        if (name == "Anonymous_ffx_radiancecache_95_5")
        {
            return Types[Normalize("DispatchDescRadianceCacheOverrides")];
        }

        string suffix = name;
        foreach (string prefix in new[] { "PfnFfx", "PFN_", "ffx" })
        {
            if (name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                suffix = name[prefix.Length..];

                break;
            }
        }

        return Types[Normalize(suffix)];
    }

    private static FieldInfo ManagedField(Type type, string native)
    {
        string name = Regex.Replace(native, @"^[pP]+(?=[A-Z_])_?", string.Empty);
        name = Regex.Replace(name, @"^(?:In|in|Out|out)(?=[A-Z_]|$)_?", string.Empty);
        string key = Normalize(name);

        return type.GetFields(BindingFlags.Public | BindingFlags.Instance)
            .SingleOrDefault(field => Normalize(field.Name) == key)
            ?? throw new InvalidOperationException(type.Name + "." + native + " missing");
    }

    private static unsafe void CheckType(JsonElement native, Type actual, string label, bool field = false)
    {
        string cpp = native.GetProperty("cpp").GetString()!.Replace("const ", "", StringComparison.Ordinal).Replace(" ", "", StringComparison.Ordinal);
        if (cpp == "ffxContext")
        {
            Require(actual == typeof(FSRContext), label);

            return;
        }

        if (cpp == "ffxContext*")
        {
            Require(actual == typeof(FSRContext*), label);

            return;
        }

        if (cpp == "ffxReturnCode_t")
        {
            Require(actual == typeof(FSRApiReturnCodes), label);

            return;
        }

        if (cpp == "size_t")
        {
            Require(actual == typeof(nuint), label);

            return;
        }

        string kind = native.GetProperty("kind").GetString()!;
        int size = native.GetProperty("size").GetInt32();
        if (kind == "POINTER")
        {
            JsonElement element = native.GetProperty("element");
            string elementKind = element.GetProperty("kind").GetString()!;
            if (elementKind is "FUNCTIONPROTO" or "FUNCTIONNOPROTO")
            {
                Require(actual.IsFunctionPointer && actual.IsUnmanagedFunctionPointer, label + " function pointer");
                JsonElement[] arguments = [.. element.GetProperty("arguments").EnumerateArray()];
                Type[] parameters = actual.GetFunctionPointerParameterTypes();
                Equal(parameters.Length, arguments.Length, label + " argument count");
                for (int i = 0; i < arguments.Length; i++)
                {
                    CheckType(arguments[i], parameters[i], label + " argument " + i);
                }

                CheckType(element.GetProperty("result"), actual.GetFunctionPointerReturnType(), label + " result");
                // Reflection erases platform calling convention modifiers for function pointers.
                // verify_fsr.py checks Cdecl/Stdcall spellings in generated source separately.
                return;
            }

            if (elementKind == "RECORD" && records[element.GetProperty("name").GetString()!].GetProperty("opaque").GetBoolean())
            {
                Require(actual == typeof(nint), label + " opaque pointer");

                return;
            }

            Require(actual.IsPointer, label + " pointer");
            CheckType(element, actual.GetElementType()!, label + " element");

            return;
        }

        if (kind is "RECORD" or "ENUM")
        {
            Require(actual == ManagedType(native.GetProperty("name").GetString()!), label);

            return;
        }

        if (kind == "CONSTANTARRAY")
        {
            Equal(actual.GetCustomAttribute<InlineArrayAttribute>()!.Length, native.GetProperty("count").GetInt32(), label + " array count");
            CheckType(native.GetProperty("element"), actual.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Single().FieldType, label + " array element");
            Equal(SizeOf(actual), size, label + " array size");

            return;
        }

        Type expected = kind switch
        {
            "VOID" => typeof(void),
            "BOOL" => field ? typeof(Bool8) : typeof(byte),
            "CHAR_S" or "CHAR_U" or "UCHAR" => typeof(byte),
            "SCHAR" => typeof(sbyte),
            "SHORT" => typeof(short),
            "USHORT" => typeof(ushort),
            "INT" => typeof(int),
            "UINT" => typeof(uint),
            "LONG" => size == 8 ? typeof(long) : typeof(int),
            "ULONG" => size == 8 ? typeof(ulong) : typeof(uint),
            "LONGLONG" => typeof(long),
            "ULONGLONG" => typeof(ulong),
            "FLOAT" => typeof(float),
            "DOUBLE" => typeof(double),
            "WCHAR" => typeof(char),
            _ => throw new InvalidOperationException(label + " unsupported type " + kind)
        };
        Require(actual == expected, label + " expected " + expected + " got " + actual);
    }

    private static int SizeOf(Type type)
    {
        return (int)typeof(VerifyBindings).GetMethod(nameof(GenericSize), BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(type).Invoke(null, null)!;
    }

    private static int AlignmentOf(Type type)
    {
        return (int)typeof(VerifyBindings).GetMethod(nameof(GenericAlignment), BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(type).Invoke(null, null)!;
    }

    private static int GenericSize<T>()
    {
        return Unsafe.SizeOf<T>();
    }

    private static int GenericAlignment<T>() where T : unmanaged
    {
        AlignmentProbe<T> probe = default;

        return (int)Unsafe.ByteOffset(ref probe.Prefix, ref Unsafe.As<T, byte>(ref probe.Value));
    }

    private static void Equal(long actual, long expected, string label)
    {
        Require(actual == expected, $"{label}: {actual} != {expected}");
    }

    private static void Equal(ulong actual, ulong expected, string label)
    {
        Require(actual == expected, $"{label}: {actual} != {expected}");
    }

    private static void Require(bool success, string label)
    {
        checks++;
        if (!success)
        {
            throw new InvalidOperationException(label);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct AlignmentProbe<T> where T : unmanaged
    {
        public byte Prefix;
        public T Value;
    }

    private delegate ReadOnlySpan<byte> Utf8ConstantGetter();
}
