// This file is part of the FidelityFX SDK.
//
// Copyright (C) 2026 Advanced Micro Devices, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and /or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

// Managed adaptations of the public inline helpers in FSR SDK v2.4.0.
// Source: GPUOpen-LibrariesAndSDKs/FidelityFX-SDK, commit 7539ae8b6568ad29ed78a753404351daf3b5cca7.
// HelperValidation checks upstream implementation hashes before regeneration.

namespace FSR.NET;

public static unsafe partial class Fsr
{
    public static partial class D3D12
    {
        public static FSRApiSurfaceFormat GetSurfaceFormat(FSRDxgiFormat format)
        {
            return format switch
            {
                FSRDxgiFormat.R32G32B32A32Typeless => FSRApiSurfaceFormat.R32G32B32A32Typeless,
                FSRDxgiFormat.R32G32B32A32Float => FSRApiSurfaceFormat.R32G32B32A32Float,
                FSRDxgiFormat.R32G32B32A32Uint => FSRApiSurfaceFormat.R32G32B32A32Uint,
                FSRDxgiFormat.R16G16B16A16Typeless => FSRApiSurfaceFormat.R16G16B16A16Typeless,
                FSRDxgiFormat.R16G16B16A16Float => FSRApiSurfaceFormat.R16G16B16A16Float,
                FSRDxgiFormat.R32G32Typeless => FSRApiSurfaceFormat.R32G32Typeless,
                FSRDxgiFormat.R32G32Float => FSRApiSurfaceFormat.R32G32Float,
                FSRDxgiFormat.R32G8X24Typeless => FSRApiSurfaceFormat.R32Float,
                FSRDxgiFormat.D32FloatS8X24Uint => FSRApiSurfaceFormat.R32Float,
                FSRDxgiFormat.R32FloatX8X24Typeless => FSRApiSurfaceFormat.R32Float,
                FSRDxgiFormat.R24G8Typeless => FSRApiSurfaceFormat.R32Uint,
                FSRDxgiFormat.D24UnormS8Uint => FSRApiSurfaceFormat.R32Uint,
                FSRDxgiFormat.R24UnormX8Typeless => FSRApiSurfaceFormat.R32Uint,
                FSRDxgiFormat.X32TypelessG8X24Uint => FSRApiSurfaceFormat.R8Uint,
                FSRDxgiFormat.X24TypelessG8Uint => FSRApiSurfaceFormat.R8Uint,
                FSRDxgiFormat.R10G10B10A2Typeless => FSRApiSurfaceFormat.R10G10B10A2Typeless,
                FSRDxgiFormat.R10G10B10A2Unorm => FSRApiSurfaceFormat.R10G10B10A2Unorm,
                FSRDxgiFormat.R11G11B10Float => FSRApiSurfaceFormat.R11G11B10Float,
                FSRDxgiFormat.R8G8B8A8Typeless => FSRApiSurfaceFormat.R8G8B8A8Typeless,
                FSRDxgiFormat.R8G8B8A8Unorm => FSRApiSurfaceFormat.R8G8B8A8Unorm,
                FSRDxgiFormat.R8G8B8A8UnormSrgb => FSRApiSurfaceFormat.R8G8B8A8Srgb,
                FSRDxgiFormat.R8G8B8A8Snorm => FSRApiSurfaceFormat.R8G8B8A8Snorm,
                FSRDxgiFormat.B8G8R8A8Typeless => FSRApiSurfaceFormat.B8G8R8A8Typeless,
                FSRDxgiFormat.B8G8R8A8Unorm => FSRApiSurfaceFormat.B8G8R8A8Unorm,
                FSRDxgiFormat.B8G8R8A8UnormSrgb => FSRApiSurfaceFormat.B8G8R8A8Srgb,
                FSRDxgiFormat.R16G16Typeless => FSRApiSurfaceFormat.R16G16Typeless,
                FSRDxgiFormat.R16G16Float => FSRApiSurfaceFormat.R16G16Float,
                FSRDxgiFormat.R16G16Uint => FSRApiSurfaceFormat.R16G16Uint,
                FSRDxgiFormat.R32Uint => FSRApiSurfaceFormat.R32Uint,
                FSRDxgiFormat.R32Typeless => FSRApiSurfaceFormat.R32Typeless,
                FSRDxgiFormat.D32Float => FSRApiSurfaceFormat.R32Float,
                FSRDxgiFormat.R32Float => FSRApiSurfaceFormat.R32Float,
                FSRDxgiFormat.R8G8Uint => FSRApiSurfaceFormat.R8G8Uint,
                FSRDxgiFormat.R8G8Typeless => FSRApiSurfaceFormat.R8G8Typeless,
                FSRDxgiFormat.R8G8Unorm => FSRApiSurfaceFormat.R8G8Unorm,
                FSRDxgiFormat.R16Typeless => FSRApiSurfaceFormat.R16Typeless,
                FSRDxgiFormat.R16Float => FSRApiSurfaceFormat.R16Float,
                FSRDxgiFormat.R16Uint => FSRApiSurfaceFormat.R16Uint,
                FSRDxgiFormat.D16Unorm => FSRApiSurfaceFormat.R16Unorm,
                FSRDxgiFormat.R16Unorm => FSRApiSurfaceFormat.R16Unorm,
                FSRDxgiFormat.R16Snorm => FSRApiSurfaceFormat.R16Snorm,
                FSRDxgiFormat.R8Typeless => FSRApiSurfaceFormat.R8Typeless,
                FSRDxgiFormat.R8Unorm => FSRApiSurfaceFormat.R8Unorm,
                FSRDxgiFormat.A8Unorm => FSRApiSurfaceFormat.R8Unorm,
                FSRDxgiFormat.R8Snorm => FSRApiSurfaceFormat.R8Snorm,
                FSRDxgiFormat.R8Uint => FSRApiSurfaceFormat.R8Uint,
                FSRDxgiFormat.R9G9B9E5SharedExp => FSRApiSurfaceFormat.R9G9B9E5SharedExp,
                FSRDxgiFormat.Unknown => FSRApiSurfaceFormat.Unknown,
                _ => FSRApiSurfaceFormat.Unknown
            };
        }

        // The caller supplies ID3D12Resource::GetDesc(). This keeps COM dispatch with its owner.
        public static FSRApiResource GetResource(nint resource, FSRD3D12ResourceDesc description, FSRApiResourceState state = FSRApiResourceState.ComputeRead, FSRApiResourceUsage additionalUsages = FSRApiResourceUsage.ReadOnly)
        {
            FSRApiResource result = new()
            {
                Resource = (void*)resource,
                State = (uint)state
            };

            if (resource is 0 || description.Dimension is FSRD3D12ResourceDimension.Unknown)
            {
                return result;
            }

            if (description.Dimension is FSRD3D12ResourceDimension.Buffer)
            {
                result.Description.Usage = (uint)FSRApiResourceUsage.Uav;
                result.Description.Size = (uint)description.Width;
                result.Description.Stride = description.Height;
                result.Description.Type = (uint)FSRApiResourceType.Buffer;
            }
            else
            {
                result.Description.Usage = description.Format switch
                {
                    FSRDxgiFormat.D16Unorm or FSRDxgiFormat.D32Float => (uint)FSRApiResourceUsage.DepthTarget,
                    FSRDxgiFormat.D24UnormS8Uint or FSRDxgiFormat.D32FloatS8X24Uint => (uint)(FSRApiResourceUsage.DepthTarget | FSRApiResourceUsage.StencilTarget),
                    _ => (uint)FSRApiResourceUsage.ReadOnly
                };

                if ((description.Flags & FSRD3D12ResourceFlags.AllowUnorderedAccess) is not 0)
                {
                    result.Description.Usage |= (uint)FSRApiResourceUsage.Uav;
                }

                result.Description.Width = (uint)description.Width;
                result.Description.Height = description.Height;
                result.Description.Depth = description.DepthOrArraySize;
                result.Description.MipCount = description.MipLevels;
                result.Description.Type = description.Dimension switch
                {
                    FSRD3D12ResourceDimension.Texture1D => (uint)FSRApiResourceType.Texture1D,
                    FSRD3D12ResourceDimension.Texture2D when description.DepthOrArraySize is 6 => (uint)FSRApiResourceType.TextureCube,
                    FSRD3D12ResourceDimension.Texture2D => (uint)FSRApiResourceType.Texture2D,
                    FSRD3D12ResourceDimension.Texture3D => (uint)FSRApiResourceType.Texture3D,
                    _ => 0
                };
            }

            result.Description.Format = (uint)GetSurfaceFormat(description.Format);
            result.Description.Usage |= (uint)additionalUsages;

            return result;
        }
    }
}
