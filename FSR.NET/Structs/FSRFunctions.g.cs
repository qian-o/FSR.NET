namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 40)]
public unsafe struct FSRFunctions
{
    [FieldOffset(0)]
    public delegate* unmanaged[Cdecl]<FSRContext*, FSRApiHeader*, FSRAllocationCallbacks*, FSRApiReturnCodes> CreateContext;

    [FieldOffset(8)]
    public delegate* unmanaged[Cdecl]<FSRContext*, FSRAllocationCallbacks*, FSRApiReturnCodes> DestroyContext;

    [FieldOffset(16)]
    public delegate* unmanaged[Cdecl]<FSRContext*, FSRApiHeader*, FSRApiReturnCodes> Configure;

    [FieldOffset(24)]
    public delegate* unmanaged[Cdecl]<FSRContext*, FSRApiHeader*, FSRApiReturnCodes> Query;

    [FieldOffset(32)]
    public delegate* unmanaged[Cdecl]<FSRContext*, FSRApiHeader*, FSRApiReturnCodes> Dispatch;
}
