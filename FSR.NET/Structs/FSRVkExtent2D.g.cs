namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct FSRVkExtent2D
{
    [FieldOffset(0)]
    public uint Width;

    [FieldOffset(4)]
    public uint Height;
}
