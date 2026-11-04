namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public struct FSRCreateContextDescFrameGenerationVersion
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public uint Version;

    public const ulong Type = Fsr.APICreateContextDescTypeFrameGenerationVersion;

    public FSRCreateContextDescFrameGenerationVersion()
    {
        this = default;
        Header.Type = Type;
    }
}
