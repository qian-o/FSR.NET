namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 40)]
public unsafe struct FSRCreateBackendVKDescDeprecated
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public nint VkDevice;

    [FieldOffset(24)]
    public nint VkPhysicalDevice;

    [FieldOffset(32)]
    public delegate* unmanaged[Stdcall]<nint, byte*, delegate* unmanaged[Stdcall]<void>> VkDeviceProcAddr;

    public const ulong Type = Fsr.APICreateContextDescTypeBackendVKDeprecated;

    public FSRCreateBackendVKDescDeprecated()
    {
        this = default;
        Header.Type = Type;
    }
}
