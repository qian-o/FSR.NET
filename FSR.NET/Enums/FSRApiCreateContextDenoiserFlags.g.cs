namespace FSR.NET;

[Flags]
public enum FSRApiCreateContextDenoiserFlags : int
{
    Debugging = 1 << 0,

    Validation = 1 << 1,

    None = 0
}
