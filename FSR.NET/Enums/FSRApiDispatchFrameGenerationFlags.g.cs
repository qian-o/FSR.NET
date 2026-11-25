namespace FSR.NET;

[Flags]
public enum FSRApiDispatchFrameGenerationFlags : int
{
    DrawDebugTearLines = 1,

    DrawDebugResetIndicators = 2,

    DrawDebugView = 4,

    NoSwapChainContextNotify = 8,

    DrawDebugPacingLines = 16,

    Reserved1 = 32,

    Reserved2 = 64,
}
