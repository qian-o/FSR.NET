namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 72)]
public struct FSRConfigureDescFrameGenerationSwapChainRegisterUiResourceDX12
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiResource UiResource;

    [FieldOffset(64)]
    public uint Flags;

    public const ulong Type = Fsr.APIConfigureDescTypeFrameGenerationSwapChainRegisterUIResourceDX12;

    public FSRConfigureDescFrameGenerationSwapChainRegisterUiResourceDX12()
    {
        this = default;
        Header.Type = Type;
    }
}
