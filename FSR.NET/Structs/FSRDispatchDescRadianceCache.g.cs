namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 280)]
public unsafe struct FSRDispatchDescRadianceCache
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public void* CommandList;

    [FieldOffset(24)]
    public FSRApiResource PredictionInputs;

    [FieldOffset(72)]
    public FSRApiResource PredictionOutputs;

    [FieldOffset(120)]
    public FSRApiResource TrainInputs;

    [FieldOffset(168)]
    public FSRApiResource TrainTargets;

    [FieldOffset(216)]
    public FSRApiResource SampleCounters;

    [FieldOffset(264)]
    public uint Flags;

    [FieldOffset(268)]
    public FSRDispatchDescRadianceCacheOverrides Overrides;

    public const ulong Type = Fsr.APIDispatchDescTypeRadianceCache;

    public FSRDispatchDescRadianceCache()
    {
        this = default;
        Header.Type = Type;
    }
}
