namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 64)]
public unsafe struct FSRCreateContextDescFrameGenerationSwapChainForHwndDX12
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public nint* SwapChain;

    [FieldOffset(24)]
    public FSRHwnd* Hwnd;

    [FieldOffset(32)]
    public FSRDxgiSwapChainDesc1* Desc;

    [FieldOffset(40)]
    public FSRDxgiSwapChainFullscreenDesc* FullscreenDesc;

    [FieldOffset(48)]
    public nint DxgiFactory;

    [FieldOffset(56)]
    public nint GameQueue;

    public const ulong Type = Fsr.APICreateContextDescTypeFrameGenerationSwapChainForHwndDX12;

    public FSRCreateContextDescFrameGenerationSwapChainForHwndDX12()
    {
        this = default;
        Header.Type = Type;
    }
}
