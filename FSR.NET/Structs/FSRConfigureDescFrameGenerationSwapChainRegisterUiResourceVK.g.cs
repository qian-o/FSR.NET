namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 72)]
public struct FSRConfigureDescFrameGenerationSwapChainRegisterUiResourceVK
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiResource UiResource;

    [FieldOffset(64)]
    public uint Flags;

    public const ulong Type = Fsr.APIConfigureDescTypeFGSwapChainRegisterUIResourceVK;

    public FSRConfigureDescFrameGenerationSwapChainRegisterUiResourceVK()
    {
        this = default;
        Header.Type = Type;
    }
}
