namespace FSR.NET.Generator;

internal class FunctionEmitter(TypeMapper mapper, Dictionary<string, string> files)
{
    internal void WriteFunctions(string group, IEnumerable<AstFunction> source)
    {
        CodeWriter text = CreateFile();
        text.BeginBlock("public static unsafe partial class Fsr");
        text.BeginBlock($"public static partial class {group}");
        foreach (AstFunction function in source.OrderBy(static function => function.Name, StringComparer.Ordinal))
        {
            ValidateNames(function.Parameters.Select(static parameter => (parameter.Name, GetParameterName(parameter.Name))), $"parameters in {function.Name}");
            string method = GetFunctionName(function.Name);
            string parameters = string.Join(", ", function.Parameters.Select(parameter =>
            {
                string declaration = $"{mapper.GetManagedType(parameter.Type)} {GetParameterName(parameter.Name)}";

                return function.Name is "ffxCreateContext" or "ffxDestroyContext" && parameter.Name is "memCb"
                    ? $"{declaration} = null"
                    : declaration;
            }));
            text.WriteLine($"[LibraryImport({group}LibraryName, EntryPoint = \"{function.Export}\")]");
            text.WriteLine("[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]");
            text.WriteLine($"public static partial {mapper.GetManagedType(function.Result)} {method}({parameters});");
            text.WriteBlankLine();
        }

        text.BeginBlock($"static {group}()");
        text.WriteLine("NativeLoader.Register();");
        text.EndBlock();

        WriteOverloads(text);

        text.EndBlock();
        text.EndBlock();
        files.Add($"API/{group}.g.cs", text.ToString());
    }

    private static void WriteOverloads(CodeWriter text)
    {
        text.BeginBlock("public static FSRApiReturnCodes Configure(ref FSRContext context, in FSRApiHeader desc)");
        text.BeginBlock("fixed (FSRContext* pContext = &context)");
        text.BeginBlock("fixed (FSRApiHeader* pDesc = &desc)");
        text.WriteLine("return Configure(pContext, pDesc);");
        text.EndBlock();
        text.EndBlock();
        text.EndBlock();

        text.BeginBlock("public static FSRApiReturnCodes Configure(in FSRApiHeader desc)");
        text.BeginBlock("fixed (FSRApiHeader* pDesc = &desc)");
        text.WriteLine("return Configure(null, pDesc);");
        text.EndBlock();
        text.EndBlock();

        text.BeginBlock("public static FSRApiReturnCodes CreateContext(out FSRContext context, ref FSRApiHeader desc, FSRAllocationCallbacks* memCb = null)");
        text.BeginBlock("fixed (FSRContext* pContext = &context)");
        text.BeginBlock("fixed (FSRApiHeader* pDesc = &desc)");
        text.WriteLine("return CreateContext(pContext, pDesc, memCb);");
        text.EndBlock();
        text.EndBlock();
        text.EndBlock();

        text.BeginBlock("public static FSRApiReturnCodes DestroyContext(ref FSRContext context, FSRAllocationCallbacks* memCb = null)");
        text.BeginBlock("fixed (FSRContext* pContext = &context)");
        text.WriteLine("return DestroyContext(pContext, memCb);");
        text.EndBlock();
        text.EndBlock();

        text.BeginBlock("public static FSRApiReturnCodes Dispatch(ref FSRContext context, in FSRApiHeader desc)");
        text.BeginBlock("fixed (FSRContext* pContext = &context)");
        text.BeginBlock("fixed (FSRApiHeader* pDesc = &desc)");
        text.WriteLine("return Dispatch(pContext, pDesc);");
        text.EndBlock();
        text.EndBlock();
        text.EndBlock();

        text.BeginBlock("public static FSRApiReturnCodes Query(ref FSRContext context, ref FSRApiHeader desc)");
        text.BeginBlock("fixed (FSRContext* pContext = &context)");
        text.BeginBlock("fixed (FSRApiHeader* pDesc = &desc)");
        text.WriteLine("return Query(pContext, pDesc);");
        text.EndBlock();
        text.EndBlock();
        text.EndBlock();

        text.BeginBlock("public static FSRApiReturnCodes Query(ref FSRApiHeader desc)");
        text.BeginBlock("fixed (FSRApiHeader* pDesc = &desc)");
        text.WriteLine("return Query(null, pDesc);");
        text.EndBlock();
        text.EndBlock();
    }
}
