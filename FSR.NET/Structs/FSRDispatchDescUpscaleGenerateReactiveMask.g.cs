namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 192)]
public unsafe struct FSRDispatchDescUpscaleGenerateReactiveMask
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public void* CommandList;

    [FieldOffset(24)]
    public FSRApiResource ColorOpaqueOnly;

    [FieldOffset(72)]
    public FSRApiResource ColorPreUpscale;

    [FieldOffset(120)]
    public FSRApiResource Reactive;

    [FieldOffset(168)]
    public FSRApiDimensions2D RenderSize;

    [FieldOffset(176)]
    public float Scale;

    [FieldOffset(180)]
    public float CutoffThreshold;

    [FieldOffset(184)]
    public float BinaryValue;

    [FieldOffset(188)]
    public uint Flags;

    public const ulong Type = Fsr.APIDispatchDescTypeUpscaleGenerateReactiveMask;

    public FSRDispatchDescUpscaleGenerateReactiveMask()
    {
        this = default;
        Header.Type = Type;
    }
}
