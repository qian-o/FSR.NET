namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 56)]
public unsafe struct FSRQueryDescUpscaleGetGPUMemoryUsageV2
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public void* Device;

    [FieldOffset(24)]
    public FSRApiDimensions2D MaxRenderSize;

    [FieldOffset(32)]
    public FSRApiDimensions2D MaxUpscaleSize;

    [FieldOffset(40)]
    public uint Flags;

    [FieldOffset(48)]
    public FSRApiEffectMemoryUsage* GpuMemoryUsageUpscaler;

    public const ulong Type = Fsr.APIQueryDescTypeUpscaleGPUMemoryUsageV2;

    public FSRQueryDescUpscaleGetGPUMemoryUsageV2()
    {
        this = default;
        Header.Type = Type;
    }
}
