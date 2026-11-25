namespace FSR.NET.Generator;

internal class AstRecord(bool isUnion, bool isOpaque, int sizeInBytes, AstField[] fields, string? descriptorType)
{
    public readonly bool IsUnion = isUnion;

    public readonly bool IsOpaque = isOpaque;

    public readonly int SizeInBytes = sizeInBytes;

    public readonly AstField[] Fields = fields;

    public readonly string? DescriptorType = descriptorType;
}
