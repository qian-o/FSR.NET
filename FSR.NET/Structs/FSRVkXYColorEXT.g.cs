namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct FSRVkXYColorEXT
{
    [FieldOffset(0)]
    public float X;

    [FieldOffset(4)]
    public float Y;
}
