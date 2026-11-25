namespace FSR.NET;

[Flags]
public enum FSRD3D12HeapFlags : int
{
    None = 0,

    Shared = 1 << 0,

    DenyBuffers = 1 << 2,

    AllowDisplay = 1 << 3,

    SharedCrossAdapter = 1 << 5,

    DenyRtDsTextures = 1 << 6,

    DenyNonRtDsTextures = 1 << 7,

    HardwareProtected = 1 << 8,

    AllowWriteWatch = 1 << 9,

    AllowShaderAtomics = 1 << 10,

    CreateNotResident = 1 << 11,

    CreateNotZeroed = 1 << 12,

    ToolsUseManualWriteTracking = 1 << 13,

    AllowAllBuffersAndTextures = 0,

    AllowOnlyBuffers = (1 << 6) | (1 << 7),

    AllowOnlyNonRtDsTextures = (1 << 2) | (1 << 6),

    AllowOnlyRtDsTextures = (1 << 2) | (1 << 7),
}
