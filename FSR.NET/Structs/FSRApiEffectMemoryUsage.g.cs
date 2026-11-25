namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 16)]
public struct FSRApiEffectMemoryUsage
{
    [FieldOffset(0)]
    public ulong TotalUsageInBytes;

    [FieldOffset(8)]
    public ulong AliasableUsageInBytes;
}
