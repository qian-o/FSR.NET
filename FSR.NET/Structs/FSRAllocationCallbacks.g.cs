namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public unsafe struct FSRAllocationCallbacks
{
    [FieldOffset(0)]
    public void* UserData;

    [FieldOffset(8)]
    public delegate* unmanaged[Cdecl]<void*, ulong, void*> Alloc;

    [FieldOffset(16)]
    public delegate* unmanaged[Cdecl]<void*, void*, void> Dealloc;
}
