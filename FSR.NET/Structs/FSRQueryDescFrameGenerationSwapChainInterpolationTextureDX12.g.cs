namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public unsafe struct FSRQueryDescFrameGenerationSwapChainInterpolationTextureDX12
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiResource* Texture;

    public const ulong Type = Fsr.APIQueryDescTypeFrameGenerationSwapChainInterpolationTextureDX12;

    public FSRQueryDescFrameGenerationSwapChainInterpolationTextureDX12()
    {
        this = default;
        Header.Type = Type;
    }
}
