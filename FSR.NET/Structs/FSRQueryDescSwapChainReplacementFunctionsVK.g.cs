namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 72)]
public unsafe struct FSRQueryDescSwapChainReplacementFunctionsVK
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public delegate* unmanaged[Cdecl]<nint, FSRVkSwapChainCreateInfoKHR*, FSRVkAllocationCallbacks*, nint*, void*, FSRVkResult> CreateSwapChainFFXAPI;

    [FieldOffset(24)]
    public delegate* unmanaged[Cdecl]<nint, nint, FSRVkAllocationCallbacks*, void*, void> DestroySwapChainFFXAPI;

    [FieldOffset(32)]
    public delegate* unmanaged[Stdcall]<nint, nint, uint*, nint*, FSRVkResult> GetSwapChainImagesKHR;

    [FieldOffset(40)]
    public delegate* unmanaged[Stdcall]<nint, nint, ulong, nint, nint, uint*, FSRVkResult> AcquireNextImageKHR;

    [FieldOffset(48)]
    public delegate* unmanaged[Stdcall]<nint, FSRVkPresentInfoKHR*, FSRVkResult> QueuePresentKHR;

    [FieldOffset(56)]
    public delegate* unmanaged[Stdcall]<nint, uint, nint*, FSRVkHdrMetadataEXT*, void> SetHdrMetadataEXT;

    [FieldOffset(64)]
    public delegate* unmanaged[Cdecl]<nint, ulong> GetLastPresentCountFFXAPI;

    public const ulong Type = Fsr.APIQueryDescTypeFGSwapChainFunctionsVK;

    public FSRQueryDescSwapChainReplacementFunctionsVK()
    {
        this = default;
        Header.Type = Type;
    }
}
