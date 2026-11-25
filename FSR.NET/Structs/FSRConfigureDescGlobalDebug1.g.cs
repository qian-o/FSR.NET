namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 32)]
public unsafe struct FSRConfigureDescGlobalDebug1
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public delegate* unmanaged[Cdecl]<uint, char*, void> FpMessage;

    [FieldOffset(24)]
    public uint DebugLevel;

    public const ulong Type = Fsr.APIConfigureDescTypeGlobalDebug1;

    public FSRConfigureDescGlobalDebug1()
    {
        this = default;
        Header.Type = Type;
    }
}
