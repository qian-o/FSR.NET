namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 32)]
public unsafe struct FSRQueryGetProviderVersion
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public ulong VersionId;

    [FieldOffset(24)]
    public byte* VersionName;

    public const ulong Type = Fsr.APIQueryDescTypeGetProviderVersion;

    public FSRQueryGetProviderVersion()
    {
        this = default;
        Header.Type = Type;
    }
}
