namespace FSR.NET;

[Flags]
public enum FSRApiDispatchFsrUpscaleFlags : int
{
    DrawDebugView = 1 << 0,

    NonLinearColorSrgb = 1 << 1,

    NonLinearColorPq = 1 << 2,

    None = 0
}
