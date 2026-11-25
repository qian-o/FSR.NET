namespace FSR.NET;

[Flags]
public enum FSRVkImageCreateFlagBits : int
{
    SparseBindingBit = 1,

    SparseResidencyBit = 2,

    SparseAliasedBit = 4,

    MutableFormatBit = 8,

    CubeCompatibleBit = 16,

    AliasBit = 1024,

    SplitInstanceBindRegionsBit = 64,

    _2DArrayCompatibleBit = 32,

    BlockTexelViewCompatibleBit = 128,

    ExtendedUsageBit = 256,

    ProtectedBit = 2048,

    DisjointBit = 512,

    CornerSampledBitNv = 8192,

    SampleLocationsCompatibleDepthBitExt = 4096,

    SubsampledBitExt = 16384,

    DescriptorBufferCaptureReplayBitExt = 65536,

    MultisampledRenderToSingleSampledBitExt = 262144,

    _2DViewCompatibleBitExt = 131072,

    VideoProfileIndependentBitKhr = 1048576,

    FragmentDensityMapOffsetBitExt = 32768,

    SplitInstanceBindRegionsBitKhr = 64,

    _2DArrayCompatibleBitKhr = 32,

    BlockTexelViewCompatibleBitKhr = 128,

    ExtendedUsageBitKhr = 256,

    DisjointBitKhr = 512,

    AliasBitKhr = 1024,

    FragmentDensityMapOffsetBitQcom = 32768,

    FlagBitsMaxEnum = 2147483647,
}
