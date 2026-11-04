namespace FSR.NET;

[StructLayout(LayoutKind.Explicit, Size = 152)]
public struct FSRDispatchDescDenoiserDominantLight
{
    [FieldOffset(0)]
    public FSRApiHeader Header;

    [FieldOffset(16)]
    public FSRApiDenoiserSignal Signal;

    [FieldOffset(120)]
    public FSRApiFloatCoords3D Direction;

    [FieldOffset(132)]
    public FSRApiFloatCoords3D Emission;

    [FieldOffset(144)]
    public float AngularRadius;

    public const ulong Type = Fsr.APIDispatchDescTypeDenoiserDominantLight;

    public FSRDispatchDescDenoiserDominantLight()
    {
        this = default;
        Header.Type = Type;
    }
}
