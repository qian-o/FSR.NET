namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 48)]
public unsafe struct FSRCreateContextDescUpscale
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public uint Flags;

    [FieldOffset(20)]
    public FSRApiDimensions2D MaxRenderSize;

    [FieldOffset(28)]
    public FSRApiDimensions2D MaxUpscaleSize;

    [FieldOffset(40)]
    public delegate* unmanaged[Cdecl]<uint, char*, void> FpMessage;

    public const ulong Type = Fsr.APICreateContextDescTypeUpscale;

    public FSRCreateContextDescUpscale()
    {
        this = default;
        Header.Type = Type;
    }
}
