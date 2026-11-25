namespace FSR.NET;

[Flags]
public enum FSRApiDenoiserSignalFlags : int
{
    None = 0,

    AmbientOcclusion = 1 << 0,

    DirectDiffuse = 1 << 1,

    DirectSpecular = 1 << 2,

    DominantLightVisibility = 1 << 3,

    IndirectDiffuse = 1 << 4,

    IndirectSpecular = 1 << 5,

    SpecularOcclusion = 1 << 6,
}
