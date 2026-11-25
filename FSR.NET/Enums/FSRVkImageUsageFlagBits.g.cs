namespace FSR.NET;

[Flags]
public enum FSRVkImageUsageFlagBits : int
{
    TransferSrcBit = 1,

    TransferDstBit = 2,

    SampledBit = 4,

    StorageBit = 8,

    ColorAttachmentBit = 16,

    DepthStencilAttachmentBit = 32,

    TransientAttachmentBit = 64,

    InputAttachmentBit = 128,

    HostTransferBit = 4194304,

    VideoDecodeDstBitKhr = 1024,

    VideoDecodeSrcBitKhr = 2048,

    VideoDecodeDpbBitKhr = 4096,

    FragmentDensityMapBitExt = 512,

    FragmentShadingRateAttachmentBitKhr = 256,

    VideoEncodeDstBitKhr = 8192,

    VideoEncodeSrcBitKhr = 16384,

    VideoEncodeDpbBitKhr = 32768,

    AttachmentFeedbackLoopBitExt = 524288,

    InvocationMaskBitHuawei = 262144,

    SampleWeightBitQcom = 1048576,

    SampleBlockMatchBitQcom = 2097152,

    TensorAliasingBitArm = 8388608,

    TileMemoryBitQcom = 134217728,

    VideoEncodeQuantizationDeltaMapBitKhr = 33554432,

    VideoEncodeEmphasisMapBitKhr = 67108864,

    ShadingRateImageBitNv = 256,

    HostTransferBitExt = 4194304,

    FlagBitsMaxEnum = 2147483647,
}
