namespace FSR.NET;

[Flags]
public enum FSRApiCreateContextDenoiserFlags : int
{
    None = 0,

    Debugging = 1 << 0,

    Validation = 1 << 1,
}
