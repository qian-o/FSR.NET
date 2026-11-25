namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public unsafe struct FSRQueryFrameGenerationSwapChainGetGPUMemoryUsageDX12
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiEffectMemoryUsage* GpuMemoryUsageFrameGenerationSwapChain;

    public const ulong Type = Fsr.APIQueryDescTypeFrameGenerationSwapChainGPUMemoryUsageDX12;

    public FSRQueryFrameGenerationSwapChainGetGPUMemoryUsageDX12()
    {
        this = default;
        Header.Type = Type;
    }
}
