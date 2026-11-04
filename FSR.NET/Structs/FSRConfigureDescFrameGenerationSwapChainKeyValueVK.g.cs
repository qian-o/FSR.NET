namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 40)]
public unsafe struct FSRConfigureDescFrameGenerationSwapChainKeyValueVK
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public ulong Key;

    [FieldOffset(24)]
    public ulong U64;

    [FieldOffset(32)]
    public void* Ptr;

    public const ulong Type = Fsr.APIConfigureDescTypeFrameGenerationSwapChainKeyValueVK;

    public FSRConfigureDescFrameGenerationSwapChainKeyValueVK()
    {
        this = default;
        Header.Type = Type;
    }
}
