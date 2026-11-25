namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public struct FSRCallbackDescFrameGenerationPresentPremulAlpha
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public Bool8 UsePremulAlpha;

    public const ulong Type = Fsr.APICallbackDescTypeFrameGenerationPresentPremulAlpha;

    public FSRCallbackDescFrameGenerationPresentPremulAlpha()
    {
        this = default;
        Header.Type = Type;
    }
}
