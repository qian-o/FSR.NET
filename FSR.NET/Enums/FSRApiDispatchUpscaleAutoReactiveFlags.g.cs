namespace FSR.NET;

[Flags]
public enum FSRApiDispatchUpscaleAutoReactiveFlags : int
{
    ApplyToneMap = 1,

    ApplyInverseToneMap = 2,

    ApplyThreshold = 4,

    UseComponentsMax = 8,
}
