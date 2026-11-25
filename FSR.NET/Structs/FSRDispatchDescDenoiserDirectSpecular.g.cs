namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 120)]
public struct FSRDispatchDescDenoiserDirectSpecular
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiDenoiserSignal Signal;

    public const ulong Type = Fsr.APIDispatchDescTypeDenoiserDirectSpecular;

    public FSRDispatchDescDenoiserDirectSpecular()
    {
        this = default;
        Header.Type = Type;
    }
}
