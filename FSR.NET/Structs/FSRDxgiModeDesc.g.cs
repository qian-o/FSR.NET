namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 28)]
public struct FSRDxgiModeDesc
{
    [FieldOffset(0)]
    public uint Width;

    [FieldOffset(4)]
    public uint Height;

    [FieldOffset(8)]
    public FSRDxgiRational RefreshRate;

    [FieldOffset(16)]
    public FSRDxgiFormat Format;

    [FieldOffset(20)]
    public FSRDxgiModeScanlineOrder ScanlineOrdering;

    [FieldOffset(24)]
    public FSRDxgiModeScaling Scaling;
}
