namespace FSR.NET;

[Flags]
public enum FSRApiUiCompositionFlags : int
{
    UsePremulAlpha = 1 << 0,

    EnableInternalUiDoubleBuffering = 1 << 1,

    None = 0
}
