namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 40)]
public struct FSRCreateContextDescDenoiser
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public uint Version;

    [FieldOffset(20)]
    public FSRApiDimensions2D MaxRenderSize;

    [FieldOffset(28)]
    public uint SignalFlags;

    [FieldOffset(32)]
    public uint CheckerboardSignalFlags;

    [FieldOffset(36)]
    public uint Flags;

    public const ulong Type = Fsr.APICreateContextDescTypeDenoiser;

    public FSRCreateContextDescDenoiser()
    {
        this = default;
        Header.Type = Type;
    }
}
