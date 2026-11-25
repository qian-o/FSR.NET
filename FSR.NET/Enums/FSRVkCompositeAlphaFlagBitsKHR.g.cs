namespace FSR.NET;

[Flags]
public enum FSRVkCompositeAlphaFlagBitsKHR : int
{
    None = 0,

    OpaqueBitKhr = 1 << 0,

    PreMultipliedBitKhr = 1 << 1,

    PostMultipliedBitKhr = 1 << 2,

    InheritBitKhr = 1 << 3,

    MaxEnumKhr = 0x7FFFFFFF,
}
