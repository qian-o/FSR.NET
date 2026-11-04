namespace FSR.NET;

[Flags]
public enum FSRVkBufferUsageFlagBits : int
{
    TransferSrcBit = 1,

    TransferDstBit = 2,

    UniformTexelBufferBit = 4,

    StorageTexelBufferBit = 8,

    UniformBufferBit = 16,

    StorageBufferBit = 32,

    IndexBufferBit = 64,

    VertexBufferBit = 128,

    IndirectBufferBit = 256,

    ShaderDeviceAddressBit = 131072,

    VideoDecodeSrcBitKhr = 8192,

    VideoDecodeDstBitKhr = 16384,

    TransformFeedbackBufferBitExt = 2048,

    TransformFeedbackCounterBufferBitExt = 4096,

    ConditionalRenderingBitExt = 512,

    AccelerationStructureBuildInputReadOnlyBitKhr = 524288,

    AccelerationStructureStorageBitKhr = 1048576,

    ShaderBindingTableBitKhr = 1024,

    VideoEncodeDstBitKhr = 32768,

    VideoEncodeSrcBitKhr = 65536,

    SamplerDescriptorBufferBitExt = 2097152,

    ResourceDescriptorBufferBitExt = 4194304,

    PushDescriptorsDescriptorBufferBitExt = 67108864,

    MicromapBuildInputReadOnlyBitExt = 8388608,

    MicromapStorageBitExt = 16777216,

    TileMemoryBitQcom = 134217728,

    RayTracingBitNv = 1024,

    ShaderDeviceAddressBitExt = 131072,

    ShaderDeviceAddressBitKhr = 131072,

    FlagBitsMaxEnum = 2147483647,
}
