namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 32)]
public struct FSRQueryDescUpscaleGetResourceRequirements
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public ulong RequiredResources;

    [FieldOffset(24)]
    public ulong OptionalResources;

    public const ulong Type = Fsr.APIQueryDescTypeUpscaleGetResourceRequirements;

    public FSRQueryDescUpscaleGetResourceRequirements()
    {
        this = default;
        Header.Type = Type;
    }
}
