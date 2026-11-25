namespace FSR.NET;

[Flags]
public enum FSRApiCreateContextUpscaleFlags : int
{
    None = 0,

    HighDynamicRange = 1 << 0,

    DisplayResolutionMotionVectors = 1 << 1,

    MotionVectorsJitterCancellation = 1 << 2,

    DepthInverted = 1 << 3,

    DepthInfinite = 1 << 4,

    AutoExposure = 1 << 5,

    DynamicResolution = 1 << 6,

    DebugChecking = 1 << 7,

    NonLinearColorSpace = 1 << 8,

    DebugVisualization = 1 << 9,
}
