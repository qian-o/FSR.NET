namespace FSR.NET.Generator;

internal class AstModel(Dictionary<string, AstRecord> records, Dictionary<string, AstEnum> enums, Dictionary<string, AstFunction> functions, Dictionary<string, AstMacro> macros, Dictionary<string, AstType> aliases)
{
    public readonly Dictionary<string, AstRecord> Records = records;

    public readonly Dictionary<string, AstEnum> Enums = enums;

    public readonly Dictionary<string, AstFunction> Functions = functions;

    public readonly Dictionary<string, AstMacro> Macros = macros;

    public readonly Dictionary<string, AstType> Aliases = aliases;
}
