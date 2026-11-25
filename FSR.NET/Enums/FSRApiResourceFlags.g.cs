namespace FSR.NET;

[Flags]
public enum FSRApiResourceFlags : int
{
    None = 0,

    Aliasable = 1 << 0,

    Undefined = 1 << 1,
}
