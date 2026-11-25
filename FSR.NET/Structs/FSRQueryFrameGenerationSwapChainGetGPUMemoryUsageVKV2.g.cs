namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 80)]
public unsafe struct FSRQueryFrameGenerationSwapChainGetGPUMemoryUsageVKV2
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public nint Device;

    [FieldOffset(24)]
    public nint PhysicalDevice;

    [FieldOffset(32)]
    public FSRApiDimensions2D DisplaySize;

    [FieldOffset(40)]
    public uint BackBufferFormat;

    [FieldOffset(44)]
    public uint BackBufferCount;

    [FieldOffset(48)]
    public FSRApiDimensions2D UiResourceSize;

    [FieldOffset(56)]
    public uint UiResourceFormat;

    [FieldOffset(60)]
    public uint Flags;

    [FieldOffset(64)]
    public uint ComposeOnGameQueue;

    [FieldOffset(68)]
    public uint Reserved;

    [FieldOffset(72)]
    public FSRApiEffectMemoryUsage* GpuMemoryUsageFrameGenerationSwapChain;

    public const ulong Type = Fsr.APIQueryDescTypeFrameGenerationSwapChainGPUMemoryUsageVKV2;

    public FSRQueryFrameGenerationSwapChainGetGPUMemoryUsageVKV2()
    {
        this = default;
        Header.Type = Type;
    }
}
