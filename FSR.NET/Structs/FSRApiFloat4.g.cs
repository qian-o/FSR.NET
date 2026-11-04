namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 16)]
public struct FSRApiFloat4
{
    [FieldOffset(0)]
    public float X;

    [FieldOffset(4)]
    public float Y;

    [FieldOffset(8)]
    public float Z;

    [FieldOffset(12)]
    public float W;
}
