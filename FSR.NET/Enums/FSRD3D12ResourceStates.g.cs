namespace FSR.NET;

[Flags]
public enum FSRD3D12ResourceStates : int
{
    StateCommon = 0,

    StateVertexAndConstantBuffer = 1 << 0,

    StateIndexBuffer = 1 << 1,

    StateRenderTarget = 1 << 2,

    StateUnorderedAccess = 1 << 3,

    StateDepthWrite = 1 << 4,

    StateDepthRead = 1 << 5,

    StateNonPixelShaderResource = 1 << 6,

    StatePixelShaderResource = 1 << 7,

    StateStreamOut = 1 << 8,

    StateIndirectArgument = 1 << 9,

    StateCopyDest = 1 << 10,

    StateCopySource = 1 << 11,

    StateResolveDest = 1 << 12,

    StateResolveSource = 1 << 13,

    StateRaytracingAccelerationStructure = 1 << 22,

    StateShadingRateSource = 1 << 24,

    StateReservedInternal8000 = 1 << 15,

    StateReservedInternal4000 = 1 << 14,

    StateReservedInternal100000 = 1 << 20,

    StateReservedInternal40000000 = 1 << 30,

    StateReservedInternal80000000 = 1 << 31,

    StateGenericRead = (1 << 0) | (1 << 1) | (1 << 6) | (1 << 7) | (1 << 9) | (1 << 11),

    StateAllShaderResource = (1 << 6) | (1 << 7),

    StatePresent = 0,

    StatePredication = 1 << 9,

    StateVideoDecodeRead = 1 << 16,

    StateVideoDecodeWrite = 1 << 17,

    StateVideoProcessRead = 1 << 18,

    StateVideoProcessWrite = 1 << 19,

    StateVideoEncodeRead = 1 << 21,

    StateVideoEncodeWrite = 1 << 23,
}
