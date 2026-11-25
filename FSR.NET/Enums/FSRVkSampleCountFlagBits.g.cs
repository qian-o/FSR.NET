namespace FSR.NET;

[Flags]
public enum FSRVkSampleCountFlagBits : int
{
    None = 0,

    _1Bit = 1 << 0,

    _2Bit = 1 << 1,

    _4Bit = 1 << 2,

    _8Bit = 1 << 3,

    _16Bit = 1 << 4,

    _32Bit = 1 << 5,

    _64Bit = 1 << 6,

    MaxEnum = 0x7FFFFFFF,
}
