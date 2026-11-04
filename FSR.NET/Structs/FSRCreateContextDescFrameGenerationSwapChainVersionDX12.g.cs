namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public struct FSRCreateContextDescFrameGenerationSwapChainVersionDX12
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public uint Version;

    public const ulong Type = Fsr.APICreateContextDescTypeFrameGenerationSwapChainVersionDX12;

    public FSRCreateContextDescFrameGenerationSwapChainVersionDX12()
    {
        this = default;
        Header.Type = Type;
    }
}
