using System.Numerics;

namespace FSR.NET.Generator;

internal class EnumEmitter(Dictionary<string, string> files)
{
    internal void WriteEnum(string name, AstEnum value)
    {
        string managed = GetTypeName(name);
        (AstEnumValue Item, string Name)[] members = GetEnumMembers(name, value);
        bool isUnsigned = name is "FfxApiReturnCodes" || members.Any(static pair => pair.Item.Value > int.MaxValue);
        bool isFlagsEnum = name.EndsWith("Flags", StringComparison.Ordinal) || name.Contains("FlagBits", StringComparison.Ordinal) || name.EndsWith("_FLAGS", StringComparison.Ordinal) || name is "FfxApiResourceUsage" or "FfxApiResourceState" or "D3D12_RESOURCE_STATES" or "FfxApiQueryResourceIdentifiers";
        bool hasZero = members.Any(static pair => pair.Item.Value is 0);
        bool hasNone = members.Any(static pair => pair.Name is "None");
        CodeWriter text = CreateFile();
        if (isFlagsEnum)
        {
            text.WriteLine("[Flags]");
        }

        text.BeginBlock($"public enum {managed} : {(isUnsigned ? "uint" : "int")}");

        if (isFlagsEnum && !hasZero && !hasNone)
        {
            text.WriteLine($"None = {(isUnsigned ? "0u" : "0")},");
            text.WriteBlankLine();
        }

        foreach ((AstEnumValue item, string member) in members)
        {
            uint bits = unchecked((uint)item.Value);
            string literal = isUnsigned ? $"{bits.ToString(CultureInfo.InvariantCulture)}u" : item.Value.ToString(CultureInfo.InvariantCulture);
            if (isFlagsEnum && bits is not 0)
            {
                int count = BitOperations.PopCount(bits);
                literal = count <= 8
                    ? string.Join(" | ", Enumerable.Range(0, 32).Where(bit => (bits & (1u << bit)) is not 0).Select(bit => count > 1 ? $"({(isUnsigned ? "1u" : "1")} << {bit})" : $"{(isUnsigned ? "1u" : "1")} << {bit}"))
                    : $"0x{bits:X8}{(isUnsigned ? "u" : string.Empty)}";
            }

            text.WriteLine($"{member} = {literal},");
            text.WriteBlankLine();
        }

        text.EndBlock();
        files.Add($"Enums/{managed}.g.cs", text.ToString());
    }
}
