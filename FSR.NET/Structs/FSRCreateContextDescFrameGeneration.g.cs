namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 40)]
public struct FSRCreateContextDescFrameGeneration
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public uint Flags;

    [FieldOffset(20)]
    public FSRApiDimensions2D DisplaySize;

    [FieldOffset(28)]
    public FSRApiDimensions2D MaxRenderSize;

    [FieldOffset(36)]
    public uint BackBufferFormat;

    public const ulong Type = Fsr.APICreateContextDescTypeFrameGeneration;

    public FSRCreateContextDescFrameGeneration()
    {
        this = default;
        Header.Type = Type;
    }
}
