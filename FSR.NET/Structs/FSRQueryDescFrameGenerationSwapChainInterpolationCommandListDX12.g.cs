namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public unsafe struct FSRQueryDescFrameGenerationSwapChainInterpolationCommandListDX12
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public void** CommandList;

    public const ulong Type = Fsr.APIQueryDescTypeFrameGenerationSwapChainInterpolationCommandListDX12;

    public FSRQueryDescFrameGenerationSwapChainInterpolationCommandListDX12()
    {
        this = default;
        Header.Type = Type;
    }
}
