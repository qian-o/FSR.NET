namespace FSR.NET;

[Flags]
public enum FSRApiDispatchRadianceCacheFlags : int
{
    DispatchInference = 1 << 0,

    DispatchTraining = 1 << 1,

    ClearInferenceCounter = 1 << 2,

    ClearTrainingCounter = 1 << 3,

    ClearAllCounters = (1 << 2) | (1 << 3),

    Reset = 1 << 4,

    OverrideLearningRate = 1 << 5,

    OverrideWeightSmoothing = 1 << 6,

    None = 0
}
