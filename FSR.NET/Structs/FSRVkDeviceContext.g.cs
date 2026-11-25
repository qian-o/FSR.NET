namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public unsafe struct FSRVkDeviceContext
{
    [FieldOffset(0)]
    public nint VkDevice;

    [FieldOffset(8)]
    public nint VkPhysicalDevice;

    [FieldOffset(16)]
    public delegate* unmanaged[Stdcall]<nint, byte*, delegate* unmanaged[Stdcall]<void>> VkDeviceProcAddr;
}
