namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 32)]
public unsafe struct FSRQueryDescUpscaleGetUpscaleRatioFromQualityMode
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public uint QualityMode;

    [FieldOffset(24)]
    public float* UpscaleRatio;

    public const ulong Type = Fsr.APIQueryDescTypeUpscaleGetUpscaleRatioFromQualityMode;

    public FSRQueryDescUpscaleGetUpscaleRatioFromQualityMode()
    {
        this = default;
        Header.Type = Type;
    }
}
