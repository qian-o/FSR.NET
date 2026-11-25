namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 88)]
public unsafe struct FSRVkImageCreateInfo
{
    [FieldOffset(0)]
    public FSRVkStructureType SType;

    [FieldOffset(8)]
    public void* Next;

    [FieldOffset(16)]
    public uint Flags;

    [FieldOffset(20)]
    public FSRVkImageType ImageType;

    [FieldOffset(24)]
    public FSRVkFormat Format;

    [FieldOffset(28)]
    public FSRVkExtent3D Extent;

    [FieldOffset(40)]
    public uint MipLevels;

    [FieldOffset(44)]
    public uint ArrayLayers;

    [FieldOffset(48)]
    public FSRVkSampleCountFlagBits Samples;

    [FieldOffset(52)]
    public FSRVkImageTiling Tiling;

    [FieldOffset(56)]
    public uint Usage;

    [FieldOffset(60)]
    public FSRVkSharingMode SharingMode;

    [FieldOffset(64)]
    public uint QueueFamilyIndexCount;

    [FieldOffset(72)]
    public uint* QueueFamilyIndices;

    [FieldOffset(80)]
    public FSRVkImageLayout InitialLayout;
}
