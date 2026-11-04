namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 80)]
public struct FSRDispatchDescDenoiserDebugView
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiResource Output;

    [FieldOffset(64)]
    public FSRApiDimensions2D OutputSize;

    [FieldOffset(72)]
    public uint Mode;

    [FieldOffset(76)]
    public uint ViewportIndex;

    public const ulong Type = Fsr.APIDispatchDescTypeDenoiserDebugView;

    public FSRDispatchDescDenoiserDebugView()
    {
        this = default;
        Header.Type = Type;
    }
}
