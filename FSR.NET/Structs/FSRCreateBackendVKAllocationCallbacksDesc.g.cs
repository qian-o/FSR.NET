namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 40)]
public unsafe struct FSRCreateBackendVKAllocationCallbacksDesc
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public delegate* unmanaged[Cdecl]<uint, FSRApiReturnCodes> PfnFfxResourceAllocator;

    [FieldOffset(24)]
    public delegate* unmanaged[Cdecl]<uint, FSRApiReturnCodes> PfnFfxResourceDeallocator;

    [FieldOffset(32)]
    public delegate* unmanaged[Cdecl]<void*, ulong, FSRApiConstantBufferAllocation> PfnFfxConstantBufferAllocator;

    public const ulong Type = Fsr.APICreateContextDescTypeBackendVKAllocationCallbacks;

    public FSRCreateBackendVKAllocationCallbacksDesc()
    {
        this = default;
        Header.Type = Type;
    }
}
