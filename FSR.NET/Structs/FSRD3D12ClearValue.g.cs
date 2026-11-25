namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 20)]
public struct FSRD3D12ClearValue
{
    [FieldOffset(0)]
    public FSRDxgiFormat Format;

    [FieldOffset(4)]
    public ColorBuffer Color;

    [FieldOffset(4)]
    public FSRD3D12DepthStencilValue DepthStencil;

    [InlineArray(4)]
    public struct ColorBuffer
    {
        private float element;
    }
}
