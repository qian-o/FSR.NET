namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 64)]
public struct FSRDispatchDescFrameGenerationPrepareCameraInfo
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public CameraPositionBuffer CameraPosition;

    [FieldOffset(28)]
    public CameraUpBuffer CameraUp;

    [FieldOffset(40)]
    public CameraRightBuffer CameraRight;

    [FieldOffset(52)]
    public CameraForwardBuffer CameraForward;

    public const ulong Type = Fsr.APIDispatchDescTypeFrameGenerationPrepareCameraInfo;

    public FSRDispatchDescFrameGenerationPrepareCameraInfo()
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
