namespace FSR.NET;

[Flags]
public enum FSRApiDispatchFrameGenerationFlags : int
{
    DrawDebugTearLines = 1 << 0,

    DrawDebugResetIndicators = 1 << 1,

    DrawDebugView = 1 << 2,

    NoSwapChainContextNotify = 1 << 3,

    DrawDebugPacingLines = 1 << 4,

    Reserved1 = 1 << 5,

    Reserved2 = 1 << 6,

    None = 0
}
