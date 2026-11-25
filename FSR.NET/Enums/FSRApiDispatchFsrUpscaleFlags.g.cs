namespace FSR.NET;

[Flags]
public enum FSRApiDispatchFsrUpscaleFlags : int
{
    None = 0,

    DrawDebugView = 1 << 0,

    NonLinearColorSrgb = 1 << 1,

    NonLinearColorPq = 1 << 2,
}
