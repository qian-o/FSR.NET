namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 72)]
public unsafe struct FSRDxgiSwapChainDesc
{
    [FieldOffset(0)]
    public FSRDxgiModeDesc BufferDesc;

    [FieldOffset(28)]
    public FSRDxgiSampleDesc SampleDesc;

    [FieldOffset(36)]
    public uint BufferUsage;

    [FieldOffset(40)]
    public uint BufferCount;

    [FieldOffset(48)]
    public FSRHwnd* OutputWindow;

    [FieldOffset(56)]
    public int Windowed;

    [FieldOffset(60)]
    public FSRDxgiSwapEffect SwapEffect;

    [FieldOffset(64)]
    public uint Flags;
}
