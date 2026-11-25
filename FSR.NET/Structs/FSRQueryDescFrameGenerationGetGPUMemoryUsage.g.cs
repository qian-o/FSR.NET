namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public unsafe struct FSRQueryDescFrameGenerationGetGPUMemoryUsage
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiEffectMemoryUsage* GpuMemoryUsageFrameGeneration;

    public const ulong Type = Fsr.APIQueryDescTypeFrameGenerationGPUMemoryUsage;

    public FSRQueryDescFrameGenerationGetGPUMemoryUsage()
    {
        this = default;
        Header.Type = Type;
    }
}
