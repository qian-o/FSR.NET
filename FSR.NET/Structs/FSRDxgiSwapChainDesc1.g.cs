namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 48)]
public struct FSRDxgiSwapChainDesc1
{
    [FieldOffset(0)]
    public uint Width;

    [FieldOffset(4)]
    public uint Height;

    [FieldOffset(8)]
    public FSRDxgiFormat Format;

    [FieldOffset(12)]
    public int Stereo;

    [FieldOffset(16)]
    public FSRDxgiSampleDesc SampleDesc;

    [FieldOffset(24)]
    public uint BufferUsage;

    [FieldOffset(28)]
    public uint BufferCount;

    [FieldOffset(32)]
    public FSRDxgiScaling Scaling;

    [FieldOffset(36)]
    public FSRDxgiSwapEffect SwapEffect;

    [FieldOffset(40)]
    public FSRDxgiAlphaMode AlphaMode;

    [FieldOffset(44)]
    public uint Flags;
}
