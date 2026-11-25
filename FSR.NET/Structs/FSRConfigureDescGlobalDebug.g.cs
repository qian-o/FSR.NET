namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 40)]
public unsafe struct FSRConfigureDescGlobalDebug
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public ulong EffectId;

    [FieldOffset(24)]
    public delegate* unmanaged[Cdecl]<uint, char*, void> FpMessage;

    [FieldOffset(32)]
    public uint DebugLevel;

    public const ulong Type = Fsr.APIConfigureDescTypeGlobalDebug;

    public FSRConfigureDescGlobalDebug()
    {
        this = default;
        Header.Type = Type;
    }
}
