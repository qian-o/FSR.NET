namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 32)]
public struct FSRCreateContextDescRadianceCache
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public uint Flags;

    [FieldOffset(20)]
    public uint Version;

    [FieldOffset(24)]
    public uint MaxInferenceSampleCount;

    [FieldOffset(28)]
    public uint MaxTrainingSampleCount;

    public const ulong Type = Fsr.APICreateContextDescTypeRadianceCache;

    public FSRCreateContextDescRadianceCache()
    {
        this = default;
        Header.Type = Type;
    }
}
