namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 72)]
public unsafe struct FSRVkSubmitInfo
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
    public uint* WaitDstStageMask;

    [FieldOffset(40)]
    public uint CommandBufferCount;

    [FieldOffset(48)]
    public nint* CommandBuffers;

    [FieldOffset(56)]
    public uint SignalSemaphoreCount;

    [FieldOffset(64)]
    public nint* SignalSemaphores;
}
