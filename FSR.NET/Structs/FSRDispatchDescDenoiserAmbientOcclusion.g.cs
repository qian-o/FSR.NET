namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 120)]
public struct FSRDispatchDescDenoiserAmbientOcclusion
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiDenoiserSignal Signal;

    public const ulong Type = Fsr.APIDispatchDescTypeDenoiserAmbientOcclusion;

    public FSRDispatchDescDenoiserAmbientOcclusion()
    {
        this = default;
        Header.Type = Type;
    }
}
