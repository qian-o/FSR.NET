namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 448)]
public unsafe struct FSRDispatchDescDenoiser
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public void* CommandList;

    [FieldOffset(24)]
    public FSRApiResource LinearDepth;

    [FieldOffset(72)]
    public FSRApiResource MotionVectors;

    [FieldOffset(120)]
    public FSRApiResource Normals;

    [FieldOffset(168)]
    public FSRApiResource SpecularAlbedo;

    [FieldOffset(216)]
    public FSRApiResource DiffuseAlbedo;

    [FieldOffset(264)]
    public FSRApiFloatCoords3D MotionVectorScale;

    [FieldOffset(276)]
    public FSRApiFloatCoords2D JitterOffsets;

    [FieldOffset(284)]
    public FSRApiFloatCoords3D CameraPositionDelta;

    [FieldOffset(296)]
    public FSRApiMatrix4x4 View;

    [FieldOffset(360)]
    public FSRApiMatrix4x4 Projection;

    [FieldOffset(424)]
    public FSRApiFloatBounds LinearDepthBounds;

    [FieldOffset(432)]
    public FSRApiDimensions2D RenderSize;

    [FieldOffset(440)]
    public uint FrameIndex;

    [FieldOffset(444)]
    public uint Flags;

    public const ulong Type = Fsr.APIDispatchDescTypeDenoiser;

    public FSRDispatchDescDenoiser()
    {
        this = default;
        Header.Type = Type;
    }
}
