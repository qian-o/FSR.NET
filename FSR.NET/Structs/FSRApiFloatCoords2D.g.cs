namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct FSRApiFloatCoords2D
{
    [FieldOffset(0)]
    public float X;

    [FieldOffset(4)]
    public float Y;
}
