namespace FSR.NET;

[Flags]
public enum FSRVkImageUsageFlagBits : int
{
    TransferSrcBit = 1 << 0,

    TransferDstBit = 1 << 1,

    SampledBit = 1 << 2,

    StorageBit = 1 << 3,

    ColorAttachmentBit = 1 << 4,

    DepthStencilAttachmentBit = 1 << 5,

    TransientAttachmentBit = 1 << 6,

    InputAttachmentBit = 1 << 7,

    HostTransferBit = 1 << 22,

    VideoDecodeDstBitKhr = 1 << 10,

    VideoDecodeSrcBitKhr = 1 << 11,

    VideoDecodeDpbBitKhr = 1 << 12,

    FragmentDensityMapBitExt = 1 << 9,

    FragmentShadingRateAttachmentBitKhr = 1 << 8,

    VideoEncodeDstBitKhr = 1 << 13,

    VideoEncodeSrcBitKhr = 1 << 14,

    VideoEncodeDpbBitKhr = 1 << 15,

    AttachmentFeedbackLoopBitExt = 1 << 19,

    InvocationMaskBitHuawei = 1 << 18,

    SampleWeightBitQcom = 1 << 20,

    SampleBlockMatchBitQcom = 1 << 21,

    TensorAliasingBitArm = 1 << 23,

    TileMemoryBitQcom = 1 << 27,

    VideoEncodeQuantizationDeltaMapBitKhr = 1 << 25,

    VideoEncodeEmphasisMapBitKhr = 1 << 26,

    ShadingRateImageBitNv = 1 << 8,

    HostTransferBitExt = 1 << 22,

    FlagBitsMaxEnum = 0x7FFFFFFF,

    None = 0
}
