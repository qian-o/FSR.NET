namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 40)]
public unsafe struct FSRQueryDescDenoiserGetDefaultKeyValue
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public ulong Key;

    [FieldOffset(24)]
    public ulong Count;

    [FieldOffset(32)]
    public void* Data;

    public const ulong Type = Fsr.APIQueryDescTypeDenoiserGetDefaultKeyValue;

    public FSRQueryDescDenoiserGetDefaultKeyValue()
    {
        this = default;
        Header.Type = Type;
    }
}
