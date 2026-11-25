namespace FSR.NET;

[Flags]
public enum FSRVkImageCreateFlagBits : int
{
    None = 0,

    SparseBindingBit = 1 << 0,

    SparseResidencyBit = 1 << 1,

    SparseAliasedBit = 1 << 2,

    MutableFormatBit = 1 << 3,

    CubeCompatibleBit = 1 << 4,

    AliasBit = 1 << 10,

    SplitInstanceBindRegionsBit = 1 << 6,

    _2DArrayCompatibleBit = 1 << 5,

    BlockTexelViewCompatibleBit = 1 << 7,

    ExtendedUsageBit = 1 << 8,

    ProtectedBit = 1 << 11,

    DisjointBit = 1 << 9,

    CornerSampledBitNv = 1 << 13,

    DescriptorHeapCaptureReplayBitExt = 1 << 16,

    SampleLocationsCompatibleDepthBitExt = 1 << 12,

    SubsampledBitExt = 1 << 14,

    MultisampledRenderToSingleSampledBitExt = 1 << 18,

    _2DViewCompatibleBitExt = 1 << 17,

    VideoProfileIndependentBitKhr = 1 << 20,

    FragmentDensityMapOffsetBitExt = 1 << 15,

    AliasSingleLayerDescriptorBitKhr = 1 << 22,

    SplitInstanceBindRegionsBitKhr = 1 << 6,

    _2DArrayCompatibleBitKhr = 1 << 5,

    BlockTexelViewCompatibleBitKhr = 1 << 7,

    ExtendedUsageBitKhr = 1 << 8,

    DisjointBitKhr = 1 << 9,

    AliasBitKhr = 1 << 10,

    DescriptorBufferCaptureReplayBitExt = 1 << 16,

    FragmentDensityMapOffsetBitQcom = 1 << 15,

    MaxEnum = 0x7FFFFFFF,
}
