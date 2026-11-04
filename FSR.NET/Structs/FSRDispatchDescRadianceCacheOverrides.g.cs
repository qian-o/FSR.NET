namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct FSRDispatchDescRadianceCacheOverrides
{
    [FieldOffset(0)]
    public float LearningRate;

    [FieldOffset(4)]
    public float WeightSmoothing;
}
