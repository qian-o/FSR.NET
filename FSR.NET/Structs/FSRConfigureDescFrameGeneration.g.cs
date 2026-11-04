namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 144)]
public unsafe struct FSRConfigureDescFrameGeneration
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public void* SwapChain;

    [FieldOffset(24)]
    public delegate* unmanaged[Cdecl]<FSRCallbackDescFrameGenerationPresent*, void*, FSRApiReturnCodes> PresentCallback;

    [FieldOffset(32)]
    public void* PresentCallbackUserContext;

    [FieldOffset(40)]
    public delegate* unmanaged[Cdecl]<FSRDispatchDescFrameGeneration*, void*, FSRApiReturnCodes> FrameGenerationCallback;

    [FieldOffset(48)]
    public void* FrameGenerationCallbackUserContext;

    [FieldOffset(56)]
    public Bool8 FrameGenerationEnabled;

    [FieldOffset(57)]
    public Bool8 AllowAsyncWorkloads;

    [FieldOffset(64)]
    public FSRApiResource HUDLessColor;

    [FieldOffset(112)]
    public uint Flags;

    [FieldOffset(116)]
    public Bool8 OnlyPresentGenerated;

    [FieldOffset(120)]
    public FSRApiRect2D GenerationRect;

    [FieldOffset(136)]
    public ulong FrameID;

    public const ulong Type = Fsr.APIConfigureDescTypeFrameGeneration;

    public FSRConfigureDescFrameGeneration()
    {
        this = default;
        Header.Type = Type;
    }
}
