namespace FSR.NET;

[Flags]
public enum FSRApiResourceUsage : int
{
    ReadOnly = 0,

    RenderTarget = 1 << 0,

    Uav = 1 << 1,

    DepthTarget = 1 << 2,

    Indirect = 1 << 3,

    ArrayView = 1 << 4,

    StencilTarget = 1 << 5,

    DccRenderTarget = 1 << 15,
}
