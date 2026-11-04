namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public unsafe struct FSRVkQueueInfoFFXAPI
{
    [FieldOffset(0)]
    public nint Queue;

    [FieldOffset(8)]
    public uint FamilyIndex;

    [FieldOffset(16)]
    public delegate* unmanaged[Cdecl]<uint, FSRVkSubmitInfo*, nint, FSRVkResult> SubmitFunc;
}
