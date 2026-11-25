namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 432)]
public unsafe struct FSRDispatchDescUpscale
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public void* CommandList;

    [FieldOffset(24)]
    public FSRApiResource Color;

    [FieldOffset(72)]
    public FSRApiResource Depth;

    [FieldOffset(120)]
    public FSRApiResource MotionVectors;

    [FieldOffset(168)]
    public FSRApiResource Exposure;

    [FieldOffset(216)]
    public FSRApiResource Reactive;

    [FieldOffset(264)]
    public FSRApiResource TransparencyAndComposition;

    [FieldOffset(312)]
    public FSRApiResource Output;

    [FieldOffset(360)]
    public FSRApiFloatCoords2D JitterOffset;

    [FieldOffset(368)]
    public FSRApiFloatCoords2D MotionVectorScale;

    [FieldOffset(376)]
    public FSRApiDimensions2D RenderSize;

    [FieldOffset(384)]
    public FSRApiDimensions2D UpscaleSize;

    [FieldOffset(392)]
    public Bool8 EnableSharpening;

    [FieldOffset(396)]
    public float Sharpness;

    [FieldOffset(400)]
    public float FrameTimeDelta;

    [FieldOffset(404)]
    public float PreExposure;

    [FieldOffset(408)]
    public Bool8 Reset;

    [FieldOffset(412)]
    public float CameraNear;

    [FieldOffset(416)]
    public float CameraFar;

    [FieldOffset(420)]
    public float CameraFovAngleVertical;

    [FieldOffset(424)]
    public float ViewSpaceToMetersFactor;

    [FieldOffset(428)]
    public uint Flags;

    public const ulong Type = Fsr.APIDispatchDescTypeUpscale;

    public FSRDispatchDescUpscale()
    {
        this = default;
        Header.Type = Type;
    }
}
