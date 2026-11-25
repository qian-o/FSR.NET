namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public struct FSROverrideVersion
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public ulong VersionId;

    public const ulong Type = Fsr.APIDescTypeOverrideVersion;

    public FSROverrideVersion()
    {
        this = default;
        Header.Type = Type;
    }
}
