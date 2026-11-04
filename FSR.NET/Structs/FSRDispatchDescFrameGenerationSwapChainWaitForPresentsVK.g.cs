namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 16)]
public struct FSRDispatchDescFrameGenerationSwapChainWaitForPresentsVK
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    public const ulong Type = Fsr.APIDispatchDescTypeFGSwapChainWaitForPresentsVK;

    public FSRDispatchDescFrameGenerationSwapChainWaitForPresentsVK()
    {
        this = default;
        Header.Type = Type;
    }
}
