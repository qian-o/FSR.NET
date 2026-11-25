namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 248)]
public unsafe struct FSRCreateContextDescFrameGenerationSwapChainVK
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public nint PhysicalDevice;

    [FieldOffset(24)]
    public nint Device;

    [FieldOffset(32)]
    public nint* SwapChain;

    [FieldOffset(40)]
    public FSRVkAllocationCallbacks* Allocator;

    [FieldOffset(48)]
    public FSRVkSwapChainCreateInfoKHR CreateInfo;

    [FieldOffset(152)]
    public FSRVkQueueInfoFFXAPI GameQueue;

    [FieldOffset(176)]
    public FSRVkQueueInfoFFXAPI AsyncComputeQueue;

    [FieldOffset(200)]
    public FSRVkQueueInfoFFXAPI PresentQueue;

    [FieldOffset(224)]
    public FSRVkQueueInfoFFXAPI ImageAcquireQueue;

    public const ulong Type = Fsr.APICreateContextDescTypeFGSwapChainVK;

    public FSRCreateContextDescFrameGenerationSwapChainVK()
    {
        this = default;
        Header.Type = Type;
    }
}
