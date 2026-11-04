namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 32)]
public struct FSRApiResourceDescription
{
    [FieldOffset(0)]
    public uint Type;

    [FieldOffset(4)]
    public uint Format;

    [FieldOffset(8)]
    public uint Width;

    [FieldOffset(8)]
    public uint Size;

    [FieldOffset(12)]
    public uint Height;

    [FieldOffset(12)]
    public uint Stride;

    [FieldOffset(16)]
    public uint Depth;

    [FieldOffset(16)]
    public uint Alignment;

    [FieldOffset(20)]
    public uint MipCount;

    [FieldOffset(24)]
    public uint Flags;

    [FieldOffset(28)]
    public uint Usage;
}
