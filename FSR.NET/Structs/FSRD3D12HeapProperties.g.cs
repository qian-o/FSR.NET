namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 20)]
public struct FSRD3D12HeapProperties
{
    [FieldOffset(0)]
    public FSRD3D12HeapType Type;

    [FieldOffset(4)]
    public FSRD3D12CpuPageProperty CPUPageProperty;

    [FieldOffset(8)]
    public FSRD3D12MemoryPool MemoryPoolPreference;

    [FieldOffset(12)]
    public uint CreationNodeMask;

    [FieldOffset(16)]
    public uint VisibleNodeMask;
}
