namespace FSR.NET;

[Flags]
public enum FSRApiResourceState : int
{
    Common = 1,

    UnorderedAccess = 2,

    ComputeRead = 4,

    PixelRead = 8,

    PixelComputeRead = 12,

    CopySrc = 16,

    CopyDest = 32,

    GenericRead = 20,

    IndirectArgument = 64,

    Present = 128,

    RenderTarget = 256,

    DepthAttachment = 512,
}
