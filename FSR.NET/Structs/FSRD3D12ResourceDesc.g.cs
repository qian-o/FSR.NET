namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 56)]
public struct FSRD3D12ResourceDesc
{
    [FieldOffset(0)]
    public FSRD3D12ResourceDimension Dimension;

    [FieldOffset(8)]
    public ulong Alignment;

    [FieldOffset(16)]
    public ulong Width;

    [FieldOffset(24)]
    public uint Height;

    [FieldOffset(28)]
    public ushort DepthOrArraySize;

    [FieldOffset(30)]
    public ushort MipLevels;

    [FieldOffset(32)]
    public FSRDxgiFormat Format;

    [FieldOffset(36)]
    public FSRDxgiSampleDesc SampleDesc;

    [FieldOffset(44)]
    public FSRD3D12TextureLayout Layout;

    [FieldOffset(48)]
    public FSRD3D12ResourceFlags Flags;
}
