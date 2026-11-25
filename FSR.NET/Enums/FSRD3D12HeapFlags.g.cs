namespace FSR.NET;

[Flags]
public enum FSRD3D12HeapFlags : int
{
    None = 0,

    Shared = 1,

    DenyBuffers = 4,

    AllowDisplay = 8,

    SharedCrossAdapter = 32,

    DenyRtDsTextures = 64,

    DenyNonRtDsTextures = 128,

    HardwareProtected = 256,

    AllowWriteWatch = 512,

    AllowShaderAtomics = 1024,

    CreateNotResident = 2048,

    CreateNotZeroed = 4096,

    ToolsUseManualWriteTracking = 8192,

    AllowAllBuffersAndTextures = 0,

    AllowOnlyBuffers = 192,

    AllowOnlyNonRtDsTextures = 68,

    AllowOnlyRtDsTextures = 132,
}
