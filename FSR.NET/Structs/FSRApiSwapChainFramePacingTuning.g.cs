namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 20)]
public struct FSRApiSwapChainFramePacingTuning
{
    [FieldOffset(0)]
    public float SafetyMarginInMs;

    [FieldOffset(4)]
    public float VarianceFactor;

    [FieldOffset(8)]
    public Bool8 AllowHybridSpin;

    [FieldOffset(12)]
    public uint HybridSpinTime;

    [FieldOffset(16)]
    public Bool8 AllowWaitForSingleObjectOnFence;
}
