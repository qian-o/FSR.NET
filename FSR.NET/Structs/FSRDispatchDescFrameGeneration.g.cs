namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 312)]
public unsafe struct FSRDispatchDescFrameGeneration
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public void* CommandList;

    [FieldOffset(24)]
    public FSRApiResource PresentColor;

    [FieldOffset(72)]
    public OutputsBuffer Outputs;

    [FieldOffset(264)]
    public uint NumGeneratedFrames;

    [FieldOffset(268)]
    public Bool8 Reset;

    [FieldOffset(272)]
    public uint BackBufferTransferFunction;

    [FieldOffset(276)]
    public MinMaxLuminanceBuffer MinMaxLuminance;

    [FieldOffset(284)]
    public FSRApiRect2D GenerationRect;

    [FieldOffset(304)]
    public ulong FrameID;

    public const ulong Type = Fsr.APIDispatchDescTypeFrameGeneration;

    public FSRDispatchDescFrameGeneration()
    {
        this = default;
        Header.Type = Type;
    }

    [InlineArray(4)]
    public struct OutputsBuffer
    {
        private FSRApiResource element;
    }

    [InlineArray(2)]
    public struct MinMaxLuminanceBuffer
    {
        private float element;
    }
}
