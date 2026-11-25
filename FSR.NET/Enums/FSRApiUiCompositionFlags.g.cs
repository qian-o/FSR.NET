namespace FSR.NET;

[Flags]
public enum FSRApiUiCompositionFlags : int
{
    None = 0,

    UsePremulAlpha = 1 << 0,

    EnableInternalUiDoubleBuffering = 1 << 1,
}
