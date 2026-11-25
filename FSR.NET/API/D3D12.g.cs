namespace FSR.NET;

public static unsafe partial class Fsr
{
    public static partial class D3D12
    {
        [LibraryImport(D3D12LibraryName, EntryPoint = "ffxConfigure")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial FSRApiReturnCodes Configure(FSRContext* context, FSRApiHeader* desc);

        [LibraryImport(D3D12LibraryName, EntryPoint = "ffxCreateContext")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial FSRApiReturnCodes CreateContext(FSRContext* context, FSRApiHeader* desc, FSRAllocationCallbacks* memCb);

        [LibraryImport(D3D12LibraryName, EntryPoint = "ffxDestroyContext")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial FSRApiReturnCodes DestroyContext(FSRContext* context, FSRAllocationCallbacks* memCb);

        [LibraryImport(D3D12LibraryName, EntryPoint = "ffxDispatch")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial FSRApiReturnCodes Dispatch(FSRContext* context, FSRApiHeader* desc);

        [LibraryImport(D3D12LibraryName, EntryPoint = "ffxQuery")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial FSRApiReturnCodes Query(FSRContext* context, FSRApiHeader* desc);

        static D3D12()
        {
            NativeLoader.Register();
        }
    }
}
