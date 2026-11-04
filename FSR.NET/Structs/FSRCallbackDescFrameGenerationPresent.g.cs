namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 192)]
public unsafe struct FSRCallbackDescFrameGenerationPresent
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public void* Device;

    [FieldOffset(24)]
    public void* CommandList;

    [FieldOffset(32)]
    public FSRApiResource CurrentBackBuffer;

    [FieldOffset(80)]
    public FSRApiResource CurrentUI;

    [FieldOffset(128)]
    public FSRApiResource OutputSwapChainBuffer;

    [FieldOffset(176)]
    public Bool8 IsGeneratedFrame;

    [FieldOffset(184)]
    public ulong FrameID;

    public const ulong Type = Fsr.APICallbackDescTypeFrameGenerationPresent;

    public FSRCallbackDescFrameGenerationPresent()
    {
        this = default;
        Header.Type = Type;
    }
}
