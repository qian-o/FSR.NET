namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 184)]
public unsafe struct FSRDispatchDescFrameGenerationPrepare
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public ulong FrameID;

    [FieldOffset(24)]
    public uint Flags;

    [FieldOffset(32)]
    public void* CommandList;

    [FieldOffset(40)]
    public FSRApiDimensions2D RenderSize;

    [FieldOffset(48)]
    public FSRApiFloatCoords2D JitterOffset;

    [FieldOffset(56)]
    public FSRApiFloatCoords2D MotionVectorScale;

    [FieldOffset(64)]
    public float FrameTimeDelta;

    [FieldOffset(68)]
    public Bool8 UnusedReset;

    [FieldOffset(72)]
    public float CameraNear;

    [FieldOffset(76)]
    public float CameraFar;

    [FieldOffset(80)]
    public float CameraFovAngleVertical;

    [FieldOffset(84)]
    public float ViewSpaceToMetersFactor;

    [FieldOffset(88)]
    public FSRApiResource Depth;

    [FieldOffset(136)]
    public FSRApiResource MotionVectors;

    public const ulong Type = Fsr.APIDispatchDescTypeFrameGenerationPrepare;

    public FSRDispatchDescFrameGenerationPrepare()
    {
        this = default;
        Header.Type = Type;
    }
}
