namespace FSR.NET;

[Flags]
public enum FSRD3D12ResourceFlags : int
{
    None = 0,

    AllowRenderTarget = 1 << 0,

    AllowDepthStencil = 1 << 1,

    AllowUnorderedAccess = 1 << 2,

    DenyShaderResource = 1 << 3,

    AllowCrossAdapter = 1 << 4,

    AllowSimultaneousAccess = 1 << 5,

    VideoDecodeReferenceOnly = 1 << 6,

    VideoEncodeReferenceOnly = 1 << 7,

    RaytracingAccelerationStructure = 1 << 8,
}
