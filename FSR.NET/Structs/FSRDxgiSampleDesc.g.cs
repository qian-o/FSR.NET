namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct FSRDxgiSampleDesc
{
    [FieldOffset(0)]
    public uint Count;

    [FieldOffset(4)]
    public uint Quality;
}
