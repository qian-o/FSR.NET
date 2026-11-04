namespace FSR.NET;

public enum FSRVkStructureType : int
{
    ApplicationInfo = 0,

    InstanceCreateInfo = 1,

    DeviceQueueCreateInfo = 2,

    DeviceCreateInfo = 3,

    SubmitInfo = 4,

    MemoryAllocateInfo = 5,

    MappedMemoryRange = 6,

    BindSparseInfo = 7,

    FenceCreateInfo = 8,

    SemaphoreCreateInfo = 9,

    EventCreateInfo = 10,

    QueryPoolCreateInfo = 11,

    BufferCreateInfo = 12,

    BufferViewCreateInfo = 13,

    ImageCreateInfo = 14,

    ImageViewCreateInfo = 15,

    ShaderModuleCreateInfo = 16,

    PipelineCacheCreateInfo = 17,

    PipelineShaderStageCreateInfo = 18,

    PipelineVertexInputStateCreateInfo = 19,

    PipelineInputAssemblyStateCreateInfo = 20,

    PipelineTessellationStateCreateInfo = 21,

    PipelineViewportStateCreateInfo = 22,

    PipelineRasterizationStateCreateInfo = 23,

    PipelineMultisampleStateCreateInfo = 24,

    PipelineDepthStencilStateCreateInfo = 25,

    PipelineColorBlendStateCreateInfo = 26,

    PipelineDynamicStateCreateInfo = 27,

    GraphicsPipelineCreateInfo = 28,

    ComputePipelineCreateInfo = 29,

    PipelineLayoutCreateInfo = 30,

    SamplerCreateInfo = 31,

    DescriptorSetLayoutCreateInfo = 32,

    DescriptorPoolCreateInfo = 33,

    DescriptorSetAllocateInfo = 34,

    WriteDescriptorSet = 35,

    CopyDescriptorSet = 36,

    FramebufferCreateInfo = 37,

    RenderPassCreateInfo = 38,

    CommandPoolCreateInfo = 39,

    CommandBufferAllocateInfo = 40,

    CommandBufferInheritanceInfo = 41,

    CommandBufferBeginInfo = 42,

    RenderPassBeginInfo = 43,

    BufferMemoryBarrier = 44,

    ImageMemoryBarrier = 45,

    MemoryBarrier = 46,

    LoaderInstanceCreateInfo = 47,

    LoaderDeviceCreateInfo = 48,

    PhysicalDeviceSubgroupProperties = 1000094000,

    BindBufferMemoryInfo = 1000157000,

    BindImageMemoryInfo = 1000157001,

    PhysicalDevice16BitStorageFeatures = 1000083000,

    MemoryDedicatedRequirements = 1000127000,

    MemoryDedicatedAllocateInfo = 1000127001,

    MemoryAllocateFlagsInfo = 1000060000,

    DeviceGroupRenderPassBeginInfo = 1000060003,

    DeviceGroupCommandBufferBeginInfo = 1000060004,

    DeviceGroupSubmitInfo = 1000060005,

    DeviceGroupBindSparseInfo = 1000060006,

    BindBufferMemoryDeviceGroupInfo = 1000060013,

    BindImageMemoryDeviceGroupInfo = 1000060014,

    PhysicalDeviceGroupProperties = 1000070000,

    DeviceGroupDeviceCreateInfo = 1000070001,

    BufferMemoryRequirementsInfo2 = 1000146000,

    ImageMemoryRequirementsInfo2 = 1000146001,

    ImageSparseMemoryRequirementsInfo2 = 1000146002,

    MemoryRequirements2 = 1000146003,

    SparseImageMemoryRequirements2 = 1000146004,

    PhysicalDeviceFeatures2 = 1000059000,

    PhysicalDeviceProperties2 = 1000059001,

    FormatProperties2 = 1000059002,

    ImageFormatProperties2 = 1000059003,

    PhysicalDeviceImageFormatInfo2 = 1000059004,

    QueueFamilyProperties2 = 1000059005,

    PhysicalDeviceMemoryProperties2 = 1000059006,

    SparseImageFormatProperties2 = 1000059007,

    PhysicalDeviceSparseImageFormatInfo2 = 1000059008,

    PhysicalDevicePointClippingProperties = 1000117000,

    RenderPassInputAttachmentAspectCreateInfo = 1000117001,

    ImageViewUsageCreateInfo = 1000117002,

    PipelineTessellationDomainOriginStateCreateInfo = 1000117003,

    RenderPassMultiviewCreateInfo = 1000053000,

    PhysicalDeviceMultiviewFeatures = 1000053001,

    PhysicalDeviceMultiviewProperties = 1000053002,

    PhysicalDeviceVariablePointersFeatures = 1000120000,

    ProtectedSubmitInfo = 1000145000,

    PhysicalDeviceProtectedMemoryFeatures = 1000145001,

    PhysicalDeviceProtectedMemoryProperties = 1000145002,

    DeviceQueueInfo2 = 1000145003,

    SamplerYcbcrConversionCreateInfo = 1000156000,

    SamplerYcbcrConversionInfo = 1000156001,

    BindImagePlaneMemoryInfo = 1000156002,

    ImagePlaneMemoryRequirementsInfo = 1000156003,

    PhysicalDeviceSamplerYcbcrConversionFeatures = 1000156004,

    SamplerYcbcrConversionImageFormatProperties = 1000156005,

    DescriptorUpdateTemplateCreateInfo = 1000085000,

    PhysicalDeviceExternalImageFormatInfo = 1000071000,

    ExternalImageFormatProperties = 1000071001,

    PhysicalDeviceExternalBufferInfo = 1000071002,

    ExternalBufferProperties = 1000071003,

    PhysicalDeviceIdProperties = 1000071004,

    ExternalMemoryBufferCreateInfo = 1000072000,

    ExternalMemoryImageCreateInfo = 1000072001,

    ExportMemoryAllocateInfo = 1000072002,

    PhysicalDeviceExternalFenceInfo = 1000112000,

    ExternalFenceProperties = 1000112001,

    ExportFenceCreateInfo = 1000113000,

    ExportSemaphoreCreateInfo = 1000077000,

    PhysicalDeviceExternalSemaphoreInfo = 1000076000,

    ExternalSemaphoreProperties = 1000076001,

    PhysicalDeviceMaintenance3Properties = 1000168000,

    DescriptorSetLayoutSupport = 1000168001,

    PhysicalDeviceShaderDrawParametersFeatures = 1000063000,

    PhysicalDeviceVulkan11Features = 49,

    PhysicalDeviceVulkan11Properties = 50,

    PhysicalDeviceVulkan12Features = 51,

    PhysicalDeviceVulkan12Properties = 52,

    ImageFormatListCreateInfo = 1000147000,

    AttachmentDescription2 = 1000109000,

    AttachmentReference2 = 1000109001,

    SubpassDescription2 = 1000109002,

    SubpassDependency2 = 1000109003,

    RenderPassCreateInfo2 = 1000109004,

    SubpassBeginInfo = 1000109005,

    SubpassEndInfo = 1000109006,

    PhysicalDevice8BitStorageFeatures = 1000177000,

    PhysicalDeviceDriverProperties = 1000196000,

    PhysicalDeviceShaderAtomicInt64Features = 1000180000,

    PhysicalDeviceShaderFloat16Int8Features = 1000082000,

    PhysicalDeviceFloatControlsProperties = 1000197000,

    DescriptorSetLayoutBindingFlagsCreateInfo = 1000161000,

    PhysicalDeviceDescriptorIndexingFeatures = 1000161001,

    PhysicalDeviceDescriptorIndexingProperties = 1000161002,

    DescriptorSetVariableDescriptorCountAllocateInfo = 1000161003,

    DescriptorSetVariableDescriptorCountLayoutSupport = 1000161004,

    PhysicalDeviceDepthStencilResolveProperties = 1000199000,

    SubpassDescriptionDepthStencilResolve = 1000199001,

    PhysicalDeviceScalarBlockLayoutFeatures = 1000221000,

    ImageStencilUsageCreateInfo = 1000246000,

    PhysicalDeviceSamplerFilterMinmaxProperties = 1000130000,

    SamplerReductionModeCreateInfo = 1000130001,

    PhysicalDeviceVulkanMemoryModelFeatures = 1000211000,

    PhysicalDeviceImagelessFramebufferFeatures = 1000108000,

    FramebufferAttachmentsCreateInfo = 1000108001,

    FramebufferAttachmentImageInfo = 1000108002,

    RenderPassAttachmentBeginInfo = 1000108003,

    PhysicalDeviceUniformBufferStandardLayoutFeatures = 1000253000,

    PhysicalDeviceShaderSubgroupExtendedTypesFeatures = 1000175000,

    PhysicalDeviceSeparateDepthStencilLayoutsFeatures = 1000241000,

    AttachmentReferenceStencilLayout = 1000241001,

    AttachmentDescriptionStencilLayout = 1000241002,

    PhysicalDeviceHostQueryResetFeatures = 1000261000,

    PhysicalDeviceTimelineSemaphoreFeatures = 1000207000,

    PhysicalDeviceTimelineSemaphoreProperties = 1000207001,

    SemaphoreTypeCreateInfo = 1000207002,

    TimelineSemaphoreSubmitInfo = 1000207003,

    SemaphoreWaitInfo = 1000207004,

    SemaphoreSignalInfo = 1000207005,

    PhysicalDeviceBufferDeviceAddressFeatures = 1000257000,

    BufferDeviceAddressInfo = 1000244001,

    BufferOpaqueCaptureAddressCreateInfo = 1000257002,

    MemoryOpaqueCaptureAddressAllocateInfo = 1000257003,

    DeviceMemoryOpaqueCaptureAddressInfo = 1000257004,

    PhysicalDeviceVulkan13Features = 53,

    PhysicalDeviceVulkan13Properties = 54,

    PipelineCreationFeedbackCreateInfo = 1000192000,

    PhysicalDeviceShaderTerminateInvocationFeatures = 1000215000,

    PhysicalDeviceToolProperties = 1000245000,

    PhysicalDeviceShaderDemoteToHelperInvocationFeatures = 1000276000,

    PhysicalDevicePrivateDataFeatures = 1000295000,

    DevicePrivateDataCreateInfo = 1000295001,

    PrivateDataSlotCreateInfo = 1000295002,

    PhysicalDevicePipelineCreationCacheControlFeatures = 1000297000,

    MemoryBarrier2 = 1000314000,

    BufferMemoryBarrier2 = 1000314001,

    ImageMemoryBarrier2 = 1000314002,

    DependencyInfo = 1000314003,

    SubmitInfo2 = 1000314004,

    SemaphoreSubmitInfo = 1000314005,

    CommandBufferSubmitInfo = 1000314006,

    PhysicalDeviceSynchronization2Features = 1000314007,

    PhysicalDeviceZeroInitializeWorkgroupMemoryFeatures = 1000325000,

    PhysicalDeviceImageRobustnessFeatures = 1000335000,

    CopyBufferInfo2 = 1000337000,

    CopyImageInfo2 = 1000337001,

    CopyBufferToImageInfo2 = 1000337002,

    CopyImageToBufferInfo2 = 1000337003,

    BlitImageInfo2 = 1000337004,

    ResolveImageInfo2 = 1000337005,

    BufferCopy2 = 1000337006,

    ImageCopy2 = 1000337007,

    ImageBlit2 = 1000337008,

    BufferImageCopy2 = 1000337009,

    ImageResolve2 = 1000337010,

    PhysicalDeviceSubgroupSizeControlProperties = 1000225000,

    PipelineShaderStageRequiredSubgroupSizeCreateInfo = 1000225001,

    PhysicalDeviceSubgroupSizeControlFeatures = 1000225002,

    PhysicalDeviceInlineUniformBlockFeatures = 1000138000,

    PhysicalDeviceInlineUniformBlockProperties = 1000138001,

    WriteDescriptorSetInlineUniformBlock = 1000138002,

    DescriptorPoolInlineUniformBlockCreateInfo = 1000138003,

    PhysicalDeviceTextureCompressionAstcHdrFeatures = 1000066000,

    RenderingInfo = 1000044000,

    RenderingAttachmentInfo = 1000044001,

    PipelineRenderingCreateInfo = 1000044002,

    PhysicalDeviceDynamicRenderingFeatures = 1000044003,

    CommandBufferInheritanceRenderingInfo = 1000044004,

    PhysicalDeviceShaderIntegerDotProductFeatures = 1000280000,

    PhysicalDeviceShaderIntegerDotProductProperties = 1000280001,

    PhysicalDeviceTexelBufferAlignmentProperties = 1000281001,

    FormatProperties3 = 1000360000,

    PhysicalDeviceMaintenance4Features = 1000413000,

    PhysicalDeviceMaintenance4Properties = 1000413001,

    DeviceBufferMemoryRequirements = 1000413002,

    DeviceImageMemoryRequirements = 1000413003,

    PhysicalDeviceVulkan14Features = 55,

    PhysicalDeviceVulkan14Properties = 56,

    DeviceQueueGlobalPriorityCreateInfo = 1000174000,

    PhysicalDeviceGlobalPriorityQueryFeatures = 1000388000,

    QueueFamilyGlobalPriorityProperties = 1000388001,

    PhysicalDeviceShaderSubgroupRotateFeatures = 1000416000,

    PhysicalDeviceShaderFloatControls2Features = 1000528000,

    PhysicalDeviceShaderExpectAssumeFeatures = 1000544000,

    PhysicalDeviceLineRasterizationFeatures = 1000259000,

    PipelineRasterizationLineStateCreateInfo = 1000259001,

    PhysicalDeviceLineRasterizationProperties = 1000259002,

    PhysicalDeviceVertexAttributeDivisorProperties = 1000525000,

    PipelineVertexInputDivisorStateCreateInfo = 1000190001,

    PhysicalDeviceVertexAttributeDivisorFeatures = 1000190002,

    PhysicalDeviceIndexTypeUint8Features = 1000265000,

    MemoryMapInfo = 1000271000,

    MemoryUnmapInfo = 1000271001,

    PhysicalDeviceMaintenance5Features = 1000470000,

    PhysicalDeviceMaintenance5Properties = 1000470001,

    RenderingAreaInfo = 1000470003,

    DeviceImageSubresourceInfo = 1000470004,

    SubresourceLayout2 = 1000338002,

    ImageSubresource2 = 1000338003,

    PipelineCreateFlags2CreateInfo = 1000470005,

    BufferUsageFlags2CreateInfo = 1000470006,

    PhysicalDevicePushDescriptorProperties = 1000080000,

    PhysicalDeviceDynamicRenderingLocalReadFeatures = 1000232000,

    RenderingAttachmentLocationInfo = 1000232001,

    RenderingInputAttachmentIndexInfo = 1000232002,

    PhysicalDeviceMaintenance6Features = 1000545000,

    PhysicalDeviceMaintenance6Properties = 1000545001,

    BindMemoryStatus = 1000545002,

    BindDescriptorSetsInfo = 1000545003,

    PushConstantsInfo = 1000545004,

    PushDescriptorSetInfo = 1000545005,

    PushDescriptorSetWithTemplateInfo = 1000545006,

    PhysicalDevicePipelineProtectedAccessFeatures = 1000466000,

    PipelineRobustnessCreateInfo = 1000068000,

    PhysicalDevicePipelineRobustnessFeatures = 1000068001,

    PhysicalDevicePipelineRobustnessProperties = 1000068002,

    PhysicalDeviceHostImageCopyFeatures = 1000270000,

    PhysicalDeviceHostImageCopyProperties = 1000270001,

    MemoryToImageCopy = 1000270002,

    ImageToMemoryCopy = 1000270003,

    CopyImageToMemoryInfo = 1000270004,

    CopyMemoryToImageInfo = 1000270005,

    HostImageLayoutTransitionInfo = 1000270006,

    CopyImageToImageInfo = 1000270007,

    SubresourceHostMemcpySize = 1000270008,

    HostImageCopyDevicePerformanceQuery = 1000270009,

    SwapChainCreateInfoKhr = 1000001000,

    PresentInfoKhr = 1000001001,

    DeviceGroupPresentCapabilitiesKhr = 1000060007,

    ImageSwapChainCreateInfoKhr = 1000060008,

    BindImageMemorySwapChainInfoKhr = 1000060009,

    AcquireNextImageInfoKhr = 1000060010,

    DeviceGroupPresentInfoKhr = 1000060011,

    DeviceGroupSwapChainCreateInfoKhr = 1000060012,

    DisplayModeCreateInfoKhr = 1000002000,

    DisplaySurfaceCreateInfoKhr = 1000002001,

    DisplayPresentInfoKhr = 1000003000,

    XlibSurfaceCreateInfoKhr = 1000004000,

    XcbSurfaceCreateInfoKhr = 1000005000,

    WaylandSurfaceCreateInfoKhr = 1000006000,

    AndroidSurfaceCreateInfoKhr = 1000008000,

    Win32SurfaceCreateInfoKhr = 1000009000,

    DebugReportCallbackCreateInfoExt = 1000011000,

    PipelineRasterizationStateRasterizationOrderAmd = 1000018000,

    DebugMarkerObjectNameInfoExt = 1000022000,

    DebugMarkerObjectTagInfoExt = 1000022001,

    DebugMarkerMarkerInfoExt = 1000022002,

    VideoProfileInfoKhr = 1000023000,

    VideoCapabilitiesKhr = 1000023001,

    VideoPictureResourceInfoKhr = 1000023002,

    VideoSessionMemoryRequirementsKhr = 1000023003,

    BindVideoSessionMemoryInfoKhr = 1000023004,

    VideoSessionCreateInfoKhr = 1000023005,

    VideoSessionParametersCreateInfoKhr = 1000023006,

    VideoSessionParametersUpdateInfoKhr = 1000023007,

    VideoBeginCodingInfoKhr = 1000023008,

    VideoEndCodingInfoKhr = 1000023009,

    VideoCodingControlInfoKhr = 1000023010,

    VideoReferenceSlotInfoKhr = 1000023011,

    QueueFamilyVideoPropertiesKhr = 1000023012,

    VideoProfileListInfoKhr = 1000023013,

    PhysicalDeviceVideoFormatInfoKhr = 1000023014,

    VideoFormatPropertiesKhr = 1000023015,

    QueueFamilyQueryResultStatusPropertiesKhr = 1000023016,

    VideoDecodeInfoKhr = 1000024000,

    VideoDecodeCapabilitiesKhr = 1000024001,

    VideoDecodeUsageInfoKhr = 1000024002,

    DedicatedAllocationImageCreateInfoNv = 1000026000,

    DedicatedAllocationBufferCreateInfoNv = 1000026001,

    DedicatedAllocationMemoryAllocateInfoNv = 1000026002,

    PhysicalDeviceTransformFeedbackFeaturesExt = 1000028000,

    PhysicalDeviceTransformFeedbackPropertiesExt = 1000028001,

    PipelineRasterizationStateStreamCreateInfoExt = 1000028002,

    CuModuleCreateInfoNvx = 1000029000,

    CuFunctionCreateInfoNvx = 1000029001,

    CuLaunchInfoNvx = 1000029002,

    CuModuleTexturingModeCreateInfoNvx = 1000029004,

    ImageViewHandleInfoNvx = 1000030000,

    ImageViewAddressPropertiesNvx = 1000030001,

    VideoEncodeH264CapabilitiesKhr = 1000038000,

    VideoEncodeH264SessionParametersCreateInfoKhr = 1000038001,

    VideoEncodeH264SessionParametersAddInfoKhr = 1000038002,

    VideoEncodeH264PictureInfoKhr = 1000038003,

    VideoEncodeH264DpbSlotInfoKhr = 1000038004,

    VideoEncodeH264NaluSliceInfoKhr = 1000038005,

    VideoEncodeH264GopRemainingFrameInfoKhr = 1000038006,

    VideoEncodeH264ProfileInfoKhr = 1000038007,

    VideoEncodeH264RateControlInfoKhr = 1000038008,

    VideoEncodeH264RateControlLayerInfoKhr = 1000038009,

    VideoEncodeH264SessionCreateInfoKhr = 1000038010,

    VideoEncodeH264QualityLevelPropertiesKhr = 1000038011,

    VideoEncodeH264SessionParametersGetInfoKhr = 1000038012,

    VideoEncodeH264SessionParametersFeedbackInfoKhr = 1000038013,

    VideoEncodeH265CapabilitiesKhr = 1000039000,

    VideoEncodeH265SessionParametersCreateInfoKhr = 1000039001,

    VideoEncodeH265SessionParametersAddInfoKhr = 1000039002,

    VideoEncodeH265PictureInfoKhr = 1000039003,

    VideoEncodeH265DpbSlotInfoKhr = 1000039004,

    VideoEncodeH265NaluSliceSegmentInfoKhr = 1000039005,

    VideoEncodeH265GopRemainingFrameInfoKhr = 1000039006,

    VideoEncodeH265ProfileInfoKhr = 1000039007,

    VideoEncodeH265RateControlInfoKhr = 1000039009,

    VideoEncodeH265RateControlLayerInfoKhr = 1000039010,

    VideoEncodeH265SessionCreateInfoKhr = 1000039011,

    VideoEncodeH265QualityLevelPropertiesKhr = 1000039012,

    VideoEncodeH265SessionParametersGetInfoKhr = 1000039013,

    VideoEncodeH265SessionParametersFeedbackInfoKhr = 1000039014,

    VideoDecodeH264CapabilitiesKhr = 1000040000,

    VideoDecodeH264PictureInfoKhr = 1000040001,

    VideoDecodeH264ProfileInfoKhr = 1000040003,

    VideoDecodeH264SessionParametersCreateInfoKhr = 1000040004,

    VideoDecodeH264SessionParametersAddInfoKhr = 1000040005,

    VideoDecodeH264DpbSlotInfoKhr = 1000040006,

    TextureLodGatherFormatPropertiesAmd = 1000041000,

    StreamDescriptorSurfaceCreateInfoGgp = 1000049000,

    PhysicalDeviceCornerSampledImageFeaturesNv = 1000050000,

    ExternalMemoryImageCreateInfoNv = 1000056000,

    ExportMemoryAllocateInfoNv = 1000056001,

    ImportMemoryWin32HandleInfoNv = 1000057000,

    ExportMemoryWin32HandleInfoNv = 1000057001,

    Win32KeyedMutexAcquireReleaseInfoNv = 1000058000,

    ValidationFlagsExt = 1000061000,

    ViSurfaceCreateInfoNn = 1000062000,

    ImageViewAstcDecodeModeExt = 1000067000,

    PhysicalDeviceAstcDecodeFeaturesExt = 1000067001,

    ImportMemoryWin32HandleInfoKhr = 1000073000,

    ExportMemoryWin32HandleInfoKhr = 1000073001,

    MemoryWin32HandlePropertiesKhr = 1000073002,

    MemoryGetWin32HandleInfoKhr = 1000073003,

    ImportMemoryFdInfoKhr = 1000074000,

    MemoryFdPropertiesKhr = 1000074001,

    MemoryGetFdInfoKhr = 1000074002,

    Win32KeyedMutexAcquireReleaseInfoKhr = 1000075000,

    ImportSemaphoreWin32HandleInfoKhr = 1000078000,

    ExportSemaphoreWin32HandleInfoKhr = 1000078001,

    D3D12FenceSubmitInfoKhr = 1000078002,

    SemaphoreGetWin32HandleInfoKhr = 1000078003,

    ImportSemaphoreFdInfoKhr = 1000079000,

    SemaphoreGetFdInfoKhr = 1000079001,

    CommandBufferInheritanceConditionalRenderingInfoExt = 1000081000,

    PhysicalDeviceConditionalRenderingFeaturesExt = 1000081001,

    ConditionalRenderingBeginInfoExt = 1000081002,

    PresentRegionsKhr = 1000084000,

    PipelineViewportWScalingStateCreateInfoNv = 1000087000,

    Surface_Capabilities__2_Ext = 1000090000,

    DisplayPowerInfoExt = 1000091000,

    DeviceEventInfoExt = 1000091001,

    DisplayEventInfoExt = 1000091002,

    SwapChainCounterCreateInfoExt = 1000091003,

    PresentTimesInfoGoogle = 1000092000,

    PhysicalDeviceMultiviewPerViewAttributesPropertiesNvx = 1000097000,

    MultiviewPerViewAttributesInfoNvx = 1000044009,

    PipelineViewportSwizzleStateCreateInfoNv = 1000098000,

    PhysicalDeviceDiscardRectanglePropertiesExt = 1000099000,

    PipelineDiscardRectangleStateCreateInfoExt = 1000099001,

    PhysicalDeviceConservativeRasterizationPropertiesExt = 1000101000,

    PipelineRasterizationConservativeStateCreateInfoExt = 1000101001,

    PhysicalDeviceDepthClipEnableFeaturesExt = 1000102000,

    PipelineRasterizationDepthClipStateCreateInfoExt = 1000102001,

    HdrMetadataExt = 1000105000,

    PhysicalDeviceRelaxedLineRasterizationFeaturesImg = 1000110000,

    SharedPresentSurfaceCapabilitiesKhr = 1000111000,

    ImportFenceWin32HandleInfoKhr = 1000114000,

    ExportFenceWin32HandleInfoKhr = 1000114001,

    FenceGetWin32HandleInfoKhr = 1000114002,

    ImportFenceFdInfoKhr = 1000115000,

    FenceGetFdInfoKhr = 1000115001,

    PhysicalDevicePerformanceQueryFeaturesKhr = 1000116000,

    PhysicalDevicePerformanceQueryPropertiesKhr = 1000116001,

    QueryPoolPerformanceCreateInfoKhr = 1000116002,

    PerformanceQuerySubmitInfoKhr = 1000116003,

    AcquireProfilingLockInfoKhr = 1000116004,

    PerformanceCounterKhr = 1000116005,

    PerformanceCounterDescriptionKhr = 1000116006,

    PhysicalDeviceSurfaceInfo2Khr = 1000119000,

    SurfaceCapabilities2Khr = 1000119001,

    SurfaceFormat2Khr = 1000119002,

    DisplayProperties2Khr = 1000121000,

    DisplayPlaneProperties2Khr = 1000121001,

    DisplayModeProperties2Khr = 1000121002,

    DisplayPlaneInfo2Khr = 1000121003,

    DisplayPlaneCapabilities2Khr = 1000121004,

    IosSurfaceCreateInfoMvk = 1000122000,

    MacosSurfaceCreateInfoMvk = 1000123000,

    DebugUtilsObjectNameInfoExt = 1000128000,

    DebugUtilsObjectTagInfoExt = 1000128001,

    DebugUtilsLabelExt = 1000128002,

    DebugUtilsMessengerCallbackDataExt = 1000128003,

    DebugUtilsMessengerCreateInfoExt = 1000128004,

    AndroidHardwareBufferUsageAndroid = 1000129000,

    AndroidHardwareBufferPropertiesAndroid = 1000129001,

    AndroidHardwareBufferFormatPropertiesAndroid = 1000129002,

    ImportAndroidHardwareBufferInfoAndroid = 1000129003,

    MemoryGetAndroidHardwareBufferInfoAndroid = 1000129004,

    ExternalFormatAndroid = 1000129005,

    AndroidHardwareBufferFormatProperties2Android = 1000129006,

    AttachmentSampleCountInfoAmd = 1000044008,

    PhysicalDeviceShaderBfloat16FeaturesKhr = 1000141000,

    SampleLocationsInfoExt = 1000143000,

    RenderPassSampleLocationsBeginInfoExt = 1000143001,

    PipelineSampleLocationsStateCreateInfoExt = 1000143002,

    PhysicalDeviceSampleLocationsPropertiesExt = 1000143003,

    MultisamplePropertiesExt = 1000143004,

    PhysicalDeviceBlendOperationAdvancedFeaturesExt = 1000148000,

    PhysicalDeviceBlendOperationAdvancedPropertiesExt = 1000148001,

    PipelineColorBlendAdvancedStateCreateInfoExt = 1000148002,

    PipelineCoverageToColorStateCreateInfoNv = 1000149000,

    WriteDescriptorSetAccelerationStructureKhr = 1000150007,

    AccelerationStructureBuildGeometryInfoKhr = 1000150000,

    AccelerationStructureDeviceAddressInfoKhr = 1000150002,

    AccelerationStructureGeometryAabbsDataKhr = 1000150003,

    AccelerationStructureGeometryInstancesDataKhr = 1000150004,

    AccelerationStructureGeometryTrianglesDataKhr = 1000150005,

    AccelerationStructureGeometryKhr = 1000150006,

    AccelerationStructureVersionInfoKhr = 1000150009,

    CopyAccelerationStructureInfoKhr = 1000150010,

    CopyAccelerationStructureToMemoryInfoKhr = 1000150011,

    CopyMemoryToAccelerationStructureInfoKhr = 1000150012,

    PhysicalDeviceAccelerationStructureFeaturesKhr = 1000150013,

    PhysicalDeviceAccelerationStructurePropertiesKhr = 1000150014,

    AccelerationStructureCreateInfoKhr = 1000150017,

    AccelerationStructureBuildSizesInfoKhr = 1000150020,

    PhysicalDeviceRayTracingPipelineFeaturesKhr = 1000347000,

    PhysicalDeviceRayTracingPipelinePropertiesKhr = 1000347001,

    RayTracingPipelineCreateInfoKhr = 1000150015,

    RayTracingShaderGroupCreateInfoKhr = 1000150016,

    RayTracingPipelineInterfaceCreateInfoKhr = 1000150018,

    PhysicalDeviceRayQueryFeaturesKhr = 1000348013,

    PipelineCoverageModulationStateCreateInfoNv = 1000152000,

    PhysicalDeviceShaderSmBuiltinsFeaturesNv = 1000154000,

    PhysicalDeviceShaderSmBuiltinsPropertiesNv = 1000154001,

    DrmFormatModifierPropertiesListExt = 1000158000,

    PhysicalDeviceImageDrmFormatModifierInfoExt = 1000158002,

    ImageDrmFormatModifierListCreateInfoExt = 1000158003,

    ImageDrmFormatModifierExplicitCreateInfoExt = 1000158004,

    ImageDrmFormatModifierPropertiesExt = 1000158005,

    DrmFormatModifierPropertiesList2Ext = 1000158006,

    ValidationCacheCreateInfoExt = 1000160000,

    ShaderModuleValidationCacheCreateInfoExt = 1000160001,

    PipelineViewportShadingRateImageStateCreateInfoNv = 1000164000,

    PhysicalDeviceShadingRateImageFeaturesNv = 1000164001,

    PhysicalDeviceShadingRateImagePropertiesNv = 1000164002,

    PipelineViewportCoarseSampleOrderStateCreateInfoNv = 1000164005,

    RayTracingPipelineCreateInfoNv = 1000165000,

    AccelerationStructureCreateInfoNv = 1000165001,

    GeometryNv = 1000165003,

    GeometryTrianglesNv = 1000165004,

    GeometryAabbNv = 1000165005,

    BindAccelerationStructureMemoryInfoNv = 1000165006,

    WriteDescriptorSetAccelerationStructureNv = 1000165007,

    AccelerationStructureMemoryRequirementsInfoNv = 1000165008,

    PhysicalDeviceRayTracingPropertiesNv = 1000165009,

    RayTracingShaderGroupCreateInfoNv = 1000165011,

    AccelerationStructureInfoNv = 1000165012,

    PhysicalDeviceRepresentativeFragmentTestFeaturesNv = 1000166000,

    PipelineRepresentativeFragmentTestStateCreateInfoNv = 1000166001,

    PhysicalDeviceImageViewImageFormatInfoExt = 1000170000,

    FilterCubicImageViewImageFormatPropertiesExt = 1000170001,

    ImportMemoryHostPointerInfoExt = 1000178000,

    MemoryHostPointerPropertiesExt = 1000178001,

    PhysicalDeviceExternalMemoryHostPropertiesExt = 1000178002,

    PhysicalDeviceShaderClockFeaturesKhr = 1000181000,

    PipelineCompilerControlCreateInfoAmd = 1000183000,

    PhysicalDeviceShaderCorePropertiesAmd = 1000185000,

    VideoDecodeH265CapabilitiesKhr = 1000187000,

    VideoDecodeH265SessionParametersCreateInfoKhr = 1000187001,

    VideoDecodeH265SessionParametersAddInfoKhr = 1000187002,

    VideoDecodeH265ProfileInfoKhr = 1000187003,

    VideoDecodeH265PictureInfoKhr = 1000187004,

    VideoDecodeH265DpbSlotInfoKhr = 1000187005,

    DeviceMemoryOverallocationCreateInfoAmd = 1000189000,

    PhysicalDeviceVertexAttributeDivisorPropertiesExt = 1000190000,

    PresentFrameTokenGgp = 1000191000,

    PhysicalDeviceMeshShaderFeaturesNv = 1000202000,

    PhysicalDeviceMeshShaderPropertiesNv = 1000202001,

    PhysicalDeviceShaderImageFootprintFeaturesNv = 1000204000,

    PipelineViewportExclusiveScissorStateCreateInfoNv = 1000205000,

    PhysicalDeviceExclusiveScissorFeaturesNv = 1000205002,

    CheckpointDataNv = 1000206000,

    QueueFamilyCheckpointPropertiesNv = 1000206001,

    QueueFamilyCheckpointProperties2Nv = 1000314008,

    CheckpointData2Nv = 1000314009,

    PhysicalDeviceShaderIntegerFunctions2FeaturesIntel = 1000209000,

    QueryPoolPerformanceQueryCreateInfoIntel = 1000210000,

    InitializePerformanceApiInfoIntel = 1000210001,

    PerformanceMarkerInfoIntel = 1000210002,

    PerformanceStreamMarkerInfoIntel = 1000210003,

    PerformanceOverrideInfoIntel = 1000210004,

    PerformanceConfigurationAcquireInfoIntel = 1000210005,

    PhysicalDevicePciBusInfoPropertiesExt = 1000212000,

    DisplayNativeHdrSurfaceCapabilitiesAmd = 1000213000,

    SwapChainDisplayNativeHdrCreateInfoAmd = 1000213001,

    ImagepipeSurfaceCreateInfoFuchsia = 1000214000,

    MetalSurfaceCreateInfoExt = 1000217000,

    PhysicalDeviceFragmentDensityMapFeaturesExt = 1000218000,

    PhysicalDeviceFragmentDensityMapPropertiesExt = 1000218001,

    RenderPassFragmentDensityMapCreateInfoExt = 1000218002,

    RenderingFragmentDensityMapAttachmentInfoExt = 1000044007,

    FragmentShadingRateAttachmentInfoKhr = 1000226000,

    PipelineFragmentShadingRateStateCreateInfoKhr = 1000226001,

    PhysicalDeviceFragmentShadingRatePropertiesKhr = 1000226002,

    PhysicalDeviceFragmentShadingRateFeaturesKhr = 1000226003,

    PhysicalDeviceFragmentShadingRateKhr = 1000226004,

    RenderingFragmentShadingRateAttachmentInfoKhr = 1000044006,

    PhysicalDeviceShaderCoreProperties2Amd = 1000227000,

    PhysicalDeviceCoherentMemoryFeaturesAmd = 1000229000,

    PhysicalDeviceShaderImageAtomicInt64FeaturesExt = 1000234000,

    PhysicalDeviceShaderQuadControlFeaturesKhr = 1000235000,

    PhysicalDeviceMemoryBudgetPropertiesExt = 1000237000,

    PhysicalDeviceMemoryPriorityFeaturesExt = 1000238000,

    MemoryPriorityAllocateInfoExt = 1000238001,

    SurfaceProtectedCapabilitiesKhr = 1000239000,

    PhysicalDeviceDedicatedAllocationImageAliasingFeaturesNv = 1000240000,

    PhysicalDeviceBufferDeviceAddressFeaturesExt = 1000244000,

    BufferDeviceAddressCreateInfoExt = 1000244002,

    ValidationFeaturesExt = 1000247000,

    PhysicalDevicePresentWaitFeaturesKhr = 1000248000,

    PhysicalDeviceCooperativeMatrixFeaturesNv = 1000249000,

    CooperativeMatrixPropertiesNv = 1000249001,

    PhysicalDeviceCooperativeMatrixPropertiesNv = 1000249002,

    PhysicalDeviceCoverageReductionModeFeaturesNv = 1000250000,

    PipelineCoverageReductionStateCreateInfoNv = 1000250001,

    FramebufferMixedSamplesCombinationNv = 1000250002,

    PhysicalDeviceFragmentShaderInterlockFeaturesExt = 1000251000,

    PhysicalDeviceYcbcrImageArraysFeaturesExt = 1000252000,

    PhysicalDeviceProvokingVertexFeaturesExt = 1000254000,

    PipelineRasterizationProvokingVertexStateCreateInfoExt = 1000254001,

    PhysicalDeviceProvokingVertexPropertiesExt = 1000254002,

    SurfaceFullScreenExclusiveInfoExt = 1000255000,

    SurfaceCapabilitiesFullScreenExclusiveExt = 1000255002,

    SurfaceFullScreenExclusiveWin32InfoExt = 1000255001,

    HeadlessSurfaceCreateInfoExt = 1000256000,

    PhysicalDeviceShaderAtomicFloatFeaturesExt = 1000260000,

    PhysicalDeviceExtendedDynamicStateFeaturesExt = 1000267000,

    PhysicalDevicePipelineExecutablePropertiesFeaturesKhr = 1000269000,

    PipelineInfoKhr = 1000269001,

    PipelineExecutablePropertiesKhr = 1000269002,

    PipelineExecutableInfoKhr = 1000269003,

    PipelineExecutableStatisticKhr = 1000269004,

    PipelineExecutableInternalRepresentationKhr = 1000269005,

    PhysicalDeviceMapMemoryPlacedFeaturesExt = 1000272000,

    PhysicalDeviceMapMemoryPlacedPropertiesExt = 1000272001,

    MemoryMapPlacedInfoExt = 1000272002,

    PhysicalDeviceShaderAtomicFloat2FeaturesExt = 1000273000,

    PhysicalDeviceDeviceGeneratedCommandsPropertiesNv = 1000277000,

    GraphicsShaderGroupCreateInfoNv = 1000277001,

    GraphicsPipelineShaderGroupsCreateInfoNv = 1000277002,

    IndirectCommandsLayoutTokenNv = 1000277003,

    IndirectCommandsLayoutCreateInfoNv = 1000277004,

    GeneratedCommandsInfoNv = 1000277005,

    GeneratedCommandsMemoryRequirementsInfoNv = 1000277006,

    PhysicalDeviceDeviceGeneratedCommandsFeaturesNv = 1000277007,

    PhysicalDeviceInheritedViewportScissorFeaturesNv = 1000278000,

    CommandBufferInheritanceViewportScissorInfoNv = 1000278001,

    PhysicalDeviceTexelBufferAlignmentFeaturesExt = 1000281000,

    CommandBufferInheritanceRenderPassTransformInfoQcom = 1000282000,

    RenderPassTransformBeginInfoQcom = 1000282001,

    PhysicalDeviceDepthBiasControlFeaturesExt = 1000283000,

    DepthBiasInfoExt = 1000283001,

    DepthBiasRepresentationInfoExt = 1000283002,

    PhysicalDeviceDeviceMemoryReportFeaturesExt = 1000284000,

    DeviceDeviceMemoryReportCreateInfoExt = 1000284001,

    DeviceMemoryReportCallbackDataExt = 1000284002,

    SamplerCustomBorderColorCreateInfoExt = 1000287000,

    PhysicalDeviceCustomBorderColorPropertiesExt = 1000287001,

    PhysicalDeviceCustomBorderColorFeaturesExt = 1000287002,

    PipelineLibraryCreateInfoKhr = 1000290000,

    PhysicalDevicePresentBarrierFeaturesNv = 1000292000,

    SurfaceCapabilitiesPresentBarrierNv = 1000292001,

    SwapChainPresentBarrierCreateInfoNv = 1000292002,

    PresentIdKhr = 1000294000,

    PhysicalDevicePresentIdFeaturesKhr = 1000294001,

    VideoEncodeInfoKhr = 1000299000,

    VideoEncodeRateControlInfoKhr = 1000299001,

    VideoEncodeRateControlLayerInfoKhr = 1000299002,

    VideoEncodeCapabilitiesKhr = 1000299003,

    VideoEncodeUsageInfoKhr = 1000299004,

    QueryPoolVideoEncodeFeedbackCreateInfoKhr = 1000299005,

    PhysicalDeviceVideoEncodeQualityLevelInfoKhr = 1000299006,

    VideoEncodeQualityLevelPropertiesKhr = 1000299007,

    VideoEncodeQualityLevelInfoKhr = 1000299008,

    VideoEncodeSessionParametersGetInfoKhr = 1000299009,

    VideoEncodeSessionParametersFeedbackInfoKhr = 1000299010,

    PhysicalDeviceDiagnosticsConfigFeaturesNv = 1000300000,

    DeviceDiagnosticsConfigCreateInfoNv = 1000300001,

    PhysicalDeviceTileShadingFeaturesQcom = 1000309000,

    PhysicalDeviceTileShadingPropertiesQcom = 1000309001,

    RenderPassTileShadingCreateInfoQcom = 1000309002,

    PerTileBeginInfoQcom = 1000309003,

    PerTileEndInfoQcom = 1000309004,

    DispatchTileInfoQcom = 1000309005,

    QueryLowLatencySupportNv = 1000310000,

    ExportMetalObjectCreateInfoExt = 1000311000,

    ExportMetalObjectsInfoExt = 1000311001,

    ExportMetalDeviceInfoExt = 1000311002,

    ExportMetalCommandQueueInfoExt = 1000311003,

    ExportMetalBufferInfoExt = 1000311004,

    ImportMetalBufferInfoExt = 1000311005,

    ExportMetalTextureInfoExt = 1000311006,

    ImportMetalTextureInfoExt = 1000311007,

    ExportMetalIoSurfaceInfoExt = 1000311008,

    ImportMetalIoSurfaceInfoExt = 1000311009,

    ExportMetalSharedEventInfoExt = 1000311010,

    ImportMetalSharedEventInfoExt = 1000311011,

    PhysicalDeviceDescriptorBufferPropertiesExt = 1000316000,

    PhysicalDeviceDescriptorBufferDensityMapPropertiesExt = 1000316001,

    PhysicalDeviceDescriptorBufferFeaturesExt = 1000316002,

    DescriptorAddressInfoExt = 1000316003,

    DescriptorGetInfoExt = 1000316004,

    BufferCaptureDescriptorDataInfoExt = 1000316005,

    ImageCaptureDescriptorDataInfoExt = 1000316006,

    ImageViewCaptureDescriptorDataInfoExt = 1000316007,

    SamplerCaptureDescriptorDataInfoExt = 1000316008,

    OpaqueCaptureDescriptorDataCreateInfoExt = 1000316010,

    DescriptorBufferBindingInfoExt = 1000316011,

    DescriptorBufferBindingPushDescriptorBufferHandleExt = 1000316012,

    AccelerationStructureCaptureDescriptorDataInfoExt = 1000316009,

    PhysicalDeviceGraphicsPipelineLibraryFeaturesExt = 1000320000,

    PhysicalDeviceGraphicsPipelineLibraryPropertiesExt = 1000320001,

    GraphicsPipelineLibraryCreateInfoExt = 1000320002,

    PhysicalDeviceShaderEarlyAndLateFragmentTestsFeaturesAmd = 1000321000,

    PhysicalDeviceFragmentShaderBarycentricFeaturesKhr = 1000203000,

    PhysicalDeviceFragmentShaderBarycentricPropertiesKhr = 1000322000,

    PhysicalDeviceShaderSubgroupUniformControlFlowFeaturesKhr = 1000323000,

    PhysicalDeviceFragmentShadingRateEnumsPropertiesNv = 1000326000,

    PhysicalDeviceFragmentShadingRateEnumsFeaturesNv = 1000326001,

    PipelineFragmentShadingRateEnumStateCreateInfoNv = 1000326002,

    AccelerationStructureGeometryMotionTrianglesDataNv = 1000327000,

    PhysicalDeviceRayTracingMotionBlurFeaturesNv = 1000327001,

    AccelerationStructureMotionInfoNv = 1000327002,

    PhysicalDeviceMeshShaderFeaturesExt = 1000328000,

    PhysicalDeviceMeshShaderPropertiesExt = 1000328001,

    PhysicalDeviceYcbcr2Plane444FormatsFeaturesExt = 1000330000,

    PhysicalDeviceFragmentDensityMap2FeaturesExt = 1000332000,

    PhysicalDeviceFragmentDensityMap2PropertiesExt = 1000332001,

    CopyCommandTransformInfoQcom = 1000333000,

    PhysicalDeviceWorkgroupMemoryExplicitLayoutFeaturesKhr = 1000336000,

    PhysicalDeviceImageCompressionControlFeaturesExt = 1000338000,

    ImageCompressionControlExt = 1000338001,

    ImageCompressionPropertiesExt = 1000338004,

    PhysicalDeviceAttachmentFeedbackLoopLayoutFeaturesExt = 1000339000,

    PhysicalDevice4444FormatsFeaturesExt = 1000340000,

    PhysicalDeviceFaultFeaturesExt = 1000341000,

    DeviceFaultCountsExt = 1000341001,

    DeviceFaultInfoExt = 1000341002,

    PhysicalDeviceRGBA10X6FormatsFeaturesExt = 1000344000,

    DirectfbSurfaceCreateInfoExt = 1000346000,

    PhysicalDeviceVertexInputDynamicStateFeaturesExt = 1000352000,

    VertexInputBindingDescription2Ext = 1000352001,

    VertexInputAttributeDescription2Ext = 1000352002,

    PhysicalDeviceDrmPropertiesExt = 1000353000,

    PhysicalDeviceAddressBindingReportFeaturesExt = 1000354000,

    DeviceAddressBindingCallbackDataExt = 1000354001,

    PhysicalDeviceDepthClipControlFeaturesExt = 1000355000,

    PipelineViewportDepthClipControlCreateInfoExt = 1000355001,

    PhysicalDevicePrimitiveTopologyListRestartFeaturesExt = 1000356000,

    ImportMemoryZirconHandleInfoFuchsia = 1000364000,

    MemoryZirconHandlePropertiesFuchsia = 1000364001,

    MemoryGetZirconHandleInfoFuchsia = 1000364002,

    ImportSemaphoreZirconHandleInfoFuchsia = 1000365000,

    SemaphoreGetZirconHandleInfoFuchsia = 1000365001,

    BufferCollectionCreateInfoFuchsia = 1000366000,

    ImportMemoryBufferCollectionFuchsia = 1000366001,

    BufferCollectionImageCreateInfoFuchsia = 1000366002,

    BufferCollectionPropertiesFuchsia = 1000366003,

    BufferConstraintsInfoFuchsia = 1000366004,

    BufferCollectionBufferCreateInfoFuchsia = 1000366005,

    ImageConstraintsInfoFuchsia = 1000366006,

    ImageFormatConstraintsInfoFuchsia = 1000366007,

    SysmemColorSpaceFuchsia = 1000366008,

    BufferCollectionConstraintsInfoFuchsia = 1000366009,

    SubpassShadingPipelineCreateInfoHuawei = 1000369000,

    PhysicalDeviceSubpassShadingFeaturesHuawei = 1000369001,

    PhysicalDeviceSubpassShadingPropertiesHuawei = 1000369002,

    PhysicalDeviceInvocationMaskFeaturesHuawei = 1000370000,

    MemoryGetRemoteAddressInfoNv = 1000371000,

    PhysicalDeviceExternalMemoryRdmaFeaturesNv = 1000371001,

    PipelinePropertiesIdentifierExt = 1000372000,

    PhysicalDevicePipelinePropertiesFeaturesExt = 1000372001,

    PhysicalDeviceFrameBoundaryFeaturesExt = 1000375000,

    FrameBoundaryExt = 1000375001,

    PhysicalDeviceMultisampledRenderToSingleSampledFeaturesExt = 1000376000,

    SubpassResolvePerformanceQueryExt = 1000376001,

    MultisampledRenderToSingleSampledInfoExt = 1000376002,

    PhysicalDeviceExtendedDynamicState2FeaturesExt = 1000377000,

    ScreenSurfaceCreateInfoQnx = 1000378000,

    PhysicalDeviceColorWriteEnableFeaturesExt = 1000381000,

    PipelineColorWriteCreateInfoExt = 1000381001,

    PhysicalDevicePrimitivesGeneratedQueryFeaturesExt = 1000382000,

    PhysicalDeviceRayTracingMaintenance1FeaturesKhr = 1000386000,

    PhysicalDeviceImageViewMinLodFeaturesExt = 1000391000,

    ImageViewMinLodCreateInfoExt = 1000391001,

    PhysicalDeviceMultiDrawFeaturesExt = 1000392000,

    PhysicalDeviceMultiDrawPropertiesExt = 1000392001,

    PhysicalDeviceImage2DViewOf3DFeaturesExt = 1000393000,

    PhysicalDeviceShaderTileImageFeaturesExt = 1000395000,

    PhysicalDeviceShaderTileImagePropertiesExt = 1000395001,

    MicromapBuildInfoExt = 1000396000,

    MicromapVersionInfoExt = 1000396001,

    CopyMicromapInfoExt = 1000396002,

    CopyMicromapToMemoryInfoExt = 1000396003,

    CopyMemoryToMicromapInfoExt = 1000396004,

    PhysicalDeviceOpacityMicromapFeaturesExt = 1000396005,

    PhysicalDeviceOpacityMicromapPropertiesExt = 1000396006,

    MicromapCreateInfoExt = 1000396007,

    MicromapBuildSizesInfoExt = 1000396008,

    AccelerationStructureTrianglesOpacityMicromapExt = 1000396009,

    PhysicalDeviceClusterCullingShaderFeaturesHuawei = 1000404000,

    PhysicalDeviceClusterCullingShaderPropertiesHuawei = 1000404001,

    PhysicalDeviceClusterCullingShaderVrsFeaturesHuawei = 1000404002,

    PhysicalDeviceBorderColorSwizzleFeaturesExt = 1000411000,

    SamplerBorderColorComponentMappingCreateInfoExt = 1000411001,

    PhysicalDevicePageableDeviceLocalMemoryFeaturesExt = 1000412000,

    PhysicalDeviceShaderCorePropertiesArm = 1000415000,

    DeviceQueueShaderCoreControlCreateInfoArm = 1000417000,

    PhysicalDeviceSchedulingControlsFeaturesArm = 1000417001,

    PhysicalDeviceSchedulingControlsPropertiesArm = 1000417002,

    PhysicalDeviceImageSlicedViewOf3DFeaturesExt = 1000418000,

    ImageViewSlicedCreateInfoExt = 1000418001,

    PhysicalDeviceDescriptorSetHostMappingFeaturesValve = 1000420000,

    DescriptorSetBindingReferenceValve = 1000420001,

    DescriptorSetLayoutHostMappingInfoValve = 1000420002,

    PhysicalDeviceNonSeamlessCubeMapFeaturesExt = 1000422000,

    PhysicalDeviceRenderPassStripedFeaturesArm = 1000424000,

    PhysicalDeviceRenderPassStripedPropertiesArm = 1000424001,

    RenderPassStripeBeginInfoArm = 1000424002,

    RenderPassStripeInfoArm = 1000424003,

    RenderPassStripeSubmitInfoArm = 1000424004,

    PhysicalDeviceCopyMemoryIndirectFeaturesNv = 1000426000,

    PhysicalDeviceCopyMemoryIndirectPropertiesNv = 1000426001,

    PhysicalDeviceMemoryDecompressionFeaturesNv = 1000427000,

    PhysicalDeviceMemoryDecompressionPropertiesNv = 1000427001,

    PhysicalDeviceDeviceGeneratedCommandsComputeFeaturesNv = 1000428000,

    ComputePipelineIndirectBufferInfoNv = 1000428001,

    PipelineIndirectDeviceAddressInfoNv = 1000428002,

    PhysicalDeviceRayTracingLinearSweptSpheresFeaturesNv = 1000429008,

    AccelerationStructureGeometryLinearSweptSpheresDataNv = 1000429009,

    AccelerationStructureGeometrySpheresDataNv = 1000429010,

    PhysicalDeviceLinearColorAttachmentFeaturesNv = 1000430000,

    PhysicalDeviceShaderMaximalReconvergenceFeaturesKhr = 1000434000,

    PhysicalDeviceImageCompressionControlSwapChainFeaturesExt = 1000437000,

    PhysicalDeviceImageProcessingFeaturesQcom = 1000440000,

    PhysicalDeviceImageProcessingPropertiesQcom = 1000440001,

    ImageViewSampleWeightCreateInfoQcom = 1000440002,

    PhysicalDeviceNestedCommandBufferFeaturesExt = 1000451000,

    PhysicalDeviceNestedCommandBufferPropertiesExt = 1000451001,

    ExternalMemoryAcquireUnmodifiedExt = 1000453000,

    PhysicalDeviceExtendedDynamicState3FeaturesExt = 1000455000,

    PhysicalDeviceExtendedDynamicState3PropertiesExt = 1000455001,

    PhysicalDeviceSubpassMergeFeedbackFeaturesExt = 1000458000,

    RenderPassCreationControlExt = 1000458001,

    RenderPassCreationFeedbackCreateInfoExt = 1000458002,

    RenderPassSubpassFeedbackCreateInfoExt = 1000458003,

    DirectDriverLoadingInfoLunarg = 1000459000,

    DirectDriverLoadingListLunarg = 1000459001,

    TensorCreateInfoArm = 1000460000,

    TensorViewCreateInfoArm = 1000460001,

    BindTensorMemoryInfoArm = 1000460002,

    WriteDescriptorSetTensorArm = 1000460003,

    PhysicalDeviceTensorPropertiesArm = 1000460004,

    TensorFormatPropertiesArm = 1000460005,

    TensorDescriptionArm = 1000460006,

    TensorMemoryRequirementsInfoArm = 1000460007,

    TensorMemoryBarrierArm = 1000460008,

    PhysicalDeviceTensorFeaturesArm = 1000460009,

    DeviceTensorMemoryRequirementsArm = 1000460010,

    CopyTensorInfoArm = 1000460011,

    TensorCopyArm = 1000460012,

    TensorDependencyInfoArm = 1000460013,

    MemoryDedicatedAllocateInfoTensorArm = 1000460014,

    PhysicalDeviceExternalTensorInfoArm = 1000460015,

    ExternalTensorPropertiesArm = 1000460016,

    ExternalMemoryTensorCreateInfoArm = 1000460017,

    PhysicalDeviceDescriptorBufferTensorFeaturesArm = 1000460018,

    PhysicalDeviceDescriptorBufferTensorPropertiesArm = 1000460019,

    DescriptorGetTensorInfoArm = 1000460020,

    TensorCaptureDescriptorDataInfoArm = 1000460021,

    TensorViewCaptureDescriptorDataInfoArm = 1000460022,

    FrameBoundaryTensorsArm = 1000460023,

    PhysicalDeviceShaderModuleIdentifierFeaturesExt = 1000462000,

    PhysicalDeviceShaderModuleIdentifierPropertiesExt = 1000462001,

    PipelineShaderStageModuleIdentifierCreateInfoExt = 1000462002,

    ShaderModuleIdentifierExt = 1000462003,

    PhysicalDeviceRasterizationOrderAttachmentAccessFeaturesExt = 1000342000,

    PhysicalDeviceOpticalFlowFeaturesNv = 1000464000,

    PhysicalDeviceOpticalFlowPropertiesNv = 1000464001,

    OpticalFlowImageFormatInfoNv = 1000464002,

    OpticalFlowImageFormatPropertiesNv = 1000464003,

    OpticalFlowSessionCreateInfoNv = 1000464004,

    OpticalFlowExecuteInfoNv = 1000464005,

    OpticalFlowSessionCreatePrivateDataInfoNv = 1000464010,

    PhysicalDeviceLegacyDitheringFeaturesExt = 1000465000,

    PhysicalDeviceExternalFormatResolveFeaturesAndroid = 1000468000,

    PhysicalDeviceExternalFormatResolvePropertiesAndroid = 1000468001,

    AndroidHardwareBufferFormatResolvePropertiesAndroid = 1000468002,

    PhysicalDeviceAntiLagFeaturesAmd = 1000476000,

    AntiLagDataAmd = 1000476001,

    AntiLagPresentationInfoAmd = 1000476002,

    SurfaceCapabilitiesPresentId2Khr = 1000479000,

    PresentId2Khr = 1000479001,

    PhysicalDevicePresentId2FeaturesKhr = 1000479002,

    SurfaceCapabilitiesPresentWait2Khr = 1000480000,

    PhysicalDevicePresentWait2FeaturesKhr = 1000480001,

    PresentWait2InfoKhr = 1000480002,

    PhysicalDeviceRayTracingPositionFetchFeaturesKhr = 1000481000,

    PhysicalDeviceShaderObjectFeaturesExt = 1000482000,

    PhysicalDeviceShaderObjectPropertiesExt = 1000482001,

    ShaderCreateInfoExt = 1000482002,

    PhysicalDevicePipelineBinaryFeaturesKhr = 1000483000,

    PipelineBinaryCreateInfoKhr = 1000483001,

    PipelineBinaryInfoKhr = 1000483002,

    PipelineBinaryKeyKhr = 1000483003,

    PhysicalDevicePipelineBinaryPropertiesKhr = 1000483004,

    ReleaseCapturedPipelineDataInfoKhr = 1000483005,

    PipelineBinaryDataInfoKhr = 1000483006,

    PipelineCreateInfoKhr = 1000483007,

    DevicePipelineBinaryInternalCacheControlKhr = 1000483008,

    PipelineBinaryHandlesInfoKhr = 1000483009,

    PhysicalDeviceTilePropertiesFeaturesQcom = 1000484000,

    TilePropertiesQcom = 1000484001,

    PhysicalDeviceAmigoProfilingFeaturesSec = 1000485000,

    AmigoProfilingSubmitInfoSec = 1000485001,

    SurfacePresentModeKhr = 1000274000,

    SurfacePresentScalingCapabilitiesKhr = 1000274001,

    SurfacePresentModeCompatibilityKhr = 1000274002,

    PhysicalDeviceSwapChainMaintenance1FeaturesKhr = 1000275000,

    SwapChainPresentFenceInfoKhr = 1000275001,

    SwapChainPresentModesCreateInfoKhr = 1000275002,

    SwapChainPresentModeInfoKhr = 1000275003,

    SwapChainPresentScalingCreateInfoKhr = 1000275004,

    ReleaseSwapChainImagesInfoKhr = 1000275005,

    PhysicalDeviceMultiviewPerViewViewportsFeaturesQcom = 1000488000,

    PhysicalDeviceRayTracingInvocationReorderFeaturesNv = 1000490000,

    PhysicalDeviceRayTracingInvocationReorderPropertiesNv = 1000490001,

    PhysicalDeviceCooperativeVectorFeaturesNv = 1000491000,

    PhysicalDeviceCooperativeVectorPropertiesNv = 1000491001,

    CooperativeVectorPropertiesNv = 1000491002,

    ConvertCooperativeVectorMatrixInfoNv = 1000491004,

    PhysicalDeviceExtendedSparseAddressSpaceFeaturesNv = 1000492000,

    PhysicalDeviceExtendedSparseAddressSpacePropertiesNv = 1000492001,

    PhysicalDeviceMutableDescriptorTypeFeaturesExt = 1000351000,

    MutableDescriptorTypeCreateInfoExt = 1000351002,

    PhysicalDeviceLegacyVertexAttributesFeaturesExt = 1000495000,

    PhysicalDeviceLegacyVertexAttributesPropertiesExt = 1000495001,

    LayerSettingsCreateInfoExt = 1000496000,

    PhysicalDeviceShaderCoreBuiltinsFeaturesArm = 1000497000,

    PhysicalDeviceShaderCoreBuiltinsPropertiesArm = 1000497001,

    PhysicalDevicePipelineLibraryGroupHandlesFeaturesExt = 1000498000,

    PhysicalDeviceDynamicRenderingUnusedAttachmentsFeaturesExt = 1000499000,

    LatencySleepModeInfoNv = 1000505000,

    LatencySleepInfoNv = 1000505001,

    SetLatencyMarkerInfoNv = 1000505002,

    GetLatencyMarkerInfoNv = 1000505003,

    LatencyTimingsFrameReportNv = 1000505004,

    LatencySubmissionPresentIdNv = 1000505005,

    OutOfBandQueueTypeInfoNv = 1000505006,

    SwapChainLatencyCreateInfoNv = 1000505007,

    LatencySurfaceCapabilitiesNv = 1000505008,

    PhysicalDeviceCooperativeMatrixFeaturesKhr = 1000506000,

    CooperativeMatrixPropertiesKhr = 1000506001,

    PhysicalDeviceCooperativeMatrixPropertiesKhr = 1000506002,

    DataGraphPipelineCreateInfoArm = 1000507000,

    DataGraphPipelineSessionCreateInfoArm = 1000507001,

    DataGraphPipelineResourceInfoArm = 1000507002,

    DataGraphPipelineConstantArm = 1000507003,

    DataGraphPipelineSessionMemoryRequirementsInfoArm = 1000507004,

    BindDataGraphPipelineSessionMemoryInfoArm = 1000507005,

    PhysicalDeviceDataGraphFeaturesArm = 1000507006,

    DataGraphPipelineShaderModuleCreateInfoArm = 1000507007,

    DataGraphPipelinePropertyQueryResultArm = 1000507008,

    DataGraphPipelineInfoArm = 1000507009,

    DataGraphPipelineCompilerControlCreateInfoArm = 1000507010,

    DataGraphPipelineSessionBindPointRequirementsInfoArm = 1000507011,

    DataGraphPipelineSessionBindPointRequirementArm = 1000507012,

    DataGraphPipelineIdentifierCreateInfoArm = 1000507013,

    DataGraphPipelineDispatchInfoArm = 1000507014,

    DataGraphProcessingEngineCreateInfoArm = 1000507016,

    QueueFamilyDataGraphProcessingEnginePropertiesArm = 1000507017,

    QueueFamilyDataGraphPropertiesArm = 1000507018,

    PhysicalDeviceQueueFamilyDataGraphProcessingEngineInfoArm = 1000507019,

    DataGraphPipelineConstantTensorSemiStructuredSparsityInfoArm = 1000507015,

    PhysicalDeviceMultiviewPerViewRenderAreasFeaturesQcom = 1000510000,

    MultiviewPerViewRenderAreasRenderPassBeginInfoQcom = 1000510001,

    PhysicalDeviceComputeShaderDerivativesFeaturesKhr = 1000201000,

    PhysicalDeviceComputeShaderDerivativesPropertiesKhr = 1000511000,

    VideoDecodeAv1CapabilitiesKhr = 1000512000,

    VideoDecodeAv1PictureInfoKhr = 1000512001,

    VideoDecodeAv1ProfileInfoKhr = 1000512003,

    VideoDecodeAv1SessionParametersCreateInfoKhr = 1000512004,

    VideoDecodeAv1DpbSlotInfoKhr = 1000512005,

    VideoEncodeAv1CapabilitiesKhr = 1000513000,

    VideoEncodeAv1SessionParametersCreateInfoKhr = 1000513001,

    VideoEncodeAv1PictureInfoKhr = 1000513002,

    VideoEncodeAv1DpbSlotInfoKhr = 1000513003,

    PhysicalDeviceVideoEncodeAv1FeaturesKhr = 1000513004,

    VideoEncodeAv1ProfileInfoKhr = 1000513005,

    VideoEncodeAv1RateControlInfoKhr = 1000513006,

    VideoEncodeAv1RateControlLayerInfoKhr = 1000513007,

    VideoEncodeAv1QualityLevelPropertiesKhr = 1000513008,

    VideoEncodeAv1SessionCreateInfoKhr = 1000513009,

    VideoEncodeAv1GopRemainingFrameInfoKhr = 1000513010,

    PhysicalDeviceVideoDecodeVp9FeaturesKhr = 1000514000,

    VideoDecodeVp9CapabilitiesKhr = 1000514001,

    VideoDecodeVp9PictureInfoKhr = 1000514002,

    VideoDecodeVp9ProfileInfoKhr = 1000514003,

    PhysicalDeviceVideoMaintenance1FeaturesKhr = 1000515000,

    VideoInlineQueryInfoKhr = 1000515001,

    PhysicalDevicePerStageDescriptorSetFeaturesNv = 1000516000,

    PhysicalDeviceImageProcessing2FeaturesQcom = 1000518000,

    PhysicalDeviceImageProcessing2PropertiesQcom = 1000518001,

    SamplerBlockMatchWindowCreateInfoQcom = 1000518002,

    SamplerCubicWeightsCreateInfoQcom = 1000519000,

    PhysicalDeviceCubicWeightsFeaturesQcom = 1000519001,

    BlitImageCubicWeightsInfoQcom = 1000519002,

    PhysicalDeviceYcbcrDegammaFeaturesQcom = 1000520000,

    SamplerYcbcrConversionYcbcrDegammaCreateInfoQcom = 1000520001,

    PhysicalDeviceCubicClampFeaturesQcom = 1000521000,

    PhysicalDeviceAttachmentFeedbackLoopDynamicStateFeaturesExt = 1000524000,

    PhysicalDeviceUnifiedImageLayoutsFeaturesKhr = 1000527000,

    AttachmentFeedbackLoopInfoExt = 1000527001,

    ScreenBufferPropertiesQnx = 1000529000,

    ScreenBufferFormatPropertiesQnx = 1000529001,

    ImportScreenBufferInfoQnx = 1000529002,

    ExternalFormatQnx = 1000529003,

    PhysicalDeviceExternalMemoryScreenBufferFeaturesQnx = 1000529004,

    PhysicalDeviceLayeredDriverPropertiesMsft = 1000530000,

    CalibratedTimestampInfoKhr = 1000184000,

    SetDescriptorBufferOffsetsInfoExt = 1000545007,

    BindDescriptorBufferEmbeddedSamplersInfoExt = 1000545008,

    PhysicalDeviceDescriptorPoolOverallocationFeaturesNv = 1000546000,

    PhysicalDeviceTileMemoryHeapFeaturesQcom = 1000547000,

    PhysicalDeviceTileMemoryHeapPropertiesQcom = 1000547001,

    TileMemoryRequirementsQcom = 1000547002,

    TileMemoryBindInfoQcom = 1000547003,

    TileMemorySizeInfoQcom = 1000547004,

    DisplaySurfaceStereoCreateInfoNv = 1000551000,

    DisplayModeStereoPropertiesNv = 1000551001,

    VideoEncodeIntraRefreshCapabilitiesKhr = 1000552000,

    VideoEncodeSessionIntraRefreshCreateInfoKhr = 1000552001,

    VideoEncodeIntraRefreshInfoKhr = 1000552002,

    VideoReferenceIntraRefreshInfoKhr = 1000552003,

    PhysicalDeviceVideoEncodeIntraRefreshFeaturesKhr = 1000552004,

    VideoEncodeQuantizationMapCapabilitiesKhr = 1000553000,

    VideoFormatQuantizationMapPropertiesKhr = 1000553001,

    VideoEncodeQuantizationMapInfoKhr = 1000553002,

    VideoEncodeQuantizationMapSessionParametersCreateInfoKhr = 1000553005,

    PhysicalDeviceVideoEncodeQuantizationMapFeaturesKhr = 1000553009,

    VideoEncodeH264QuantizationMapCapabilitiesKhr = 1000553003,

    VideoEncodeH265QuantizationMapCapabilitiesKhr = 1000553004,

    VideoFormatH265QuantizationMapPropertiesKhr = 1000553006,

    VideoEncodeAv1QuantizationMapCapabilitiesKhr = 1000553007,

    VideoFormatAv1QuantizationMapPropertiesKhr = 1000553008,

    PhysicalDeviceRawAccessChainsFeaturesNv = 1000555000,

    ExternalComputeQueueDeviceCreateInfoNv = 1000556000,

    ExternalComputeQueueCreateInfoNv = 1000556001,

    ExternalComputeQueueDataParamsNv = 1000556002,

    PhysicalDeviceExternalComputeQueuePropertiesNv = 1000556003,

    PhysicalDeviceShaderRelaxedExtendedInstructionFeaturesKhr = 1000558000,

    PhysicalDeviceCommandBufferInheritanceFeaturesNv = 1000559000,

    PhysicalDeviceMaintenance7FeaturesKhr = 1000562000,

    PhysicalDeviceMaintenance7PropertiesKhr = 1000562001,

    PhysicalDeviceLayeredApiPropertiesListKhr = 1000562002,

    PhysicalDeviceLayeredApiPropertiesKhr = 1000562003,

    PhysicalDeviceLayeredApiVulkanPropertiesKhr = 1000562004,

    PhysicalDeviceShaderAtomicFloat16VectorFeaturesNv = 1000563000,

    PhysicalDeviceShaderReplicatedCompositesFeaturesExt = 1000564000,

    PhysicalDeviceShaderFloat8FeaturesExt = 1000567000,

    PhysicalDeviceRayTracingValidationFeaturesNv = 1000568000,

    PhysicalDeviceClusterAccelerationStructureFeaturesNv = 1000569000,

    PhysicalDeviceClusterAccelerationStructurePropertiesNv = 1000569001,

    ClusterAccelerationStructureClustersBottomLevelInputNv = 1000569002,

    ClusterAccelerationStructureTriangleClusterInputNv = 1000569003,

    ClusterAccelerationStructureMoveObjectsInputNv = 1000569004,

    ClusterAccelerationStructureInputInfoNv = 1000569005,

    ClusterAccelerationStructureCommandsInfoNv = 1000569006,

    RayTracingPipelineClusterAccelerationStructureCreateInfoNv = 1000569007,

    PhysicalDevicePartitionedAccelerationStructureFeaturesNv = 1000570000,

    PhysicalDevicePartitionedAccelerationStructurePropertiesNv = 1000570001,

    WriteDescriptorSetPartitionedAccelerationStructureNv = 1000570002,

    PartitionedAccelerationStructureInstancesInputNv = 1000570003,

    BuildPartitionedAccelerationStructureInfoNv = 1000570004,

    PartitionedAccelerationStructureFlagsNv = 1000570005,

    PhysicalDeviceDeviceGeneratedCommandsFeaturesExt = 1000572000,

    PhysicalDeviceDeviceGeneratedCommandsPropertiesExt = 1000572001,

    GeneratedCommandsMemoryRequirementsInfoExt = 1000572002,

    IndirectExecutionSetCreateInfoExt = 1000572003,

    GeneratedCommandsInfoExt = 1000572004,

    IndirectCommandsLayoutCreateInfoExt = 1000572006,

    IndirectCommandsLayoutTokenExt = 1000572007,

    WriteIndirectExecutionSetPipelineExt = 1000572008,

    WriteIndirectExecutionSetShaderExt = 1000572009,

    IndirectExecutionSetPipelineInfoExt = 1000572010,

    IndirectExecutionSetShaderInfoExt = 1000572011,

    IndirectExecutionSetShaderLayoutInfoExt = 1000572012,

    GeneratedCommandsPipelineInfoExt = 1000572013,

    GeneratedCommandsShaderInfoExt = 1000572014,

    PhysicalDeviceMaintenance8FeaturesKhr = 1000574000,

    MemoryBarrierAccessFlags3Khr = 1000574002,

    PhysicalDeviceImageAlignmentControlFeaturesMesa = 1000575000,

    PhysicalDeviceImageAlignmentControlPropertiesMesa = 1000575001,

    ImageAlignmentControlCreateInfoMesa = 1000575002,

    PhysicalDeviceDepthClampControlFeaturesExt = 1000582000,

    PipelineViewportDepthClampControlCreateInfoExt = 1000582001,

    PhysicalDeviceMaintenance9FeaturesKhr = 1000584000,

    PhysicalDeviceMaintenance9PropertiesKhr = 1000584001,

    QueueFamilyOwnershipTransferPropertiesKhr = 1000584002,

    PhysicalDeviceVideoMaintenance2FeaturesKhr = 1000586000,

    VideoDecodeH264InlineSessionParametersInfoKhr = 1000586001,

    VideoDecodeH265InlineSessionParametersInfoKhr = 1000586002,

    VideoDecodeAv1InlineSessionParametersInfoKhr = 1000586003,

    OhSurfaceCreateInfoOhos = 1000587000,

    PhysicalDeviceHdrVividFeaturesHuawei = 1000590000,

    HdrVividDynamicMetadataHuawei = 1000590001,

    PhysicalDeviceCooperativeMatrix2FeaturesNv = 1000593000,

    CooperativeMatrixFlexibleDimensionsPropertiesNv = 1000593001,

    PhysicalDeviceCooperativeMatrix2PropertiesNv = 1000593002,

    PhysicalDevicePipelineOpacityMicromapFeaturesArm = 1000596000,

    ImportMemoryMetalHandleInfoExt = 1000602000,

    MemoryMetalHandlePropertiesExt = 1000602001,

    MemoryGetMetalHandleInfoExt = 1000602002,

    PhysicalDeviceDepthClampZeroOneFeaturesKhr = 1000421000,

    PhysicalDeviceVertexAttributeRobustnessFeaturesExt = 1000608000,

    PhysicalDeviceFormatPackFeaturesArm = 1000609000,

    PhysicalDeviceFragmentDensityMapLayeredFeaturesValve = 1000611000,

    PhysicalDeviceFragmentDensityMapLayeredPropertiesValve = 1000611001,

    PipelineFragmentDensityMapLayeredCreateInfoValve = 1000611002,

    PhysicalDeviceRobustness2FeaturesKhr = 1000286000,

    PhysicalDeviceRobustness2PropertiesKhr = 1000286001,

    PhysicalDeviceFragmentDensityMapOffsetFeaturesExt = 1000425000,

    PhysicalDeviceFragmentDensityMapOffsetPropertiesExt = 1000425001,

    RenderPassFragmentDensityMapOffsetEndInfoExt = 1000425002,

    RenderingEndInfoExt = 1000619003,

    PhysicalDeviceZeroInitializeDeviceMemoryFeaturesExt = 1000620000,

    PhysicalDevicePresentModeFifoLatestReadyFeaturesKhr = 1000361000,

    PhysicalDevicePipelineCacheIncrementalModeFeaturesSec = 1000637000,

    PhysicalDeviceVariablePointerFeatures = 1000120000,

    PhysicalDeviceShaderDrawParameterFeatures = 1000063000,

    DebugReportCreateInfoExt = 1000011000,

    RenderingInfoKhr = 1000044000,

    RenderingAttachmentInfoKhr = 1000044001,

    PipelineRenderingCreateInfoKhr = 1000044002,

    PhysicalDeviceDynamicRenderingFeaturesKhr = 1000044003,

    CommandBufferInheritanceRenderingInfoKhr = 1000044004,

    RenderPassMultiviewCreateInfoKhr = 1000053000,

    PhysicalDeviceMultiviewFeaturesKhr = 1000053001,

    PhysicalDeviceMultiviewPropertiesKhr = 1000053002,

    PhysicalDeviceFeatures2Khr = 1000059000,

    PhysicalDeviceProperties2Khr = 1000059001,

    FormatProperties2Khr = 1000059002,

    ImageFormatProperties2Khr = 1000059003,

    PhysicalDeviceImageFormatInfo2Khr = 1000059004,

    QueueFamilyProperties2Khr = 1000059005,

    PhysicalDeviceMemoryProperties2Khr = 1000059006,

    SparseImageFormatProperties2Khr = 1000059007,

    PhysicalDeviceSparseImageFormatInfo2Khr = 1000059008,

    MemoryAllocateFlagsInfoKhr = 1000060000,

    DeviceGroupRenderPassBeginInfoKhr = 1000060003,

    DeviceGroupCommandBufferBeginInfoKhr = 1000060004,

    DeviceGroupSubmitInfoKhr = 1000060005,

    DeviceGroupBindSparseInfoKhr = 1000060006,

    BindBufferMemoryDeviceGroupInfoKhr = 1000060013,

    BindImageMemoryDeviceGroupInfoKhr = 1000060014,

    PhysicalDeviceTextureCompressionAstcHdrFeaturesExt = 1000066000,

    PipelineRobustnessCreateInfoExt = 1000068000,

    PhysicalDevicePipelineRobustnessFeaturesExt = 1000068001,

    PhysicalDevicePipelineRobustnessPropertiesExt = 1000068002,

    PhysicalDeviceGroupPropertiesKhr = 1000070000,

    DeviceGroupDeviceCreateInfoKhr = 1000070001,

    PhysicalDeviceExternalImageFormatInfoKhr = 1000071000,

    ExternalImageFormatPropertiesKhr = 1000071001,

    PhysicalDeviceExternalBufferInfoKhr = 1000071002,

    ExternalBufferPropertiesKhr = 1000071003,

    PhysicalDeviceIdPropertiesKhr = 1000071004,

    ExternalMemoryBufferCreateInfoKhr = 1000072000,

    ExternalMemoryImageCreateInfoKhr = 1000072001,

    ExportMemoryAllocateInfoKhr = 1000072002,

    PhysicalDeviceExternalSemaphoreInfoKhr = 1000076000,

    ExternalSemaphorePropertiesKhr = 1000076001,

    ExportSemaphoreCreateInfoKhr = 1000077000,

    PhysicalDevicePushDescriptorPropertiesKhr = 1000080000,

    PhysicalDeviceShaderFloat16Int8FeaturesKhr = 1000082000,

    PhysicalDeviceFloat16Int8FeaturesKhr = 1000082000,

    PhysicalDevice16BitStorageFeaturesKhr = 1000083000,

    DescriptorUpdateTemplateCreateInfoKhr = 1000085000,

    Surface_Capabilities2_Ext = 1000090000,

    PhysicalDeviceImagelessFramebufferFeaturesKhr = 1000108000,

    FramebufferAttachmentsCreateInfoKhr = 1000108001,

    FramebufferAttachmentImageInfoKhr = 1000108002,

    RenderPassAttachmentBeginInfoKhr = 1000108003,

    AttachmentDescription2Khr = 1000109000,

    AttachmentReference2Khr = 1000109001,

    SubpassDescription2Khr = 1000109002,

    SubpassDependency2Khr = 1000109003,

    RenderPassCreateInfo2Khr = 1000109004,

    SubpassBeginInfoKhr = 1000109005,

    SubpassEndInfoKhr = 1000109006,

    PhysicalDeviceExternalFenceInfoKhr = 1000112000,

    ExternalFencePropertiesKhr = 1000112001,

    ExportFenceCreateInfoKhr = 1000113000,

    PhysicalDevicePointClippingPropertiesKhr = 1000117000,

    RenderPassInputAttachmentAspectCreateInfoKhr = 1000117001,

    ImageViewUsageCreateInfoKhr = 1000117002,

    PipelineTessellationDomainOriginStateCreateInfoKhr = 1000117003,

    PhysicalDeviceVariablePointersFeaturesKhr = 1000120000,

    PhysicalDeviceVariablePointerFeaturesKhr = 1000120000,

    MemoryDedicatedRequirementsKhr = 1000127000,

    MemoryDedicatedAllocateInfoKhr = 1000127001,

    PhysicalDeviceSamplerFilterMinmaxPropertiesExt = 1000130000,

    SamplerReductionModeCreateInfoExt = 1000130001,

    PhysicalDeviceInlineUniformBlockFeaturesExt = 1000138000,

    PhysicalDeviceInlineUniformBlockPropertiesExt = 1000138001,

    WriteDescriptorSetInlineUniformBlockExt = 1000138002,

    DescriptorPoolInlineUniformBlockCreateInfoExt = 1000138003,

    BufferMemoryRequirementsInfo2Khr = 1000146000,

    ImageMemoryRequirementsInfo2Khr = 1000146001,

    ImageSparseMemoryRequirementsInfo2Khr = 1000146002,

    MemoryRequirements2Khr = 1000146003,

    SparseImageMemoryRequirements2Khr = 1000146004,

    ImageFormatListCreateInfoKhr = 1000147000,

    AttachmentSampleCountInfoNv = 1000044008,

    SamplerYcbcrConversionCreateInfoKhr = 1000156000,

    SamplerYcbcrConversionInfoKhr = 1000156001,

    BindImagePlaneMemoryInfoKhr = 1000156002,

    ImagePlaneMemoryRequirementsInfoKhr = 1000156003,

    PhysicalDeviceSamplerYcbcrConversionFeaturesKhr = 1000156004,

    SamplerYcbcrConversionImageFormatPropertiesKhr = 1000156005,

    BindBufferMemoryInfoKhr = 1000157000,

    BindImageMemoryInfoKhr = 1000157001,

    DescriptorSetLayoutBindingFlagsCreateInfoExt = 1000161000,

    PhysicalDeviceDescriptorIndexingFeaturesExt = 1000161001,

    PhysicalDeviceDescriptorIndexingPropertiesExt = 1000161002,

    DescriptorSetVariableDescriptorCountAllocateInfoExt = 1000161003,

    DescriptorSetVariableDescriptorCountLayoutSupportExt = 1000161004,

    PhysicalDeviceMaintenance3PropertiesKhr = 1000168000,

    DescriptorSetLayoutSupportKhr = 1000168001,

    DeviceQueueGlobalPriorityCreateInfoExt = 1000174000,

    PhysicalDeviceShaderSubgroupExtendedTypesFeaturesKhr = 1000175000,

    PhysicalDevice8BitStorageFeaturesKhr = 1000177000,

    PhysicalDeviceShaderAtomicInt64FeaturesKhr = 1000180000,

    CalibratedTimestampInfoExt = 1000184000,

    DeviceQueueGlobalPriorityCreateInfoKhr = 1000174000,

    PhysicalDeviceGlobalPriorityQueryFeaturesKhr = 1000388000,

    QueueFamilyGlobalPriorityPropertiesKhr = 1000388001,

    PipelineVertexInputDivisorStateCreateInfoExt = 1000190001,

    PhysicalDeviceVertexAttributeDivisorFeaturesExt = 1000190002,

    PipelineCreationFeedbackCreateInfoExt = 1000192000,

    PhysicalDeviceDriverPropertiesKhr = 1000196000,

    PhysicalDeviceFloatControlsPropertiesKhr = 1000197000,

    PhysicalDeviceDepthStencilResolvePropertiesKhr = 1000199000,

    SubpassDescriptionDepthStencilResolveKhr = 1000199001,

    PhysicalDeviceComputeShaderDerivativesFeaturesNv = 1000201000,

    PhysicalDeviceFragmentShaderBarycentricFeaturesNv = 1000203000,

    PhysicalDeviceTimelineSemaphoreFeaturesKhr = 1000207000,

    PhysicalDeviceTimelineSemaphorePropertiesKhr = 1000207001,

    SemaphoreTypeCreateInfoKhr = 1000207002,

    TimelineSemaphoreSubmitInfoKhr = 1000207003,

    SemaphoreWaitInfoKhr = 1000207004,

    SemaphoreSignalInfoKhr = 1000207005,

    QueryPoolCreateInfoIntel = 1000210000,

    PhysicalDeviceVulkanMemoryModelFeaturesKhr = 1000211000,

    PhysicalDeviceShaderTerminateInvocationFeaturesKhr = 1000215000,

    PhysicalDeviceScalarBlockLayoutFeaturesExt = 1000221000,

    PhysicalDeviceSubgroupSizeControlPropertiesExt = 1000225000,

    PipelineShaderStageRequiredSubgroupSizeCreateInfoExt = 1000225001,

    PhysicalDeviceSubgroupSizeControlFeaturesExt = 1000225002,

    PhysicalDeviceDynamicRenderingLocalReadFeaturesKhr = 1000232000,

    RenderingAttachmentLocationInfoKhr = 1000232001,

    RenderingInputAttachmentIndexInfoKhr = 1000232002,

    PhysicalDeviceSeparateDepthStencilLayoutsFeaturesKhr = 1000241000,

    AttachmentReferenceStencilLayoutKhr = 1000241001,

    AttachmentDescriptionStencilLayoutKhr = 1000241002,

    PhysicalDeviceBufferAddressFeaturesExt = 1000244000,

    BufferDeviceAddressInfoExt = 1000244001,

    PhysicalDeviceToolPropertiesExt = 1000245000,

    ImageStencilUsageCreateInfoExt = 1000246000,

    PhysicalDeviceUniformBufferStandardLayoutFeaturesKhr = 1000253000,

    PhysicalDeviceBufferDeviceAddressFeaturesKhr = 1000257000,

    BufferDeviceAddressInfoKhr = 1000244001,

    BufferOpaqueCaptureAddressCreateInfoKhr = 1000257002,

    MemoryOpaqueCaptureAddressAllocateInfoKhr = 1000257003,

    DeviceMemoryOpaqueCaptureAddressInfoKhr = 1000257004,

    PhysicalDeviceLineRasterizationFeaturesExt = 1000259000,

    PipelineRasterizationLineStateCreateInfoExt = 1000259001,

    PhysicalDeviceLineRasterizationPropertiesExt = 1000259002,

    PhysicalDeviceHostQueryResetFeaturesExt = 1000261000,

    PhysicalDeviceIndexTypeUint8FeaturesExt = 1000265000,

    PhysicalDeviceHostImageCopyFeaturesExt = 1000270000,

    PhysicalDeviceHostImageCopyPropertiesExt = 1000270001,

    MemoryToImageCopyExt = 1000270002,

    ImageToMemoryCopyExt = 1000270003,

    CopyImageToMemoryInfoExt = 1000270004,

    CopyMemoryToImageInfoExt = 1000270005,

    HostImageLayoutTransitionInfoExt = 1000270006,

    CopyImageToImageInfoExt = 1000270007,

    SubresourceHostMemcpySizeExt = 1000270008,

    HostImageCopyDevicePerformanceQueryExt = 1000270009,

    MemoryMapInfoKhr = 1000271000,

    MemoryUnmapInfoKhr = 1000271001,

    SurfacePresentModeExt = 1000274000,

    SurfacePresentScalingCapabilitiesExt = 1000274001,

    SurfacePresentModeCompatibilityExt = 1000274002,

    PhysicalDeviceSwapChainMaintenance1FeaturesExt = 1000275000,

    SwapChainPresentFenceInfoExt = 1000275001,

    SwapChainPresentModesCreateInfoExt = 1000275002,

    SwapChainPresentModeInfoExt = 1000275003,

    SwapChainPresentScalingCreateInfoExt = 1000275004,

    ReleaseSwapChainImagesInfoExt = 1000275005,

    PhysicalDeviceShaderDemoteToHelperInvocationFeaturesExt = 1000276000,

    PhysicalDeviceShaderIntegerDotProductFeaturesKhr = 1000280000,

    PhysicalDeviceShaderIntegerDotProductPropertiesKhr = 1000280001,

    PhysicalDeviceTexelBufferAlignmentPropertiesExt = 1000281001,

    PhysicalDeviceRobustness2FeaturesExt = 1000286000,

    PhysicalDeviceRobustness2PropertiesExt = 1000286001,

    PhysicalDevicePrivateDataFeaturesExt = 1000295000,

    DevicePrivateDataCreateInfoExt = 1000295001,

    PrivateDataSlotCreateInfoExt = 1000295002,

    PhysicalDevicePipelineCreationCacheControlFeaturesExt = 1000297000,

    MemoryBarrier2Khr = 1000314000,

    BufferMemoryBarrier2Khr = 1000314001,

    ImageMemoryBarrier2Khr = 1000314002,

    DependencyInfoKhr = 1000314003,

    SubmitInfo2Khr = 1000314004,

    SemaphoreSubmitInfoKhr = 1000314005,

    CommandBufferSubmitInfoKhr = 1000314006,

    PhysicalDeviceSynchronization2FeaturesKhr = 1000314007,

    PhysicalDeviceZeroInitializeWorkgroupMemoryFeaturesKhr = 1000325000,

    PhysicalDeviceImageRobustnessFeaturesExt = 1000335000,

    CopyBufferInfo2Khr = 1000337000,

    CopyImageInfo2Khr = 1000337001,

    CopyBufferToImageInfo2Khr = 1000337002,

    CopyImageToBufferInfo2Khr = 1000337003,

    BlitImageInfo2Khr = 1000337004,

    ResolveImageInfo2Khr = 1000337005,

    BufferCopy2Khr = 1000337006,

    ImageCopy2Khr = 1000337007,

    ImageBlit2Khr = 1000337008,

    BufferImageCopy2Khr = 1000337009,

    ImageResolve2Khr = 1000337010,

    SubresourceLayout2Ext = 1000338002,

    ImageSubresource2Ext = 1000338003,

    PhysicalDeviceRasterizationOrderAttachmentAccessFeaturesArm = 1000342000,

    PhysicalDeviceMutableDescriptorTypeFeaturesValve = 1000351000,

    MutableDescriptorTypeCreateInfoValve = 1000351002,

    FormatProperties3Khr = 1000360000,

    PhysicalDevicePresentModeFifoLatestReadyFeaturesExt = 1000361000,

    PipelineInfoExt = 1000269001,

    PhysicalDeviceGlobalPriorityQueryFeaturesExt = 1000388000,

    QueueFamilyGlobalPriorityPropertiesExt = 1000388001,

    PhysicalDeviceMaintenance4FeaturesKhr = 1000413000,

    PhysicalDeviceMaintenance4PropertiesKhr = 1000413001,

    DeviceBufferMemoryRequirementsKhr = 1000413002,

    DeviceImageMemoryRequirementsKhr = 1000413003,

    PhysicalDeviceShaderSubgroupRotateFeaturesKhr = 1000416000,

    PhysicalDeviceDepthClampZeroOneFeaturesExt = 1000421000,

    PhysicalDeviceFragmentDensityMapOffsetFeaturesQcom = 1000425000,

    PhysicalDeviceFragmentDensityMapOffsetPropertiesQcom = 1000425001,

    SubpassFragmentDensityMapOffsetEndInfoQcom = 1000425002,

    PhysicalDevicePipelineProtectedAccessFeaturesExt = 1000466000,

    PhysicalDeviceMaintenance5FeaturesKhr = 1000470000,

    PhysicalDeviceMaintenance5PropertiesKhr = 1000470001,

    RenderingAreaInfoKhr = 1000470003,

    DeviceImageSubresourceInfoKhr = 1000470004,

    SubresourceLayout2Khr = 1000338002,

    ImageSubresource2Khr = 1000338003,

    PipelineCreateFlags2CreateInfoKhr = 1000470005,

    BufferUsageFlags2CreateInfoKhr = 1000470006,

    ShaderRequiredSubgroupSizeCreateInfoExt = 1000225001,

    PhysicalDeviceVertexAttributeDivisorPropertiesKhr = 1000525000,

    PipelineVertexInputDivisorStateCreateInfoKhr = 1000190001,

    PhysicalDeviceVertexAttributeDivisorFeaturesKhr = 1000190002,

    PhysicalDeviceShaderFloatControls2FeaturesKhr = 1000528000,

    PhysicalDeviceIndexTypeUint8FeaturesKhr = 1000265000,

    PhysicalDeviceLineRasterizationFeaturesKhr = 1000259000,

    PipelineRasterizationLineStateCreateInfoKhr = 1000259001,

    PhysicalDeviceLineRasterizationPropertiesKhr = 1000259002,

    PhysicalDeviceShaderExpectAssumeFeaturesKhr = 1000544000,

    PhysicalDeviceMaintenance6FeaturesKhr = 1000545000,

    PhysicalDeviceMaintenance6PropertiesKhr = 1000545001,

    BindMemoryStatusKhr = 1000545002,

    BindDescriptorSetsInfoKhr = 1000545003,

    PushConstantsInfoKhr = 1000545004,

    PushDescriptorSetInfoKhr = 1000545005,

    PushDescriptorSetWithTemplateInfoKhr = 1000545006,

    MaxEnum = 2147483647,
}
