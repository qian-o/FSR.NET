namespace FSR.NET;

[Flags]
public enum FSRApiCreateContextFrameGenerationFlags : int
{
    AsyncWorkloadSupport = 1 << 0,

    DisplayResolutionMotionVectors = 1 << 1,

    MotionVectorsJitterCancellation = 1 << 2,

    DepthInverted = 1 << 3,

    DepthInfinite = 1 << 4,

    HighDynamicRange = 1 << 5,

    DebugChecking = 1 << 6,

    None = 0
}
