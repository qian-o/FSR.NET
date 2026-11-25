namespace FSR.NET;

public enum FSRVkResult : int
{
    Success = 0,

    NotReady = 1,

    Timeout = 2,

    EventSet = 3,

    EventReset = 4,

    Incomplete = 5,

    ErrorOutOfHostMemory = -1,

    ErrorOutOfDeviceMemory = -2,

    ErrorInitializationFailed = -3,

    ErrorDeviceLost = -4,

    ErrorMemoryMapFailed = -5,

    ErrorLayerNotPresent = -6,

    ErrorExtensionNotPresent = -7,

    ErrorFeatureNotPresent = -8,

    ErrorIncompatibleDriver = -9,

    ErrorTooManyObjects = -10,

    ErrorFormatNotSupported = -11,

    ErrorFragmentedPool = -12,

    ErrorUnknown = -13,

    ErrorOutOfPoolMemory = -1000069000,

    ErrorInvalidExternalHandle = -1000072003,

    ErrorFragmentation = -1000161000,

    ErrorInvalidOpaqueCaptureAddress = -1000257000,

    PipelineCompileRequired = 1000297000,

    ErrorNotPermitted = -1000174001,

    ErrorSurfaceLostKhr = -1000000000,

    ErrorNativeWindowInUseKhr = -1000000001,

    SuboptimalKhr = 1000001003,

    ErrorOutOfDateKhr = -1000001004,

    ErrorIncompatibleDisplayKhr = -1000003001,

    ErrorValidationFailedExt = -1000011001,

    ErrorInvalidShaderNv = -1000012000,

    ErrorImageUsageNotSupportedKhr = -1000023000,

    ErrorVideoPictureLayoutNotSupportedKhr = -1000023001,

    ErrorVideoProfileOperationNotSupportedKhr = -1000023002,

    ErrorVideoProfileFormatNotSupportedKhr = -1000023003,

    ErrorVideoProfileCodecNotSupportedKhr = -1000023004,

    ErrorVideoStdVersionNotSupportedKhr = -1000023005,

    ErrorInvalidDrmFormatModifierPlaneLayoutExt = -1000158000,

    ErrorFullScreenExclusiveModeLostExt = -1000255000,

    ThreadIdleKhr = 1000268000,

    ThreadDoneKhr = 1000268001,

    OperationDeferredKhr = 1000268002,

    OperationNotDeferredKhr = 1000268003,

    ErrorInvalidVideoStdParametersKhr = -1000299000,

    ErrorCompressionExhaustedExt = -1000338000,

    IncompatibleShaderBinaryExt = 1000482000,

    PipelineBinaryMissingKhr = 1000483000,

    ErrorNotEnoughSpaceKhr = -1000483000,

    ErrorOutOfPoolMemoryKhr = -1000069000,

    ErrorInvalidExternalHandleKhr = -1000072003,

    ErrorFragmentationExt = -1000161000,

    ErrorNotPermittedExt = -1000174001,

    ErrorNotPermittedKhr = -1000174001,

    ErrorInvalidDeviceAddressExt = -1000257000,

    ErrorInvalidOpaqueCaptureAddressKhr = -1000257000,

    PipelineCompileRequiredExt = 1000297000,

    ErrorPipelineCompileRequiredExt = 1000297000,

    ErrorIncompatibleShaderBinaryExt = 1000482000,

    ResultMaxEnum = 2147483647,
}
