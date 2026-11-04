namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 4)]
public struct FSRHwnd
{
    [FieldOffset(0)]
    public int Unused;
}
