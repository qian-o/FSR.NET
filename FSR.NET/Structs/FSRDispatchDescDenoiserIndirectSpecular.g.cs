namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 120)]
public struct FSRDispatchDescDenoiserIndirectSpecular
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiDenoiserSignal Signal;

    public const ulong Type = Fsr.APIDispatchDescTypeDenoiserIndirectSpecular;

    public FSRDispatchDescDenoiserIndirectSpecular()
    {
        this = default;
        Header.Type = Type;
    }
}
