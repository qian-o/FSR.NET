namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public unsafe struct FSRQueryFrameGenerationSwapChainGetGPUMemoryUsageVK
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiEffectMemoryUsage* GpuMemoryUsageFrameGenerationSwapChain;

    public const ulong Type = Fsr.APIQueryDescTypeFrameGenerationSwapChainGPUMemoryUsageVK;

    public FSRQueryFrameGenerationSwapChainGetGPUMemoryUsageVK()
    {
        this = default;
        Header.Type = Type;
    }
}
