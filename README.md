# FSR.NET

[![NuGet Version](https://img.shields.io/nuget/vpre/FSR.NET)](https://www.nuget.org/packages/FSR.NET)

C# bindings for AMD FSR SDK 2.4.0, including Upscaling, Frame Generation, Ray Regeneration and Radiance Caching.

## Installation

```sh
dotnet add package FSR.NET
```

Requires .NET 10. Official native binaries are included for Windows x64. Effect availability depends on the GPU, driver and backend. Upscaling, Frame Generation and its swapchain support DirectX 12 and Vulkan; Ray Regeneration and Radiance Caching are DirectX 12 only.

## Usage

Use `Fsr.D3D12` or `Fsr.Vulkan` to call the five C ABI entry points in the corresponding official loader. The bindings preserve native descriptors, pointer chains and result codes.

- Check for `FSRApiReturnCodes.Ok` before reading outputs.
- Use `new()` for descriptor defaults; constructors set `Header.Type`. `default` produces zero-initialized data and does not initialize descriptor types.
- Chain backend creation descriptors through `Header.Next`. Null-context queries without their own device field also require the backend descriptor.
- Query supported effect versions with `FSRQueryDescGetVersions`; SDK package version 2.4.0 is not an effect version ID. Leaving the override unset enables the SDK's default provider selection.
- Keep GPU resources, callbacks and native addresses valid while FSR uses them. Pin managed data when FSR retains its address across calls.
- Destroy contexts explicitly after their GPU work and presentation complete. Copying a context or descriptor does not transfer ownership.
- SDK inline helpers are managed implementations, not DLL imports. `Fsr.D3D12.GetResource` accepts the resource description obtained by the caller from `ID3D12Resource::GetDesc`; Vulkan resource descriptions, format conversion, matrix conversion and function-table loading are also provided.

## Updating

Run **Update FSR** in GitHub Actions to update the SDK, native binaries and generated bindings. It pins one official SDK commit, checks the public headers against the Clang AST, validates Windows x64 layouts, and verifies the managed bindings and native DLLs in the NuGet package before committing the update.

To regenerate bindings from the checked-in AST:

```sh
dotnet run --project FSR.NET.Generator -- .
dotnet build FSR.NET.slnx -c Release --warnaserror
python .github/scripts/verify_fsr.py --root . --package FSR.NET/bin/Release/FSR.NET.2.4.0.nupkg
```

## License

Bindings and build tooling: [MIT](LICENSE). Bundled AMD components retain their [AMD FSR SDK terms](https://github.com/GPUOpen-LibrariesAndSDKs/FidelityFX-SDK/blob/v2.4.0/Kits/FidelityFX/docs/license.md).
