namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 120)]
public struct FSRDispatchDescDenoiserIndirectDiffuse
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiDenoiserSignal Signal;

    public const ulong Type = Fsr.APIDispatchDescTypeDenoiserIndirectDiffuse;

    public FSRDispatchDescDenoiserIndirectDiffuse()
    {
        this = default;
        Header.Type = Type;
    }
}
