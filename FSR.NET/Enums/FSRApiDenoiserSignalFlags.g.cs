namespace FSR.NET;

[Flags]
public enum FSRApiDenoiserSignalFlags : int
{
    None = 0,

    AmbientOcclusion = 1,

    DirectDiffuse = 2,

    DirectSpecular = 4,

    DominantLightVisibility = 8,

    IndirectDiffuse = 16,

    IndirectSpecular = 32,

    SpecularOcclusion = 64,
}
