namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 64)]
public unsafe struct FSRVkPresentInfoKHR
{
    [FieldOffset(0)]
    public FSRVkStructureType SType;

    [FieldOffset(8)]
    public void* Next;

    [FieldOffset(16)]
    public uint WaitSemaphoreCount;

    [FieldOffset(24)]
    public nint* WaitSemaphores;

    [FieldOffset(32)]
    public uint SwapChainCount;

    [FieldOffset(40)]
    public nint* SwapChains;

    [FieldOffset(48)]
    public uint* ImageIndices;

    [FieldOffset(56)]
    public FSRVkResult* Results;
}
