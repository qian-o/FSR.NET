namespace FSR.NET;

public enum FSRApiReturnCodes : uint
{
    Ok = 0u,

    Error = 1u,

    ErrorUnknownDescType = 2u,

    ErrorRuntimeError = 3u,

    NoProvider = 4u,

    ErrorMemory = 5u,

    ErrorParameter = 6u,

    ProviderNoSupportNewDescType = 7u,
}
