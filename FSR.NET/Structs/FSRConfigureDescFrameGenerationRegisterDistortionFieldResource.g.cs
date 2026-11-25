namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 64)]
public struct FSRConfigureDescFrameGenerationRegisterDistortionFieldResource
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiResource DistortionField;

    public const ulong Type = Fsr.APIConfigureDescTypeFrameGenerationRegisterDistortionResource;

    public FSRConfigureDescFrameGenerationRegisterDistortionFieldResource()
    {
        this = default;
        Header.Type = Type;
    }
}
