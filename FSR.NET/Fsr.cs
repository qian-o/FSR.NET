namespace FSR.NET;

public static unsafe partial class Fsr
{
    internal const string D3D12LibraryName = "amd_fidelityfx_loader_dx12";
    internal const string VulkanLibraryName = "amd_fidelityfx_loader_vk";

    public static string RuntimeDirectory => NativeLoader.RuntimeDirectory;
}
