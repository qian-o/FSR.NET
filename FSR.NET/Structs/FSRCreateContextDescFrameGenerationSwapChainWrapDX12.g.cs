namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 32)]
public unsafe struct FSRCreateContextDescFrameGenerationSwapChainWrapDX12
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public nint* SwapChain;

    [FieldOffset(24)]
    public nint GameQueue;

    public const ulong Type = Fsr.APICreateContextDescTypeFrameGenerationSwapChainWrapDX12;

    public FSRCreateContextDescFrameGenerationSwapChainWrapDX12()
    {
        this = default;
        Header.Type = Type;
    }
}
