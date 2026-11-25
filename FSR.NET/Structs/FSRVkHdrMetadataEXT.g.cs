namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 64)]
public unsafe struct FSRVkHdrMetadataEXT
{
    [FieldOffset(0)]
    public FSRVkStructureType SType;

    [FieldOffset(8)]
    public void* Next;

    [FieldOffset(16)]
    public FSRVkXYColorEXT DisplayPrimaryRed;

    [FieldOffset(24)]
    public FSRVkXYColorEXT DisplayPrimaryGreen;

    [FieldOffset(32)]
    public FSRVkXYColorEXT DisplayPrimaryBlue;

    [FieldOffset(40)]
    public FSRVkXYColorEXT WhitePoint;

    [FieldOffset(48)]
    public float MaxLuminance;

    [FieldOffset(52)]
    public float MinLuminance;

    [FieldOffset(56)]
    public float MaxContentLightLevel;

    [FieldOffset(60)]
    public float MaxFrameAverageLightLevel;
}
