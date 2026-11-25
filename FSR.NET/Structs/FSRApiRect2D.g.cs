namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 16)]
public struct FSRApiRect2D
{
    [FieldOffset(0)]
    public int Left;

    [FieldOffset(4)]
    public int Top;

    [FieldOffset(8)]
    public int Width;

    [FieldOffset(12)]
    public int Height;
}
