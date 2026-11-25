namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 16)]
public struct FSRDispatchDescFrameGenerationSwapChainWaitForPresentsDX12
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    public const ulong Type = Fsr.APIDispatchDescTypeFrameGenerationSwapChainWaitForPresentsDX12;

    public FSRDispatchDescFrameGenerationSwapChainWaitForPresentsDX12()
    {
        this = default;
        Header.Type = Type;
    }
}
