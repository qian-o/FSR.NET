namespace FSR.NET.Generator;

internal class AstMacro(string name, string[] tokens, bool isFunctionLike, string? value)
{
    public readonly string Name = name;

    public readonly string[] Tokens = tokens;

    public readonly bool IsFunctionLike = isFunctionLike;

    public readonly string? Value = value;
}
