namespace FSR.NET;

[Flags]
public enum FSRD3D12ResourceStates : int
{
    Common = 0,

    VertexAndConstantBuffer = 1,

    IndexBuffer = 2,

    RenderTarget = 4,

    UnorderedAccess = 8,

    DepthWrite = 16,

    DepthRead = 32,

    NonPixelShaderResource = 64,

    PixelShaderResource = 128,

    StreamOut = 256,

    IndirectArgument = 512,

    CopyDest = 1024,

    CopySource = 2048,

    ResolveDest = 4096,

    ResolveSource = 8192,

    RaytracingAccelerationStructure = 4194304,

    ShadingRateSource = 16777216,

    ReservedInternal8000 = 32768,

    ReservedInternal4000 = 16384,

    ReservedInternal100000 = 1048576,

    ReservedInternal40000000 = 1073741824,

    ReservedInternal80000000 = -2147483648,

    GenericRead = 2755,

    AllShaderResource = 192,

    Present = 0,

    Predication = 512,

    VideoDecodeRead = 65536,

    VideoDecodeWrite = 131072,

    VideoProcessRead = 262144,

    VideoProcessWrite = 524288,

    VideoEncodeRead = 2097152,

    VideoEncodeWrite = 8388608,
}
