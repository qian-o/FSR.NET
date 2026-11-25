namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 232)]
public unsafe struct FSRDispatchDescFrameGenerationPrepareV2
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
    public Bool8 Reset;

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

    [FieldOffset(184)]
    public CameraPositionBuffer CameraPosition;

    [FieldOffset(196)]
    public CameraUpBuffer CameraUp;

    [FieldOffset(208)]
    public CameraRightBuffer CameraRight;

    [FieldOffset(220)]
    public CameraForwardBuffer CameraForward;

    public const ulong Type = Fsr.APIDispatchDescTypeFrameGenerationPrepareV2;

    public FSRDispatchDescFrameGenerationPrepareV2()
    {
        this = default;
        Header.Type = Type;
    }

    [InlineArray(3)]
    public struct CameraPositionBuffer
    {
        private float element;
    }

    [InlineArray(3)]
    public struct CameraUpBuffer
    {
        private float element;
    }

    [InlineArray(3)]
    public struct CameraRightBuffer
    {
        private float element;
    }

    [InlineArray(3)]
    public struct CameraForwardBuffer
    {
        private float element;
    }
}
