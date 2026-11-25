namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 40)]
public unsafe struct FSRConfigureDescFrameGenerationSwapChainKeyValueDX12
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public ulong Key;

    [FieldOffset(24)]
    public ulong U64;

    [FieldOffset(32)]
    public void* Ptr;

    public const ulong Type = Fsr.APIConfigureDescTypeFrameGenerationSwapChainKeyValueDX12;

    public FSRConfigureDescFrameGenerationSwapChainKeyValueDX12()
    {
        this = default;
        Header.Type = Type;
    }
}
