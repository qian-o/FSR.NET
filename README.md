# FSR.NET

[![NuGet Version](https://img.shields.io/nuget/v/FSR.NET)](https://www.nuget.org/packages/FSR.NET)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

Low-level, zero-overhead C# bindings for the **AMD FidelityFX SDK** (**FSR Upscaling**, **Frame Generation**, **Ray Regeneration**, and **Radiance Caching**), auto-generated from the official C/C++ headers.

## Features

- **DirectX 12 & Vulkan** — Covers `Fsr.D3D12` and `Fsr.Vulkan` loaders, descriptors, callbacks, and resource/matrix helpers on `win-x64`.
- **Safe ABI & Descriptor Chaining** — Verified MSVC struct layouts, automatic `Header.Type` initialization via `new()`, and `ref` overloads that prevent struct-slicing.
- **On-Demand Runtime Staging** — Ships only `amd_fidelityfx_loader_{dx12,vk}.dll`; stage official or local signed effect DLLs via `<FSRRuntime>`.
- **Native AOT Ready** — Built with `IsAotCompatible` and zero external dependencies.

## Installation

```bash
dotnet add package FSR.NET
```

### Runtime Binaries

Add `<FSRRuntime>` to your `.csproj` to stage signed FSR effect DLLs (`amd_fidelityfx_*`) on build and publish:

```xml
<ItemGroup>
    <!-- Download official binaries (cached in system temp): All | win-x64 -->
    <FSRRuntime Include="win-x64" />

    <!-- Or copy from a local directory or file (use None to clear inherited entries) -->
    <!--
    <FSRRuntime Include="win-x64">
        <Path>path\to\FidelityFX-SDK\Kits\FidelityFX\signedbin</Path>
    </FSRRuntime>
    -->
</ItemGroup>
```

## Quick Start

```csharp
using FSR.NET;

FSRCreateBackendDX12Desc backendDesc = new() { Device = d3d12Device };
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

Fsr.D3D12.DestroyContext(ref context);
```

## Building

```bash
dotnet run --project FSR.NET.Generator -- .
dotnet build FSR.NET.slnx -c Release
```

## License

[MIT](LICENSE). Official AMD FidelityFX runtime binaries remain subject to the [AMD FidelityFX SDK License](https://github.com/GPUOpen-LibrariesAndSDKs/FidelityFX-SDK/blob/main/Kits/FidelityFX/docs/license.md).
