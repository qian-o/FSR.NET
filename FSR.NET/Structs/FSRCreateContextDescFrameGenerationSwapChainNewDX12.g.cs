namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 48)]
public unsafe struct FSRCreateContextDescFrameGenerationSwapChainNewDX12
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public nint* SwapChain;

    [FieldOffset(24)]
    public FSRDxgiSwapChainDesc* Desc;

    [FieldOffset(32)]
    public nint DxgiFactory;

    [FieldOffset(40)]
    public nint GameQueue;

    public const ulong Type = Fsr.APICreateContextDescTypeFrameGenerationSwapChainNewDX12;

    public FSRCreateContextDescFrameGenerationSwapChainNewDX12()
    {
        this = default;
        Header.Type = Type;
    }
}
