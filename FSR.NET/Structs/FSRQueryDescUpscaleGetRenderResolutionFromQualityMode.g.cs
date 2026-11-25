namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 48)]
public unsafe struct FSRQueryDescUpscaleGetRenderResolutionFromQualityMode
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public uint DisplayWidth;

    [FieldOffset(20)]
    public uint DisplayHeight;

    [FieldOffset(24)]
    public uint QualityMode;

    [FieldOffset(32)]
    public uint* RenderWidth;

    [FieldOffset(40)]
    public uint* RenderHeight;

    public const ulong Type = Fsr.APIQueryDescTypeUpscaleGetRenderResolutionFromQualityMode;

    public FSRQueryDescUpscaleGetRenderResolutionFromQualityMode()
    {
        this = default;
        Header.Type = Type;
    }
}
