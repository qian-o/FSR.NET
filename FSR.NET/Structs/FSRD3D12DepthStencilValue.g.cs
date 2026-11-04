namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct FSRD3D12DepthStencilValue
{
    [FieldOffset(0)]
    public float Depth;

    [FieldOffset(4)]
    public byte Stencil;
}
