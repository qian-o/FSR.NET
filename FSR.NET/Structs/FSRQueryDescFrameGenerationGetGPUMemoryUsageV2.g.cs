namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 64)]
public unsafe struct FSRQueryDescFrameGenerationGetGPUMemoryUsageV2
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public void* Device;

    [FieldOffset(24)]
    public FSRApiDimensions2D MaxRenderSize;

    [FieldOffset(32)]
    public FSRApiDimensions2D DisplaySize;

    [FieldOffset(40)]
    public uint CreateFlags;

    [FieldOffset(44)]
    public uint DispatchFlags;

    [FieldOffset(48)]
    public uint BackBufferFormat;

    [FieldOffset(52)]
    public uint HudlessBackBufferFormat;

    [FieldOffset(56)]
    public FSRApiEffectMemoryUsage* GpuMemoryUsageFrameGeneration;

    public const ulong Type = Fsr.APIQueryDescTypeFrameGenerationGPUMemoryUsageV2;

    public FSRQueryDescFrameGenerationGetGPUMemoryUsageV2()
    {
        this = default;
        Header.Type = Type;
    }
}
