namespace FSR.NET;

[Flags]
public enum FSRVkSampleCountFlagBits : int
{
    _1Bit = 1 << 0,

    _2Bit = 1 << 1,

    _4Bit = 1 << 2,

    _8Bit = 1 << 3,

    _16Bit = 1 << 4,

    _32Bit = 1 << 5,

    _64Bit = 1 << 6,

    FlagBitsMaxEnum = 0x7FFFFFFF,

    None = 0
}
