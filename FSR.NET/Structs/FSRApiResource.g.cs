namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 48)]
public unsafe struct FSRApiResource
{
    [FieldOffset(0)]
    public void* Resource;

    [FieldOffset(8)]
    public FSRApiResourceDescription Description;

    [FieldOffset(40)]
    public uint State;
}
