namespace FSR.NET;

[Flags]
public enum FSRApiDispatchDenoiserFlags : int
{
    Reset = 1 << 0,

    NonEncodedAlbedo = 1 << 1,

    NonGammaAlbedo = 1 << 1,

    None = 0
}
