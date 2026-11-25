namespace FSR.NET;

[Flags]
public enum FSRApiResourceUsage : int
{
    ReadOnly = 0,

    RenderTarget = 1,

    Uav = 2,

    DepthTarget = 4,

    Indirect = 8,

    ArrayView = 16,

    StencilTarget = 32,

    DccRenderTarget = 32768,
}
