namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 12)]
public struct FSRApiFloatCoords3D
{
    [FieldOffset(0)]
    public float X;

    [FieldOffset(4)]
    public float Y;

    [FieldOffset(8)]
    public float Z;
}
