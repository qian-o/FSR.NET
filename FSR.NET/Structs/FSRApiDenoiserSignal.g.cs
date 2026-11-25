namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 104)]
public struct FSRApiDenoiserSignal
{
    [FieldOffset(0)]
    public FSRApiResource Input;

    [FieldOffset(48)]
    public FSRApiResource Output;

    [FieldOffset(96)]
    public uint CheckerboardOrigin;
}
