namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public struct FSRCreateBackendDX12Desc
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public nint Device;

    public const ulong Type = Fsr.APICreateContextDescTypeBackendDX12;

    public FSRCreateBackendDX12Desc()
    {
        this = default;
        Header.Type = Type;
    }
}
