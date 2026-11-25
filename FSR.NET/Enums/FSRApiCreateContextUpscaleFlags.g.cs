namespace FSR.NET;

[Flags]
public enum FSRApiCreateContextUpscaleFlags : int
{
    HighDynamicRange = 1,

    DisplayResolutionMotionVectors = 2,

    MotionVectorsJitterCancellation = 4,

    DepthInverted = 8,

    DepthInfinite = 16,

    AutoExposure = 32,

    DynamicResolution = 64,

    DebugChecking = 128,

    NonLinearColorSpace = 256,

    DebugVisualization = 512,
}
