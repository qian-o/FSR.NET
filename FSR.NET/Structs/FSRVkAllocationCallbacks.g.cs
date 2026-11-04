namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 48)]
public unsafe struct FSRVkAllocationCallbacks
{
    [FieldOffset(0)]
    public void* UserData;

    [FieldOffset(8)]
    public delegate* unmanaged[Stdcall]<void*, nuint, nuint, FSRVkSystemAllocationScope, void*> PfnAllocation;

    [FieldOffset(16)]
    public delegate* unmanaged[Stdcall]<void*, void*, nuint, nuint, FSRVkSystemAllocationScope, void*> PfnReallocation;

    [FieldOffset(24)]
    public delegate* unmanaged[Stdcall]<void*, void*, void> PfnFree;

    [FieldOffset(32)]
    public delegate* unmanaged[Stdcall]<void*, nuint, FSRVkInternalAllocationType, FSRVkSystemAllocationScope, void> PfnInternalAllocation;

    [FieldOffset(40)]
    public delegate* unmanaged[Stdcall]<void*, nuint, FSRVkInternalAllocationType, FSRVkSystemAllocationScope, void> PfnInternalFree;
}
