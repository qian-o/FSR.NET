namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 152)]
public unsafe struct FSRFrameGenerationConfig
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public void* SwapChain;

    [FieldOffset(24)]
    public delegate* unmanaged[Cdecl]<FSRCallbackDescFrameGenerationPresent*, void*, FSRApiReturnCodes> PresentCallback;

    [FieldOffset(32)]
    public void* PresentCallbackContext;

    [FieldOffset(40)]
    public delegate* unmanaged[Cdecl]<FSRDispatchDescFrameGeneration*, void*, FSRApiReturnCodes> FrameGenerationCallback;

    [FieldOffset(48)]
    public void* FrameGenerationCallbackContext;

    [FieldOffset(56)]
    public Bool8 FrameGenerationEnabled;

    [FieldOffset(57)]
    public Bool8 AllowAsyncWorkloads;

    [FieldOffset(58)]
    public Bool8 AllowAsyncPresent;

    [FieldOffset(64)]
    public FSRApiResource HUDLessColor;

    [FieldOffset(112)]
    public uint Flags;

    [FieldOffset(116)]
    public Bool8 OnlyPresentInterpolated;

    [FieldOffset(120)]
    public FSRApiRect2D InterpolationRect;

    [FieldOffset(136)]
    public ulong FrameID;

    [FieldOffset(144)]
    public Bool8 DrawDebugPacingLines;

    public const ulong Type = Fsr.APIFrameGenerationConfig;

    public FSRFrameGenerationConfig()
    {
        this = default;
        Header.Type = Type;
    }
}
