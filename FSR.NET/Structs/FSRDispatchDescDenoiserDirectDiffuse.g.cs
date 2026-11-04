namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 120)]
public struct FSRDispatchDescDenoiserDirectDiffuse
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiDenoiserSignal Signal;

    public const ulong Type = Fsr.APIDispatchDescTypeDenoiserDirectDiffuse;

    public FSRDispatchDescDenoiserDirectDiffuse()
    {
        this = default;
        Header.Type = Type;
    }
}
