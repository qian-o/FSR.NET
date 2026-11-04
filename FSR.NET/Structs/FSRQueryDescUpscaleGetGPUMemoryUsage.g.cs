namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public unsafe struct FSRQueryDescUpscaleGetGPUMemoryUsage
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiEffectMemoryUsage* GpuMemoryUsageUpscaler;

    public const ulong Type = Fsr.APIQueryDescTypeUpscaleGPUMemoryUsage;

    public FSRQueryDescUpscaleGetGPUMemoryUsage()
    {
        this = default;
        Header.Type = Type;
    }
}
