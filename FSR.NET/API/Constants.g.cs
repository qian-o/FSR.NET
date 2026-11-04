namespace FSR.NET;

public static unsafe partial class Fsr
{
    public const ulong APIBackendIdDX12 = 0UL;

    public const ulong APIBackendIdVK = 16777216UL;

    public const ulong APIBackendIdXbox = 33554432UL;

    public const ulong APIBackendMask = 4278190080UL;

    public const ulong APICallbackDescTypeFrameGenerationPresent = 131077UL;

    public const ulong APICallbackDescTypeFrameGenerationPresentPremulAlpha = 131085UL;

    public const ulong APIConfigureDescTypeDenoiserKeyValue = 327713UL;

    public const ulong APIConfigureDescTypeFGSwapChainRegisterUIResourceVK = 262146UL;

    public const ulong APIConfigureDescTypeFrameGeneration = 131074UL;

    public const ulong APIConfigureDescTypeFrameGenerationSwapChainKeyValueDX12 = 196616UL;

    public const ulong APIConfigureDescTypeFrameGenerationSwapChainKeyValueVK = 262152UL;

    public const ulong APIConfigureDescTypeFrameGenerationSwapChainRegisterUIResourceDX12 = 196610UL;

    public const ulong APIConfigureDescTypeFrameGenerationKeyValue = 131078UL;

    public const ulong APIConfigureDescTypeFrameGenerationRegisterDistortionResource = 131080UL;

    public const ulong APIConfigureDescTypeGlobalDebug = 7UL;

    public const ulong APIConfigureDescTypeGlobalDebug1 = 1UL;

    public const ulong APIConfigureDescTypeUpscaleKeyValue = 65543UL;

    public const ulong APIConfigureGlobalDebugLevelErrors = 1UL;

    public const ulong APIConfigureGlobalDebugLevelSilence = 0UL;

    public const ulong APIConfigureGlobalDebugLevelVerbose = 268435455UL;

    public const ulong APIConfigureGlobalDebugLevelWarnings = 2UL;

    public const ulong APICreateContextDescTypeBackendDX12 = 2UL;

    public const ulong APICreateContextDescTypeBackendDX12AllocationCallbacks = 3UL;

    public const ulong APICreateContextDescTypeBackendVK = 16777219UL;

    public const ulong APICreateContextDescTypeBackendVKAllocationCallbacks = 16777220UL;

    public const ulong APICreateContextDescTypeBackendVKDeprecated = 3UL;

    public const ulong APICreateContextDescTypeDenoiser = 327681UL;

    public const ulong APICreateContextDescTypeFGSwapChainModeVK = 262160UL;

    public const ulong APICreateContextDescTypeFGSwapChainVK = 262145UL;

    public const ulong APICreateContextDescTypeFrameGeneration = 131073UL;

    public const ulong APICreateContextDescTypeFrameGenerationSwapChainForHwndDX12 = 196614UL;

    public const ulong APICreateContextDescTypeFrameGenerationSwapChainNewDX12 = 196613UL;

    public const ulong APICreateContextDescTypeFrameGenerationSwapChainVersionDX12 = 196619UL;

    public const ulong APICreateContextDescTypeFrameGenerationSwapChainVersionVK = 262154UL;

    public const ulong APICreateContextDescTypeFrameGenerationSwapChainWrapDX12 = 196609UL;

    public const ulong APICreateContextDescTypeFrameGenerationHudless = 131081UL;

    public const ulong APICreateContextDescTypeFrameGenerationVersion = 131086UL;

    public const ulong APICreateContextDescTypeRadianceCache = 393218UL;

    public const ulong APICreateContextDescTypeUpscale = 65536UL;

    public const ulong APICreateContextDescTypeUpscaleVersion = 65547UL;

    public const ulong APIDebugDispatchDescTypeRadianceCache = 393221UL;

    public const ulong APIDenoiserDebugViewMaxViewports = 12UL;

    public const ulong APIDescTypeOverrideVersion = 5UL;

    public const ulong APIDispatchDescTypeDenoiser = 327745UL;

    public const ulong APIDispatchDescTypeDenoiserAmbientOcclusion = 327747UL;

    public const ulong APIDispatchDescTypeDenoiserDebugView = 327746UL;

    public const ulong APIDispatchDescTypeDenoiserDirectDiffuse = 327748UL;

    public const ulong APIDispatchDescTypeDenoiserDirectSpecular = 327749UL;

    public const ulong APIDispatchDescTypeDenoiserDominantLight = 327750UL;

    public const ulong APIDispatchDescTypeDenoiserIndirectDiffuse = 327751UL;

    public const ulong APIDispatchDescTypeDenoiserIndirectSpecular = 327752UL;

    public const ulong APIDispatchDescTypeDenoiserSpecularOcclusion = 327753UL;

    public const ulong APIDispatchDescTypeFGSwapChainWaitForPresentsVK = 262151UL;

    public const ulong APIDispatchDescTypeFrameGeneration = 131075UL;

    public const ulong APIDispatchDescTypeFrameGenerationSwapChainWaitForPresentsDX12 = 196615UL;

    public const ulong APIDispatchDescTypeFrameGenerationPrepare = 131076UL;

    public const ulong APIDispatchDescTypeFrameGenerationPrepareCameraInfo = 131082UL;

    public const ulong APIDispatchDescTypeFrameGenerationPrepareV2 = 131084UL;

    public const ulong APIDispatchDescTypeRadianceCache = 393220UL;

    public const ulong APIDispatchDescTypeUpscale = 65537UL;

    public const ulong APIDispatchDescTypeUpscaleGenerateReactiveMask = 65542UL;

    public const ulong APIEffectCount = 6UL;

    public const ulong APIEffectIdDenoiser = 327680UL;

    public const ulong APIEffectIdFrameGeneration = 131072UL;

    public const ulong APIEffectIdFrameGenerationSwapChain = 196608UL;

    public const ulong APIEffectIdFrameGenerationSwapChainVK = 262144UL;

    public const ulong APIEffectIdGeneral = 0UL;

    public const ulong APIEffectIdRadianceCache = 393216UL;

    public const ulong APIEffectIdUpscale = 65536UL;

    public const ulong APIEffectMask = 16711680UL;

    public const ulong APIFrameGenerationConfig = 131087UL;

    public const ulong APIQueryDescTypeDenoiserGetDefaultKeyValue = 327809UL;

    public const ulong APIQueryDescTypeDenoiserGPUMemoryUsage = 327810UL;

    public const ulong APIQueryDescTypeFGSwapChainFunctionsVK = 262149UL;

    public const ulong APIQueryDescTypeFGSwapChainInterpolationCommandListVK = 262147UL;

    public const ulong APIQueryDescTypeFGSwapChainInterpolationTextureVK = 262148UL;

    public const ulong APIQueryDescTypeFrameGenerationSwapChainGPUMemoryUsageDX12 = 196617UL;

    public const ulong APIQueryDescTypeFrameGenerationSwapChainGPUMemoryUsageDX12V2 = 196618UL;

    public const ulong APIQueryDescTypeFrameGenerationSwapChainGPUMemoryUsageVK = 262153UL;

    public const ulong APIQueryDescTypeFrameGenerationSwapChainGPUMemoryUsageVKV2 = 262155UL;

    public const ulong APIQueryDescTypeFrameGenerationSwapChainInterpolationCommandListDX12 = 196611UL;

    public const ulong APIQueryDescTypeFrameGenerationSwapChainInterpolationTextureDX12 = 196612UL;

    public const ulong APIQueryDescTypeFrameGenerationGPUMemoryUsage = 131079UL;

    public const ulong APIQueryDescTypeFrameGenerationGPUMemoryUsageV2 = 131083UL;

    public const ulong APIQueryDescTypeGetProviderVersion = 6UL;

    public const ulong APIQueryDescTypeGetVersions = 4UL;

    public const ulong APIQueryDescTypeUpscaleGetJitterOffset = 65541UL;

    public const ulong APIQueryDescTypeUpscaleGetJitterPhaseCount = 65540UL;

    public const ulong APIQueryDescTypeUpscaleGetRenderResolutionFromQualityMode = 65539UL;

    public const ulong APIQueryDescTypeUpscaleGetUpscaleRatioFromQualityMode = 65538UL;

    public const ulong APIQueryDescTypeUpscaleGetResourceRequirements = 65546UL;

    public const ulong APIQueryDescTypeUpscaleGPUMemoryUsage = 65544UL;

    public const ulong APIQueryDescTypeUpscaleGPUMemoryUsageV2 = 65545UL;

    public const ulong DenoiserVersion = 4202497UL;

    public const ulong DenoiserVersionMajor = 1UL;

    public const ulong DenoiserVersionMinor = 2UL;

    public const ulong DenoiserVersionPatch = 1UL;

    public static ReadOnlySpan<byte> FrameGenerationPrepareReplacementSuffix => "PrepareV2\0"u8;

    public const ulong FrameGenerationSwapChainDX12Version = 16777216UL;

    public const ulong FrameGenerationSwapChainDX12VersionMajor = 4UL;

    public const ulong FrameGenerationSwapChainDX12VersionMinor = 0UL;

    public const ulong FrameGenerationSwapChainDX12VersionPatch = 0UL;

    public const ulong FrameGenerationSwapChainVKVersion = 16777216UL;

    public const ulong FrameGenerationSwapChainVKVersionMajor = 4UL;

    public const ulong FrameGenerationSwapChainVKVersionMinor = 0UL;

    public const ulong FrameGenerationSwapChainVKVersionPatch = 0UL;

    public const ulong FrameGenerationVersion = 16777218UL;

    public const ulong FrameGenerationVersionMajor = 4UL;

    public const ulong FrameGenerationVersionMinor = 0UL;

    public const ulong FrameGenerationVersionPatch = 2UL;

    public const ulong RadianceCacheVersion = 36864UL;

    public const ulong RadianceCacheVersionMajor = 0UL;

    public const ulong RadianceCacheVersionMinor = 9UL;

    public const ulong RadianceCacheVersionPatch = 0UL;

    public const ulong ResourceNameSize = 64UL;

    public const ulong UpscalerVersion = 16781314UL;

    public const ulong UpscalerVersionMajor = 4UL;

    public const ulong UpscalerVersionMinor = 1UL;

    public const ulong UpscalerVersionPatch = 2UL;

    public static uint FFX_API_MAKE_EFFECT_SUB_ID(uint effectId, uint subversion)
    {
        return (effectId & (uint)APIEffectMask) | (subversion & ~(uint)APIEffectMask);
    }

    public static uint FFX_API_MAKE_BACKEND_SUB_ID(uint backendId, uint subversion)
    {
        return (backendId & (uint)APIBackendMask) | (subversion & ~(uint)APIBackendMask);
    }

    public static uint FFX_API_MAKE_BACKEND_EFFECT_SUB_ID(uint backendId, uint effectId, uint subversion)
    {
        return (subversion & ~(uint)APIEffectMask) | (effectId & (uint)APIEffectMask) | (backendId & (uint)APIBackendMask) | (subversion & ~((uint)APIBackendMask | (uint)APIEffectMask));
    }

    public static uint FFX_DENOISER_MAKE_VERSION(uint major, uint minor, uint patch)
    {
        return (major << 22) | (minor << 12) | patch;
    }

    public static uint FFX_FRAMEGENERATION_MAKE_VERSION(uint major, uint minor, uint patch)
    {
        return (major << 22) | (minor << 12) | patch;
    }

    public static uint FFX_FRAMEGENERATION_SWAPCHAIN_DX12_MAKE_VERSION(uint major, uint minor, uint patch)
    {
        return (major << 22) | (minor << 12) | patch;
    }

    public static uint FFX_FRAMEGENERATION_SWAPCHAIN_VK_MAKE_VERSION(uint major, uint minor, uint patch)
    {
        return (major << 22) | (minor << 12) | patch;
    }

    public static uint FFX_RADIANCECACHE_MAKE_VERSION(uint major, uint minor, uint patch)
    {
        return (major << 22) | (minor << 12) | patch;
    }

    public static uint FFX_UPSCALER_MAKE_VERSION(uint major, uint minor, uint patch)
    {
        return (major << 22) | (minor << 12) | patch;
    }
}
