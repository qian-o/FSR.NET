namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 64)]
public unsafe struct FSRQueryFrameGenerationSwapChainGetGPUMemoryUsageDX12V2
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public void* Device;

    [FieldOffset(24)]
    public FSRApiDimensions2D DisplaySize;

    [FieldOffset(32)]
    public uint BackBufferFormat;

    [FieldOffset(36)]
    public uint BackBufferCount;

    [FieldOffset(40)]
    public FSRApiDimensions2D UiResourceSize;

    [FieldOffset(48)]
    public uint UiResourceFormat;

    [FieldOffset(52)]
    public uint Flags;

    [FieldOffset(56)]
    public FSRApiEffectMemoryUsage* GpuMemoryUsageFrameGenerationSwapChain;

    public const ulong Type = Fsr.APIQueryDescTypeFrameGenerationSwapChainGPUMemoryUsageDX12V2;

    public FSRQueryFrameGenerationSwapChainGetGPUMemoryUsageDX12V2()
    {
        this = default;
        Header.Type = Type;
    }
}
