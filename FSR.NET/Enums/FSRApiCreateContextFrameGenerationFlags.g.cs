namespace FSR.NET;

[Flags]
public enum FSRApiCreateContextFrameGenerationFlags : int
{
    AsyncWorkloadSupport = 1,

    DisplayResolutionMotionVectors = 2,

    MotionVectorsJitterCancellation = 4,

    DepthInverted = 8,

    DepthInfinite = 16,

    HighDynamicRange = 32,

    DebugChecking = 64,
}
