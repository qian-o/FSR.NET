namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 56)]
public unsafe struct FSRQueryDescGetVersions
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public ulong CreateDescType;

    [FieldOffset(24)]
    public void* Device;

    [FieldOffset(32)]
    public ulong* OutputCount;

    [FieldOffset(40)]
    public ulong* VersionIds;

    [FieldOffset(48)]
    public byte** VersionNames;

    public const ulong Type = Fsr.APIQueryDescTypeGetVersions;

    public FSRQueryDescGetVersions()
    {
        this = default;
        Header.Type = Type;
    }
}
