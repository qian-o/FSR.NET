namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 40)]
public struct FSRCreateBackendVKDesc
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRVkDeviceContext DeviceContext;

    public const ulong Type = Fsr.APICreateContextDescTypeBackendVK;

    public FSRCreateBackendVKDesc()
    {
        this = default;
        Header.Type = Type;
    }
}
