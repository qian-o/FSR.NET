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
            string parameters = string.Join(", ", function.Parameters.Select(parameter => $"{mapper.GetManagedType(parameter.Type)} {GetParameterName(parameter.Name)}"));
            text.WriteLine($"[LibraryImport({group}LibraryName, EntryPoint = \"{function.Export}\")]");
            text.WriteLine("[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]");
            text.WriteLine($"public static partial {mapper.GetManagedType(function.Result)} {method}({parameters});");
            text.WriteBlankLine();
        }

        text.BeginBlock($"static {group}()");
        text.WriteLine("NativeLoader.Register();");
        text.EndBlock();
        text.EndBlock();
        text.EndBlock();
        files.Add($"API/{group}.g.cs", text.ToString());
    }
}
