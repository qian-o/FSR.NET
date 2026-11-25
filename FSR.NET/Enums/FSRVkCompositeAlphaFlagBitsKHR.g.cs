namespace FSR.NET;

[Flags]
public enum FSRVkCompositeAlphaFlagBitsKHR : int
{
    OpaqueBitKhr = 1 << 0,

    PreMultipliedBitKhr = 1 << 1,

    PostMultipliedBitKhr = 1 << 2,

    InheritBitKhr = 1 << 3,

    FlagBitsMaxEnumKhr = 0x7FFFFFFF,

    None = 0
}
