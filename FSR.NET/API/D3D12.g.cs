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
        public static partial FSRApiReturnCodes CreateContext(FSRContext* context, FSRApiHeader* desc, FSRAllocationCallbacks* memCb = null);

        [LibraryImport(D3D12LibraryName, EntryPoint = "ffxDestroyContext")]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        public static partial FSRApiReturnCodes DestroyContext(FSRContext* context, FSRAllocationCallbacks* memCb = null);

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

        public static FSRApiReturnCodes Configure(ref FSRContext context, ref FSRApiHeader desc)
        {
            fixed (FSRContext* pContext = &context)
            {
                fixed (FSRApiHeader* pDesc = &desc)
                {
                    return Configure(pContext, pDesc);
                }
            }
        }

        public static FSRApiReturnCodes Configure(ref FSRApiHeader desc)
        {
            fixed (FSRApiHeader* pDesc = &desc)
            {
                return Configure(null, pDesc);
            }
        }

        public static FSRApiReturnCodes CreateContext(out FSRContext context, ref FSRApiHeader desc, FSRAllocationCallbacks* memCb = null)
        {
            fixed (FSRContext* pContext = &context)
            {
                fixed (FSRApiHeader* pDesc = &desc)
                {
                    return CreateContext(pContext, pDesc, memCb);
                }
            }
        }

        public static FSRApiReturnCodes DestroyContext(ref FSRContext context, FSRAllocationCallbacks* memCb = null)
        {
            fixed (FSRContext* pContext = &context)
            {
                return DestroyContext(pContext, memCb);
            }
        }

        public static FSRApiReturnCodes Dispatch(ref FSRContext context, ref FSRApiHeader desc)
        {
            fixed (FSRContext* pContext = &context)
            {
                fixed (FSRApiHeader* pDesc = &desc)
                {
                    return Dispatch(pContext, pDesc);
                }
            }
        }

        public static FSRApiReturnCodes Query(ref FSRContext context, ref FSRApiHeader desc)
        {
            fixed (FSRContext* pContext = &context)
            {
                fixed (FSRApiHeader* pDesc = &desc)
                {
                    return Query(pContext, pDesc);
                }
            }
        }

        public static FSRApiReturnCodes Query(ref FSRApiHeader desc)
        {
            fixed (FSRApiHeader* pDesc = &desc)
            {
                return Query(null, pDesc);
            }
        }
    }
}
