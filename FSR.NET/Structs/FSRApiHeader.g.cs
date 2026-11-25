namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 16)]
public unsafe struct FSRApiHeader
{
    [FieldOffset(0)]
    public ulong Type;

    [FieldOffset(8)]
    public FSRApiHeader* Next;
}
