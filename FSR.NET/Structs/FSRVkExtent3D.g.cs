namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 12)]
public struct FSRVkExtent3D
{
    [FieldOffset(0)]
    public uint Width;

    [FieldOffset(4)]
    public uint Height;

    [FieldOffset(8)]
    public uint Depth;
}
