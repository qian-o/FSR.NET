# FSR.NET

[![NuGet Version](https://img.shields.io/nuget/v/FSR.NET)](https://www.nuget.org/packages/FSR.NET)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

Low-level, zero-overhead C# bindings for the **AMD FidelityFX SDK** (**FSR Upscaling**, **Frame Generation**, **Ray Regeneration**, and **Radiance Caching**), auto-generated from the official C/C++ headers via Clang AST extraction.

## Features

- **DirectX 12 & Vulkan Support** — Complete bindings for the official `Fsr.D3D12` and `Fsr.Vulkan` C ABI loaders, descriptors, callback delegates, and header-only resource/format/matrix helpers.
- **Lightweight Core + Flexible Runtime Staging** — Ships only the official `amd_fidelityfx_loader_{dx12,vk}.dll` libraries in the NuGet package; add `<FSRRuntime>` items in your `.csproj` to automatically download, SHA-256 verify, and cache signed effect DLLs in the system temporary directory, or copy local SDK/custom effect DLLs directly.
- **Exact Native ABI & Safe Descriptor Chaining** — Explicit struct layouts verified against MSVC `sizeof`/`offsetof`, automatic `Header.Type` initialization via `new()`, and `ref` descriptor overloads that prevent struct-slicing.
- **Native AOT & Trimming Ready** — Built with `IsAotCompatible` enabled and zero external dependencies.

## Installation

```bash
dotnet add package FSR.NET
```

> **Requirements:** .NET 10+, Windows `x64` (`win-x64`), and a compatible GPU and driver. Upscaling and Frame Generation support DirectX 12 and Vulkan; Ray Regeneration and Radiance Caching require DirectX 12.

### Runtime Binaries

By default, `FSR.NET` only includes `amd_fidelityfx_loader_{dx12,vk}.dll`. Declare `<FSRRuntime>` items in your `.csproj` to stage official or local signed FSR effect DLLs (`amd_fidelityfx_upscaler*`, `amd_fidelityfx_framegeneration*`, `amd_fidelityfx_denoiser*`, `amd_fidelityfx_radiancecache*`) during `build` and `publish`:

```xml
<ItemGroup>
    <!-- Omit <Path> to download official signed DLLs (win-x64 or All) into Path.GetTempPath() -->
    <FSRRuntime Include="win-x64" />

    <!-- Or set <Path> to copy a local signedbin folder or specific DLL files without network access -->
    <!--
    <FSRRuntime Include="win-x64">
        <Path>path\to\FidelityFX-SDK\Kits\FidelityFX\signedbin</Path>
    </FSRRuntime>
    -->
</ItemGroup>
```

- **Online Download (`<Path>` omitted or empty)** — Supported `Include` values are `All`, `None`, and `win-x64`. Official binaries are verified against pinned SHA-256 digests and cached under `<SystemTemp>/FSR.NET/cache/<version>`.
- **Local Directory or Files (`<Path>` specified)** — For `win-x64`, `<Path>` can point to either a directory of `.dll` files or an individual DLL (repeat the item to pick specific effect DLLs). For `Include="All"`, `<Path>` expects `<rid>/` subdirectories (such as `win-x64/`).
- **Overrides, Packaging & Single-File Publish** — Concrete RID entries override matching RIDs from `All`, `Include="None"` disables inherited entries, `ProjectReference` consumers inherit staged DLLs cleanly, `dotnet pack` never bundles downloaded or copied runtime binaries, `<RuntimeIdentifier>` filters staged targets automatically, and effect DLLs stay side-by-side with the loaders during `PublishSingleFile` builds.

## Quick Start

```csharp
using FSR.NET;

FSRCreateBackendDX12Desc backendDesc = new()
{
    Device = d3d12Device
};

FSRCreateContextDescUpscale upscaleDesc = new()
{
    MaxRenderSize = new() { Width = renderWidth, Height = renderHeight },
    MaxUpscaleSize = new() { Width = displayWidth, Height = displayHeight },
    Flags = (uint)(FSRApiCreateContextUpscaleFlags.HighDynamicRange | FSRApiCreateContextUpscaleFlags.AutoExposure)
};
upscaleDesc.Header.Next = &backendDesc.Header;

FSRApiReturnCodes result = Fsr.D3D12.CreateContext(out FSRContext context, ref upscaleDesc.Header);
if (result is not FSRApiReturnCodes.Ok)
{
    throw new InvalidOperationException($"FSR context creation failed: {result}");
}

// Destroy context after GPU execution completes:
Fsr.D3D12.DestroyContext(ref context);
```

## Usage Notes

- Instantiate descriptors with `new()` so `Header.Type` is populated automatically (`default` zero-initializes without setting `Header.Type`).
- Chain linked descriptors via `Header.Next` and pass descriptors by `ref desc.Header` (or pointer) so native code reads the full descriptor in place.
- Query available provider versions via `FSRQueryDescGetVersions` (the NuGet package version is not an effect provider ID).
- Keep GPU resources, linked descriptors, callbacks, and `FSRStrings` allocations valid for as long as FSR uses them.
- Destroy contexts explicitly only after all queued GPU work and presentation referencing them have completed.

## Building & Regenerating Bindings

Run the **Update FSR** GitHub Actions workflow to pull the latest AMD FidelityFX SDK, validate official signed DLLs and MSVC layouts, refresh the runtime manifest, and regenerate C# bindings.

To regenerate locally from `FSR.NET.Generator/ast.json` and verify the package:

```bash
dotnet run --project FSR.NET.Generator -- .
dotnet build FSR.NET.slnx -c Release --warnaserror
python3 .github/scripts/build.py verify --root .
```

## License

Bindings and tooling are licensed under [MIT](LICENSE). Official AMD FidelityFX SDK components and signed runtime binaries remain subject to the [AMD FidelityFX SDK License](https://github.com/GPUOpen-LibrariesAndSDKs/FidelityFX-SDK/blob/main/Kits/FidelityFX/docs/license.md).
