namespace FSR.NET;

[Flags]
public enum FSRApiResourceState : int
{
    None = 0,

    Common = 1 << 0,

    UnorderedAccess = 1 << 1,

    ComputeRead = 1 << 2,

    PixelRead = 1 << 3,

    PixelComputeRead = (1 << 2) | (1 << 3),

    CopySrc = 1 << 4,

    CopyDest = 1 << 5,

    GenericRead = (1 << 2) | (1 << 4),

    IndirectArgument = 1 << 6,

    Present = 1 << 7,

    RenderTarget = 1 << 8,

    DepthAttachment = 1 << 9,
}
