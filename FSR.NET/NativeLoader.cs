using System.Reflection;

namespace FSR.NET;

internal static class NativeLoader
{
    private static readonly Lock @lock = new();
    private static readonly string? Rid = OperatingSystem.IsWindows() && RuntimeInformation.ProcessArchitecture is Architecture.X64 ? "win-x64" : null;

    private static bool isRegistered;

    internal static string RuntimeDirectory { get; } = GetRuntimeDirectory();

    internal static void Register()
    {
        using Lock.Scope _ = @lock.EnterScope();

        if (isRegistered)
        {
            return;
        }

        try
        {
            NativeLibrary.SetDllImportResolver(typeof(Fsr).Assembly, Resolve);
        }
        catch (InvalidOperationException)
        {
            // The application has already registered an assembly resolver.
        }

        isRegistered = true;
    }

    private static string GetRuntimeDirectory()
    {
        if (Rid is null || File.Exists(Path.Combine(AppContext.BaseDirectory, $"{Fsr.D3D12LibraryName}.dll")) || File.Exists(Path.Combine(AppContext.BaseDirectory, $"{Fsr.VulkanLibraryName}.dll")))
        {
            return AppContext.BaseDirectory;
        }

        string directory = Path.Combine(AppContext.BaseDirectory, "runtimes", Rid, "native");

        return Directory.Exists(directory) ? directory : AppContext.BaseDirectory;
    }

    private static nint Resolve(string name, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (name is not (Fsr.D3D12LibraryName or Fsr.VulkanLibraryName))
        {
            return 0;
        }

        if (Rid is null)
        {
            throw new PlatformNotSupportedException("AMD FSR SDK supports Windows x64.");
        }

        string fileName = $"{name}.dll";

        if (RuntimeDirectory == AppContext.BaseDirectory)
        {
            return Load(assembly, searchPath, Path.Combine(AppContext.BaseDirectory, fileName), fileName);
        }

        return Load(assembly, searchPath, Path.Combine(RuntimeDirectory, fileName), Path.Combine(AppContext.BaseDirectory, fileName), fileName);
    }

    private static nint Load(Assembly assembly, DllImportSearchPath? searchPath, params ReadOnlySpan<string> paths)
    {
        foreach (string path in paths)
        {
            if (Path.IsPathRooted(path))
            {
                if (File.Exists(path) && NativeLibrary.TryLoad(path, out nint handle))
                {
                    return handle;
                }
            }
            else if (NativeLibrary.TryLoad(path, assembly, searchPath, out nint handle))
            {
                return handle;
            }
        }

        return 0;
    }
}
