namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public struct FSRCreateContextDescUpscaleVersion
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public uint Version;

    public const ulong Type = Fsr.APICreateContextDescTypeUpscaleVersion;

    public FSRCreateContextDescUpscaleVersion()
    {
        this = default;
        Header.Type = Type;
    }
}
