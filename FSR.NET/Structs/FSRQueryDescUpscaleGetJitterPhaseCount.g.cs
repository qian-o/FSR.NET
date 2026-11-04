namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 32)]
public unsafe struct FSRQueryDescUpscaleGetJitterPhaseCount
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public uint RenderWidth;

    [FieldOffset(20)]
    public uint DisplayWidth;

    [FieldOffset(24)]
    public int* PhaseCount;

    public const ulong Type = Fsr.APIQueryDescTypeUpscaleGetJitterPhaseCount;

    public FSRQueryDescUpscaleGetJitterPhaseCount()
    {
        this = default;
        Header.Type = Type;
    }
}
