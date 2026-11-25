namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 56)]
public unsafe struct FSRQueryDescDenoiserGetGPUMemoryUsage
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public void* Device;

    [FieldOffset(24)]
    public FSRApiDimensions2D MaxRenderSize;

    [FieldOffset(32)]
    public uint SignalFlags;

    [FieldOffset(36)]
    public uint CheckerboardSignalFlags;

    [FieldOffset(40)]
    public uint Flags;

    [FieldOffset(48)]
    public FSRApiEffectMemoryUsage* GpuMemoryUsage;

    public const ulong Type = Fsr.APIQueryDescTypeDenoiserGPUMemoryUsage;

    public FSRQueryDescDenoiserGetGPUMemoryUsage()
    {
        this = default;
        Header.Type = Type;
    }
}
