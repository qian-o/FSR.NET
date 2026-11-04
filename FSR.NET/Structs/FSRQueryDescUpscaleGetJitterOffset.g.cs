namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 40)]
public unsafe struct FSRQueryDescUpscaleGetJitterOffset
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public int Index;

    [FieldOffset(20)]
    public int PhaseCount;

    [FieldOffset(24)]
    public float* X;

    [FieldOffset(32)]
    public float* Y;

    public const ulong Type = Fsr.APIQueryDescTypeUpscaleGetJitterOffset;

    public FSRQueryDescUpscaleGetJitterOffset()
    {
        this = default;
        Header.Type = Type;
    }
}
