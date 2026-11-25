namespace FSR.NET.Generator;

internal class AstParameter(string name, AstType type)
{
    public readonly string Name = name;

    public readonly AstType Type = type;
}
