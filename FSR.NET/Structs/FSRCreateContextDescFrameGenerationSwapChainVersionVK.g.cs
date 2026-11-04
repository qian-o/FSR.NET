namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public struct FSRCreateContextDescFrameGenerationSwapChainVersionVK
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public uint Version;

    public const ulong Type = Fsr.APICreateContextDescTypeFrameGenerationSwapChainVersionVK;

    public FSRCreateContextDescFrameGenerationSwapChainVersionVK()
    {
        this = default;
        Header.Type = Type;
    }
}
