namespace FSR.NET;

[Flags]
public enum FSRApiCreateContextRadianceCacheFlags : int
{
    None = 0,

    TryForceWmma = 1 << 0,
}
