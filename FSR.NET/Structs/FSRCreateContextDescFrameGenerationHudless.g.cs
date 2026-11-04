namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public struct FSRCreateContextDescFrameGenerationHudless
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public uint HudlessBackBufferFormat;

    public const ulong Type = Fsr.APICreateContextDescTypeFrameGenerationHudless;

    public FSRCreateContextDescFrameGenerationHudless()
    {
        this = default;
        Header.Type = Type;
    }
}
