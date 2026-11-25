namespace FSR.NET;

[Flags]
public enum FSRVkSurfaceTransformFlagBitsKHR : int
{
    None = 0,

    IdentityBitKhr = 1 << 0,

    Rotate90BitKhr = 1 << 1,

    Rotate180BitKhr = 1 << 2,

    Rotate270BitKhr = 1 << 3,

    HorizontalMirrorBitKhr = 1 << 4,

    HorizontalMirrorRotate90BitKhr = 1 << 5,

    HorizontalMirrorRotate180BitKhr = 1 << 6,

    HorizontalMirrorRotate270BitKhr = 1 << 7,

    InheritBitKhr = 1 << 8,

    MaxEnumKhr = 0x7FFFFFFF,
}
