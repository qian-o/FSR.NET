namespace FSR.NET;

[Flags]
public enum FSRApiDispatchFsrUpscaleFlags : int
{
    DrawDebugView = 1,

    NonLinearColorSrgb = 2,

    NonLinearColorPq = 4,
}
