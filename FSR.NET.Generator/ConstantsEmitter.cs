namespace FSR.NET.Generator;

internal class ConstantsEmitter(AstModel model, Dictionary<string, string> files)
{
    // C/C++ export annotations, deprecation attributes and compiler diagnostics
    // do not describe runtime API values or callable helpers.
    private static readonly HashSet<string> CompilerMacros =
    [
        "FFX_API_ENTRY",
        "FFX_DEPRECATION",
        "FFX_PRAGMA_WARNING_PUSH",
        "FFX_PRAGMA_WARNING_POP",
        "FFX_PRAGMA_WARNING_DISABLE_DEPRECATIONS",
        "FFX_PRAGMA_WARNING_WARN_DEPRECATIONS"
    ];

    internal void WriteConstants()
    {
        VerifyMacroCoverage();
        ValidateNames(model.Macros.Values
            .Where(static macro => !CompilerMacros.Contains(macro.Name) && !macro.IsFunctionLike)
            .Select(static macro => (macro.Name, GetConstantName(macro.Name))), "constants");
        CodeWriter text = CreateFile();
        text.BeginBlock("public static unsafe partial class Fsr");
        foreach (AstMacro macro in model.Macros.Values.OrderBy(static value => value.Name, StringComparer.Ordinal))
        {
            if (CompilerMacros.Contains(macro.Name) || macro.IsFunctionLike)
            {
                continue;
            }

            if (macro.Value is string value)
            {
                text.WriteLine($"public const ulong {GetConstantName(macro.Name)} = {value}UL;");
                text.WriteBlankLine();
            }
            else if (macro.Tokens.All(static token => token.StartsWith('"')))
            {
                string valueLiteral = string.Concat(macro.Tokens.Select(static token => token[1..^1]));
                text.WriteLine($"public static ReadOnlySpan<byte> {GetConstantName(macro.Name)} => \"{valueLiteral}\\0\"u8;");
                text.WriteBlankLine();
            }
        }

        string effectMask = GetConstantName("FFX_API_EFFECT_MASK");
        string backendMask = GetConstantName("FFX_API_BACKEND_MASK");
        WriteMacro(text, "FFX_API_MAKE_EFFECT_SUB_ID", "uint effectId, uint subversion", $"(effectId & (uint){effectMask}) | (subversion & ~(uint){effectMask})");
        WriteMacro(text, "FFX_API_MAKE_BACKEND_SUB_ID", "uint backendId, uint subversion", $"(backendId & (uint){backendMask}) | (subversion & ~(uint){backendMask})");
        WriteMacro(text, "FFX_API_MAKE_BACKEND_EFFECT_SUB_ID", "uint backendId, uint effectId, uint subversion", $"(subversion & ~(uint){effectMask}) | (effectId & (uint){effectMask}) | (backendId & (uint){backendMask}) | (subversion & ~((uint){backendMask} | (uint){effectMask}))");
        foreach (string name in model.Macros.Keys.Where(static name => name.EndsWith("_MAKE_VERSION", StringComparison.Ordinal)).Order(StringComparer.Ordinal))
        {
            WriteMacro(text, name, "uint major, uint minor, uint patch", "(major << 22) | (minor << 12) | patch");
        }

        text.EndBlock();
        files.Add("API/Constants.g.cs", text.ToString());
    }

    private void VerifyMacroCoverage()
    {
        foreach (AstMacro macro in model.Macros.Values)
        {
            if (CompilerMacros.Contains(macro.Name))
            {
                continue;
            }

            if (macro.IsFunctionLike)
            {
                if (macro.Name is "FFX_API_MAKE_EFFECT_SUB_ID" or "FFX_API_MAKE_BACKEND_SUB_ID" or "FFX_API_MAKE_BACKEND_EFFECT_SUB_ID"
                    || macro.Name.EndsWith("_MAKE_VERSION", StringComparison.Ordinal))
                {
                    continue;
                }
            }
            else if (macro.Tokens.Length is not 0 && (macro.Value is not null || macro.Tokens.All(static token => token.StartsWith('"'))))
            {
                continue;
            }

            throw new InvalidOperationException($"Unmapped SDK macro: {macro.Name}. Review its managed API representation before regenerating bindings.");
        }
    }

    private void WriteMacro(CodeWriter text, string name, string parameters, string expression)
    {
        if (!model.Macros.TryGetValue(name, out AstMacro? macro) || !macro.IsFunctionLike)
        {
            throw new InvalidOperationException($"Missing function macro: {name}.");
        }

        string expected = name switch
        {
            "FFX_API_MAKE_EFFECT_SUB_ID" => "(effectId,subversion)((effectId&FFX_API_EFFECT_MASK)|(subversion&~FFX_API_EFFECT_MASK))",
            "FFX_API_MAKE_BACKEND_SUB_ID" => "(backendId,subversion)((backendId&FFX_API_BACKEND_MASK)|(subversion&~FFX_API_BACKEND_MASK))",
            "FFX_API_MAKE_BACKEND_EFFECT_SUB_ID" => "(backendId,effectId,subversion)((subversion&~FFX_API_EFFECT_MASK)|(effectId&FFX_API_EFFECT_MASK)|(backendId&FFX_API_BACKEND_MASK)|(subversion&~(FFX_API_BACKEND_MASK|FFX_API_EFFECT_MASK)))",
            _ => "(major,minor,patch)(((major)<<22)|((minor)<<12)|(patch))"
        };
        if (string.Concat(macro.Tokens) != expected)
        {
            throw new InvalidOperationException($"Review the changed function macro: {name}.");
        }

        text.BeginBlock($"public static uint {name}({parameters})");
        text.WriteLine($"return {expression};");
        text.EndBlock();
    }
}
