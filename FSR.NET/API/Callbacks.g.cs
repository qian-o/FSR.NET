namespace FSR.NET;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRApiConstantBufferAllocation FSRApiConstantBufferAllocator(void* arg0, ulong arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRApiReturnCodes FSRApiFrameGenerationDispatchFunc(FSRDispatchDescFrameGeneration* arg0, void* arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRApiReturnCodes FSRApiPresentCallbackFunc(FSRCallbackDescFrameGenerationPresent* arg0, void* arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate int FSRWaitCallbackFunc(char* arg0, ulong arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate ulong FSRGetLastPresentCountFFXAPI(nint arg0);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRVkResult FSRVkCreateSwapChainFFXAPI(nint arg0, FSRVkSwapChainCreateInfoKHR* arg1, FSRVkAllocationCallbacks* arg2, nint* arg3, void* arg4);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void FSRVkDestroySwapChainFFXAPI(nint arg0, nint arg1, FSRVkAllocationCallbacks* arg2, void* arg3);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRVkResult FSRVkQueueSubmitFFXAPI(uint arg0, FSRVkSubmitInfo* arg1, nint arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRApiReturnCodes FSRConfigure(FSRContext* arg0, FSRApiHeader* arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRApiReturnCodes FSRCreateContext(FSRContext* arg0, FSRApiHeader* arg1, FSRAllocationCallbacks* arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRApiReturnCodes FSRDestroyContext(FSRContext* arg0, FSRAllocationCallbacks* arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRApiReturnCodes FSRDispatch(FSRContext* arg0, FSRApiHeader* arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRApiReturnCodes FSRHeapAllocatorFuncDX12(uint arg0, FSRD3D12HeapDesc* arg1, byte arg2, nint* arg3, ulong* arg4);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRApiReturnCodes FSRHeapDeallocatorFuncDX12(uint arg0, nint arg1, ulong arg2, ulong arg3);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRApiReturnCodes FSRQuery(FSRContext* arg0, FSRApiHeader* arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRApiReturnCodes FSRResourceAllocatorFuncDX12(uint arg0, FSRD3D12ResourceStates arg1, FSRD3D12HeapProperties* arg2, FSRD3D12ResourceDesc* arg3, FSRApiResourceDescription* arg4, FSRD3D12ClearValue* arg5, nint* arg6);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRApiReturnCodes FSRResourceAllocatorFuncVK(uint arg0);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRApiReturnCodes FSRResourceDeallocatorFuncDX12(uint arg0, nint arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate FSRApiReturnCodes FSRResourceDeallocatorFuncVK(uint arg0);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void* FSRAlloc(void* arg0, ulong arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void FSRApiMessage(uint arg0, char* arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void FSRDealloc(void* arg0, void* arg1);
