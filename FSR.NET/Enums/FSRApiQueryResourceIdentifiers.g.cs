namespace FSR.NET;

[Flags]
public enum FSRApiQueryResourceIdentifiers : int
{
    None = 0,

    InputColor = 1 << 0,

    InputDepth = 1 << 1,

    InputMv = 1 << 2,

    InputExposure = 1 << 3,

    InputReactiveMask = 1 << 4,

    InputTransparencyComposition = 1 << 5,
}
