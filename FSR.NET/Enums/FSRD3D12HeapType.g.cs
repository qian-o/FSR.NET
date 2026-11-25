namespace FSR.NET;

public enum FSRD3D12HeapType : int
{
    Default = 1,

    Upload = 2,

    Readback = 3,

    Custom = 4,

    GpuUpload = 5,
}
