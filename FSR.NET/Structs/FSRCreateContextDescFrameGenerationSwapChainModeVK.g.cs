namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public struct FSRCreateContextDescFrameGenerationSwapChainModeVK
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public Bool8 ComposeOnPresentQueue;

    public const ulong Type = Fsr.APICreateContextDescTypeFGSwapChainModeVK;

    public FSRCreateContextDescFrameGenerationSwapChainModeVK()
    {
        this = default;
        Header.Type = Type;
    }
}
