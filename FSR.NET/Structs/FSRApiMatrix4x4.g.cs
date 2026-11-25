namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 64)]
public struct FSRApiMatrix4x4
{
    [FieldOffset(0)]
    public RowsBuffer Rows;

    [InlineArray(4)]
    public struct RowsBuffer
    {
        private FSRApiFloat4 element;
    }
}
