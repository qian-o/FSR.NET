namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 48)]
public struct FSRD3D12HeapDesc
{
    [FieldOffset(0)]
    public ulong SizeInBytes;

    [FieldOffset(8)]
    public FSRD3D12HeapProperties Properties;

    [FieldOffset(32)]
    public ulong Alignment;

    [FieldOffset(40)]
    public FSRD3D12HeapFlags Flags;
}
