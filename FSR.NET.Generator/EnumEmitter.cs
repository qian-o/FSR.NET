namespace FSR.NET.Generator;

internal class EnumEmitter(Dictionary<string, string> files)
{
    internal void WriteEnum(string name, AstEnum value)
    {
        string managed = GetTypeName(name);
        bool isUnsigned = name is "FfxApiReturnCodes" || value.Values.Any(static item => item.Value > int.MaxValue);
        bool isFlagsEnum = name.EndsWith("Flags", StringComparison.Ordinal) || name.Contains("FlagBits", StringComparison.Ordinal) || name.EndsWith("_FLAGS", StringComparison.Ordinal) || name is "FfxApiResourceUsage" or "FfxApiResourceState" or "D3D12_RESOURCE_STATES";
        CodeWriter text = CreateFile();
        if (isFlagsEnum)
        {
            text.WriteLine("[Flags]");
        }

        text.BeginBlock($"public enum {managed} : {(isUnsigned ? "uint" : "int")}");
        foreach ((AstEnumValue item, string member) in value.Values.Zip(GetEnumMemberNames(value)))
        {
            string literal = isUnsigned ? $"{unchecked((uint)item.Value).ToString(CultureInfo.InvariantCulture)}u" : item.Value.ToString(CultureInfo.InvariantCulture);
            text.WriteLine($"{member} = {literal},");
            text.WriteBlankLine();
        }

        text.EndBlock();
        files.Add($"Enums/{managed}.g.cs", text.ToString());
    }
}
