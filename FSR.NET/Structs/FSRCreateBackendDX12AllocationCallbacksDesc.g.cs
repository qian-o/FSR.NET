namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 56)]
public unsafe struct FSRCreateBackendDX12AllocationCallbacksDesc
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public delegate* unmanaged[Cdecl]<uint, FSRD3D12ResourceStates, FSRD3D12HeapProperties*, FSRD3D12ResourceDesc*, FSRApiResourceDescription*, FSRD3D12ClearValue*, nint*, FSRApiReturnCodes> PfnFfxResourceAllocator;

    [FieldOffset(24)]
    public delegate* unmanaged[Cdecl]<uint, nint, FSRApiReturnCodes> PfnFfxResourceDeallocator;

    [FieldOffset(32)]
    public delegate* unmanaged[Cdecl]<uint, FSRD3D12HeapDesc*, byte, nint*, ulong*, FSRApiReturnCodes> PfnFfxHeapAllocator;

    [FieldOffset(40)]
    public delegate* unmanaged[Cdecl]<uint, nint, ulong, ulong, FSRApiReturnCodes> PfnFfxHeapDeallocator;

    [FieldOffset(48)]
    public delegate* unmanaged[Cdecl]<void*, ulong, FSRApiConstantBufferAllocation> PfnFfxConstantBufferAllocator;

    public const ulong Type = Fsr.APICreateContextDescTypeBackendDX12AllocationCallbacks;

    public FSRCreateBackendDX12AllocationCallbacksDesc()
    {
        this = default;
        Header.Type = Type;
    }
}
