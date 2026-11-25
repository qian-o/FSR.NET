namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public unsafe struct FSRQueryDescFrameGenerationSwapChainInterpolationCommandListVK
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public void** CommandList;

    public const ulong Type = Fsr.APIQueryDescTypeFGSwapChainInterpolationCommandListVK;

    public FSRQueryDescFrameGenerationSwapChainInterpolationCommandListVK()
    {
        this = default;
        Header.Type = Type;
    }
}
