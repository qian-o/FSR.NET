namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 120)]
public struct FSRDispatchDescDenoiserSpecularOcclusion
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiDenoiserSignal Signal;

    public const ulong Type = Fsr.APIDispatchDescTypeDenoiserSpecularOcclusion;

    public FSRDispatchDescDenoiserSpecularOcclusion()
    {
        this = default;
        Header.Type = Type;
    }
}
