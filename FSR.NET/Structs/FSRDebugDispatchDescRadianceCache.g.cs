namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 120)]
public unsafe struct FSRDebugDispatchDescRadianceCache
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public void* CommandList;

    [FieldOffset(24)]
    public FSRApiResource DebugTexture;

    [FieldOffset(72)]
    public FSRApiResource DebugPixelInfo;

    public const ulong Type = Fsr.APIDebugDispatchDescTypeRadianceCache;

    public FSRDebugDispatchDescRadianceCache()
    {
        this = default;
        Header.Type = Type;
    }
}
