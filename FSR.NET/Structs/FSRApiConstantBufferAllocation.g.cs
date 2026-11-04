namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 56)]
public struct FSRApiConstantBufferAllocation
{
    [FieldOffset(0)]
    public FSRApiResource Resource;

    [FieldOffset(48)]
    public ulong Handle;
}
