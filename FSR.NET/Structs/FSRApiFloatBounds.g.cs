namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct FSRApiFloatBounds
{
    [FieldOffset(0)]
    public float Min;

    [FieldOffset(4)]
    public float Max;
}
