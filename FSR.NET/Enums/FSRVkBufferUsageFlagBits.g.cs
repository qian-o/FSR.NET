namespace FSR.NET;

[Flags]
public enum FSRVkBufferUsageFlagBits : int
{
    TransferSrcBit = 1 << 0,

    TransferDstBit = 1 << 1,

    UniformTexelBufferBit = 1 << 2,

    StorageTexelBufferBit = 1 << 3,

    UniformBufferBit = 1 << 4,

    StorageBufferBit = 1 << 5,

    IndexBufferBit = 1 << 6,

    VertexBufferBit = 1 << 7,

    IndirectBufferBit = 1 << 8,

    ShaderDeviceAddressBit = 1 << 17,

    VideoDecodeSrcBitKhr = 1 << 13,

    VideoDecodeDstBitKhr = 1 << 14,

    TransformFeedbackBufferBitExt = 1 << 11,

    TransformFeedbackCounterBufferBitExt = 1 << 12,

    ConditionalRenderingBitExt = 1 << 9,

    DescriptorHeapBitExt = 1 << 28,

    AccelerationStructureBuildInputReadOnlyBitKhr = 1 << 19,

    AccelerationStructureStorageBitKhr = 1 << 20,

    ShaderBindingTableBitKhr = 1 << 10,

    VideoEncodeDstBitKhr = 1 << 15,

    VideoEncodeSrcBitKhr = 1 << 16,

    SamplerDescriptorBufferBitExt = 1 << 21,

    ResourceDescriptorBufferBitExt = 1 << 22,

    PushDescriptorsDescriptorBufferBitExt = 1 << 26,

    MicromapBuildInputReadOnlyBitExt = 1 << 23,

    MicromapStorageBitExt = 1 << 24,

    TileMemoryBitQcom = 1 << 27,

    RayTracingBitNv = 1 << 10,

    ShaderDeviceAddressBitExt = 1 << 17,

    ShaderDeviceAddressBitKhr = 1 << 17,

    FlagBitsMaxEnum = 0x7FFFFFFF,

    None = 0
}
