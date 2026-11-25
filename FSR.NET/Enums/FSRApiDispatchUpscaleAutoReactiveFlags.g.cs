namespace FSR.NET;

[Flags]
public enum FSRApiDispatchUpscaleAutoReactiveFlags : int
{
    None = 0,

    ApplyToneMap = 1 << 0,

    ApplyInverseToneMap = 1 << 1,

    ApplyThreshold = 1 << 2,

    UseComponentsMax = 1 << 3,
}
