namespace FSR.NET;

public static unsafe partial class Fsr
{
    public static partial class Vulkan
    {
        [LibraryImport(VulkanLibraryName, EntryPoint = "ffxConfigure")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial FSRApiReturnCodes Configure(FSRContext* context, FSRApiHeader* desc);

        [LibraryImport(VulkanLibraryName, EntryPoint = "ffxCreateContext")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial FSRApiReturnCodes CreateContext(FSRContext* context, FSRApiHeader* desc, FSRAllocationCallbacks* memCb);

        [LibraryImport(VulkanLibraryName, EntryPoint = "ffxDestroyContext")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial FSRApiReturnCodes DestroyContext(FSRContext* context, FSRAllocationCallbacks* memCb);

        [LibraryImport(VulkanLibraryName, EntryPoint = "ffxDispatch")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial FSRApiReturnCodes Dispatch(FSRContext* context, FSRApiHeader* desc);

        [LibraryImport(VulkanLibraryName, EntryPoint = "ffxQuery")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial FSRApiReturnCodes Query(FSRContext* context, FSRApiHeader* desc);

        static Vulkan()
        {
            NativeLoader.Register();
        }
    }
}
