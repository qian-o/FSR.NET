namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public unsafe struct FSRQueryDescFrameGenerationSwapChainInterpolationTextureVK
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiResource* Texture;

    public const ulong Type = Fsr.APIQueryDescTypeFGSwapChainInterpolationTextureVK;

    public FSRQueryDescFrameGenerationSwapChainInterpolationTextureVK()
    {
        this = default;
        Header.Type = Type;
    }
}
