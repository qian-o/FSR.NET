namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 20)]
public struct FSRDxgiSwapChainFullscreenDesc
{
    [FieldOffset(0)]
    public FSRDxgiRational RefreshRate;

    [FieldOffset(8)]
    public FSRDxgiModeScanlineOrder ScanlineOrdering;

    [FieldOffset(12)]
    public FSRDxgiModeScaling Scaling;

    [FieldOffset(16)]
    public int Windowed;
}
