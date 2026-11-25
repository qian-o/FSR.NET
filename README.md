# FSR.NET

[![NuGet Version](https://img.shields.io/nuget/vpre/FSR.NET)](https://www.nuget.org/packages/FSR.NET)

C# bindings for AMD FSR SDK 2.4.0, including Upscaling, Frame Generation, Ray Regeneration and Radiance Caching.

## Installation

```sh
dotnet add package FSR.NET
```

Requires .NET 10. Supports `win-x64`. Effect availability depends on the GPU, driver and backend. Upscaling, Frame Generation and its swapchain support DirectX 12 and Vulkan; Ray Regeneration and Radiance Caching are DirectX 12 only.

To automatically download and stage official FSR effect runtime DLLs (`amd_fidelityfx_upscaler*`, `amd_fidelityfx_framegeneration*`, `amd_fidelityfx_denoiser*`, `amd_fidelityfx_radiancecache*`) on build and publish, add to your `.csproj`:

```xml
<PropertyGroup>
    <FsrDownloadRuntime>true</FsrDownloadRuntime>
</PropertyGroup>
```

Binaries are verified with SHA-256, cached in `LocalApplicationData/FSR.NET/cache`, and staged according to `<RuntimeIdentifier>` / `<RuntimeIdentifiers>`.

## Usage

Use `Fsr.D3D12` or `Fsr.Vulkan` to call the five C ABI entry points in the corresponding official loader. The bindings preserve native descriptors, pointer chains and result codes.

- Check for `FSRApiReturnCodes.Ok` before reading outputs.
- Use `new()` for descriptor defaults; constructors set `Header.Type`. `default` zero-initializes and does not set descriptor types.
- Chain backend creation descriptors through `Header.Next`. Null-context queries without their own device field also require the backend descriptor.
- Query supported effect versions with `FSRQueryDescGetVersions`; SDK package version 2.4.0 is not an effect version ID. Leaving the override unset enables default provider selection.
- Keep GPU resources, callbacks and native addresses valid while FSR uses them. Use `FSRStrings` or pin managed data when FSR retains addresses across calls.
- Destroy contexts explicitly after their GPU work and presentation complete.
- SDK inline helpers are managed implementations, not DLL imports (`Fsr.D3D12.GetResource`, Vulkan resource descriptions, format conversion, matrix conversion and `Fsr.LoadFunctions`).

## Updating

Run **Update FSR** in GitHub Actions to update the SDK, native assets, runtime manifest and generated bindings.

To regenerate locally from `FSR.NET.Generator/ast.json` and build the package:

```sh
dotnet run --project FSR.NET.Generator -- .
dotnet build FSR.NET.slnx -c Release --warnaserror
python .github/scripts/build.py verify --root .
```

## License

Bindings and tooling: [MIT](LICENSE). Official AMD FSR SDK components retain their [AMD FSR SDK terms](https://github.com/GPUOpen-LibrariesAndSDKs/FidelityFX-SDK/blob/v2.4.0/Kits/FidelityFX/docs/license.md).
