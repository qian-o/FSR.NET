namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct FSRDxgiRational
{
    [FieldOffset(0)]
    public uint Numerator;

    [FieldOffset(4)]
    public uint Denominator;
}
