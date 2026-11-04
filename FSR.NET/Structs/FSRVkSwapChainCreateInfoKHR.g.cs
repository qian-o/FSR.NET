namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 104)]
public unsafe struct FSRVkSwapChainCreateInfoKHR
{
    [FieldOffset(0)]
    public FSRVkStructureType SType;

    [FieldOffset(8)]
    public void* Next;

    [FieldOffset(16)]
    public uint Flags;

    [FieldOffset(24)]
    public nint Surface;

    [FieldOffset(32)]
    public uint MinImageCount;

    [FieldOffset(36)]
    public FSRVkFormat ImageFormat;

    [FieldOffset(40)]
    public FSRVkColorSpaceKHR ImageColorSpace;

    [FieldOffset(44)]
    public FSRVkExtent2D ImageExtent;

    [FieldOffset(52)]
    public uint ImageArrayLayers;

    [FieldOffset(56)]
    public uint ImageUsage;

    [FieldOffset(60)]
    public FSRVkSharingMode ImageSharingMode;

    [FieldOffset(64)]
    public uint QueueFamilyIndexCount;

    [FieldOffset(72)]
    public uint* QueueFamilyIndices;

    [FieldOffset(80)]
    public FSRVkSurfaceTransformFlagBitsKHR PreTransform;

    [FieldOffset(84)]
    public FSRVkCompositeAlphaFlagBitsKHR CompositeAlpha;

    [FieldOffset(88)]
    public FSRVkPresentModeKHR PresentMode;

    [FieldOffset(92)]
    public uint Clipped;

    [FieldOffset(96)]
    public nint OldSwapChain;
}
