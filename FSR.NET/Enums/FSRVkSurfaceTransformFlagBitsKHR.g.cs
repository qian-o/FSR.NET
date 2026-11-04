namespace FSR.NET;

[Flags]
public enum FSRVkSurfaceTransformFlagBitsKHR : int
{
    IdentityBitKhr = 1,

    Rotate90BitKhr = 2,

    Rotate180BitKhr = 4,

    Rotate270BitKhr = 8,

    HorizontalMirrorBitKhr = 16,

    HorizontalMirrorRotate90BitKhr = 32,

    HorizontalMirrorRotate180BitKhr = 64,

    HorizontalMirrorRotate270BitKhr = 128,

    InheritBitKhr = 256,

    FlagBitsMaxEnumKhr = 2147483647,
}
