namespace FSR.NET;

public enum FSRVkImageLayout : int
{
    Undefined = 0,

    General = 1,

    ColorAttachmentOptimal = 2,

    DepthStencilAttachmentOptimal = 3,

    DepthStencilReadOnlyOptimal = 4,

    ShaderReadOnlyOptimal = 5,

    TransferSrcOptimal = 6,

    TransferDstOptimal = 7,

    Preinitialized = 8,

    DepthReadOnlyStencilAttachmentOptimal = 1000117000,

    DepthAttachmentStencilReadOnlyOptimal = 1000117001,

    DepthAttachmentOptimal = 1000241000,

    DepthReadOnlyOptimal = 1000241001,

    StencilAttachmentOptimal = 1000241002,

    StencilReadOnlyOptimal = 1000241003,

    ReadOnlyOptimal = 1000314000,

    AttachmentOptimal = 1000314001,

    RenderingLocalRead = 1000232000,

    PresentSrcKhr = 1000001002,

    VideoDecodeDstKhr = 1000024000,

    VideoDecodeSrcKhr = 1000024001,

    VideoDecodeDpbKhr = 1000024002,

    SharedPresentKhr = 1000111000,

    FragmentDensityMapOptimalExt = 1000218000,

    FragmentShadingRateAttachmentOptimalKhr = 1000164003,

    VideoEncodeDstKhr = 1000299000,

    VideoEncodeSrcKhr = 1000299001,

    VideoEncodeDpbKhr = 1000299002,

    AttachmentFeedbackLoopOptimalExt = 1000339000,

    TensorAliasingArm = 1000460000,

    VideoEncodeQuantizationMapKhr = 1000553000,

    ZeroInitializedExt = 1000620000,

    DepthReadOnlyStencilAttachmentOptimalKhr = 1000117000,

    DepthAttachmentStencilReadOnlyOptimalKhr = 1000117001,

    ShadingRateOptimalNv = 1000164003,

    RenderingLocalReadKhr = 1000232000,

    DepthAttachmentOptimalKhr = 1000241000,

    DepthReadOnlyOptimalKhr = 1000241001,

    StencilAttachmentOptimalKhr = 1000241002,

    StencilReadOnlyOptimalKhr = 1000241003,

    ReadOnlyOptimalKhr = 1000314000,

    AttachmentOptimalKhr = 1000314001,

    MaxEnum = 2147483647,
}
