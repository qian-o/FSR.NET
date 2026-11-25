namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 56)]
public unsafe struct FSRVkBufferCreateInfo
{
    [FieldOffset(0)]
    public FSRVkStructureType SType;

    [FieldOffset(8)]
    public void* Next;

    [FieldOffset(16)]
    public uint Flags;

    [FieldOffset(24)]
    public ulong Size;

    [FieldOffset(32)]
    public uint Usage;

    [FieldOffset(36)]
    public FSRVkSharingMode SharingMode;

    [FieldOffset(40)]
    public uint QueueFamilyIndexCount;

    [FieldOffset(48)]
    public uint* QueueFamilyIndices;
}
