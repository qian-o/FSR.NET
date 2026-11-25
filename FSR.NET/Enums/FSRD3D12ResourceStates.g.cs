namespace FSR.NET;

[Flags]
public enum FSRD3D12ResourceStates : int
{
    Common = 0,

    VertexAndConstantBuffer = 1 << 0,

    IndexBuffer = 1 << 1,

    RenderTarget = 1 << 2,

    UnorderedAccess = 1 << 3,

    DepthWrite = 1 << 4,

    DepthRead = 1 << 5,

    NonPixelShaderResource = 1 << 6,

    PixelShaderResource = 1 << 7,

    StreamOut = 1 << 8,

    IndirectArgument = 1 << 9,

    CopyDest = 1 << 10,

    CopySource = 1 << 11,

    ResolveDest = 1 << 12,

    ResolveSource = 1 << 13,

    RaytracingAccelerationStructure = 1 << 22,

    ShadingRateSource = 1 << 24,

    ReservedInternal8000 = 1 << 15,

    ReservedInternal4000 = 1 << 14,

    ReservedInternal100000 = 1 << 20,

    ReservedInternal40000000 = 1 << 30,

    ReservedInternal80000000 = 1 << 31,

    GenericRead = (1 << 0) | (1 << 1) | (1 << 6) | (1 << 7) | (1 << 9) | (1 << 11),

    AllShaderResource = (1 << 6) | (1 << 7),

    Present = 0,

    Predication = 1 << 9,

    VideoDecodeRead = 1 << 16,

    VideoDecodeWrite = 1 << 17,

    VideoProcessRead = 1 << 18,

    VideoProcessWrite = 1 << 19,

    VideoEncodeRead = 1 << 21,

    VideoEncodeWrite = 1 << 23,
}
