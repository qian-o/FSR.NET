namespace FSR.NET;

[Flags]
public enum FSRApiDispatchRadianceCacheFlags : int
{
    DispatchInference = 1,

    DispatchTraining = 2,

    ClearInferenceCounter = 4,

    ClearTrainingCounter = 8,

    ClearAllCounters = 12,

    Reset = 16,

    OverrideLearningRate = 32,

    OverrideWeightSmoothing = 64,
}
