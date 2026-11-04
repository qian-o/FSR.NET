namespace FSR.NET;

[Flags]
public enum FSRD3D12ResourceFlags : int
{
    None = 0,

    AllowRenderTarget = 1,

    AllowDepthStencil = 2,

    AllowUnorderedAccess = 4,

    DenyShaderResource = 8,

    AllowCrossAdapter = 16,

    AllowSimultaneousAccess = 32,

    VideoDecodeReferenceOnly = 64,

    VideoEncodeReferenceOnly = 128,

    RaytracingAccelerationStructure = 256,
}
