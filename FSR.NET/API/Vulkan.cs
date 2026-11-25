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
    public static partial class Vulkan
    {
        public static FSRApiSurfaceFormat GetSurfaceFormat(FSRVkFormat format)
        {
            return format switch
            {
                FSRVkFormat.R32G32B32A32Sfloat => FSRApiSurfaceFormat.R32G32B32A32Float,
                FSRVkFormat.R32G32B32Sfloat => FSRApiSurfaceFormat.R32G32B32Float,
                FSRVkFormat.R32G32B32A32Uint => FSRApiSurfaceFormat.R32G32B32A32Uint,
                FSRVkFormat.R16G16B16A16Sfloat => FSRApiSurfaceFormat.R16G16B16A16Float,
                FSRVkFormat.R32G32Sfloat => FSRApiSurfaceFormat.R32G32Float,
                FSRVkFormat.R32Uint => FSRApiSurfaceFormat.R32Uint,
                FSRVkFormat.D24UnormS8Uint => FSRApiSurfaceFormat.R32Uint,
                FSRVkFormat.X8D24UnormPack32 => FSRApiSurfaceFormat.R32Uint,
                FSRVkFormat.R8G8B8A8Unorm => FSRApiSurfaceFormat.R8G8B8A8Unorm,
                FSRVkFormat.R8G8B8A8Snorm => FSRApiSurfaceFormat.R8G8B8A8Snorm,
                FSRVkFormat.R8G8B8A8Srgb => FSRApiSurfaceFormat.R8G8B8A8Srgb,
                FSRVkFormat.B8G8R8A8Unorm => FSRApiSurfaceFormat.B8G8R8A8Unorm,
                FSRVkFormat.B8G8R8A8Srgb => FSRApiSurfaceFormat.B8G8R8A8Srgb,
                FSRVkFormat.B10G11R11UfloatPack32 => FSRApiSurfaceFormat.R11G11B10Float,
                FSRVkFormat.A2B10G10R10UnormPack32 => FSRApiSurfaceFormat.R10G10B10A2Unorm,
                FSRVkFormat.R16G16Sfloat => FSRApiSurfaceFormat.R16G16Float,
                FSRVkFormat.R16G16Uint => FSRApiSurfaceFormat.R16G16Uint,
                FSRVkFormat.R16G16Sint => FSRApiSurfaceFormat.R16G16Sint,
                FSRVkFormat.R16Sfloat => FSRApiSurfaceFormat.R16Float,
                FSRVkFormat.R16Uint => FSRApiSurfaceFormat.R16Uint,
                FSRVkFormat.R16Unorm => FSRApiSurfaceFormat.R16Unorm,
                FSRVkFormat.D16Unorm => FSRApiSurfaceFormat.R16Unorm,
                FSRVkFormat.D16UnormS8Uint => FSRApiSurfaceFormat.R16Unorm,
                FSRVkFormat.R16Snorm => FSRApiSurfaceFormat.R16Snorm,
                FSRVkFormat.R8Unorm => FSRApiSurfaceFormat.R8Unorm,
                FSRVkFormat.R8Snorm => FSRApiSurfaceFormat.R8Snorm,
                FSRVkFormat.R8Uint => FSRApiSurfaceFormat.R8Uint,
                FSRVkFormat.S8Uint => FSRApiSurfaceFormat.R8Uint,
                FSRVkFormat.R8G8Unorm => FSRApiSurfaceFormat.R8G8Unorm,
                FSRVkFormat.R8G8Uint => FSRApiSurfaceFormat.R8G8Uint,
                FSRVkFormat.R32Sfloat => FSRApiSurfaceFormat.R32Float,
                FSRVkFormat.D32Sfloat => FSRApiSurfaceFormat.R32Float,
                FSRVkFormat.D32SfloatS8Uint => FSRApiSurfaceFormat.R32Float,
                FSRVkFormat.E5B9G9R9UfloatPack32 => FSRApiSurfaceFormat.R9G9B9E5SharedExp,
                FSRVkFormat.Undefined => FSRApiSurfaceFormat.Unknown,
                _ => FSRApiSurfaceFormat.Unknown
            };
        }

        public static FSRApiSurfaceFormat GetSurfaceFormatToGamma(FSRApiSurfaceFormat format)
        {
            return format switch
            {
                FSRApiSurfaceFormat.R8G8B8A8Unorm => FSRApiSurfaceFormat.R8G8B8A8Srgb,
                FSRApiSurfaceFormat.B8G8R8A8Unorm => FSRApiSurfaceFormat.B8G8R8A8Srgb,
                _ => format
            };
        }

        public static bool IsDepthFormat(FSRVkFormat format)
        {
            return format is FSRVkFormat.D16Unorm or FSRVkFormat.X8D24UnormPack32 or FSRVkFormat.D32Sfloat or FSRVkFormat.D16UnormS8Uint or FSRVkFormat.D24UnormS8Uint or FSRVkFormat.D32SfloatS8Uint;
        }

        public static bool IsStencilFormat(FSRVkFormat format)
        {
            return format is FSRVkFormat.S8Uint or FSRVkFormat.D16UnormS8Uint or FSRVkFormat.D24UnormS8Uint or FSRVkFormat.D32SfloatS8Uint;
        }

        public static FSRApiResourceDescription GetBufferResourceDescription(nint buffer, FSRVkBufferCreateInfo createInfo, FSRApiResourceUsage additionalUsages = FSRApiResourceUsage.ReadOnly)
        {
            if (buffer is 0)
            {
                return default;
            }

            FSRApiResourceDescription description = new()
            {
                Usage = (uint)additionalUsages,
                Size = (uint)createInfo.Size,
                Format = (uint)FSRApiSurfaceFormat.Unknown,
                Type = (uint)FSRApiResourceType.Buffer
            };

            if ((createInfo.Usage & (uint)FSRVkBufferUsageFlagBits.StorageBufferBit) is not 0)
            {
                description.Usage |= (uint)FSRApiResourceUsage.Uav;
            }

            if ((createInfo.Usage & (uint)FSRVkBufferUsageFlagBits.IndirectBufferBit) is not 0)
            {
                description.Usage |= (uint)FSRApiResourceUsage.Indirect;
            }

            return description;
        }

        public static FSRApiResourceDescription GetImageResourceDescription(nint image, FSRVkImageCreateInfo createInfo, FSRApiResourceUsage additionalUsages = FSRApiResourceUsage.ReadOnly)
        {
            if (image is 0)
            {
                return default;
            }

            FSRApiResourceDescription description = new()
            {
                Usage = (uint)additionalUsages,
                Width = createInfo.Extent.Width,
                Height = createInfo.Extent.Height,
                MipCount = createInfo.MipLevels,
                Format = (uint)GetSurfaceFormat(createInfo.Format)
            };

            if (IsDepthFormat(createInfo.Format))
            {
                description.Usage |= (uint)FSRApiResourceUsage.DepthTarget;
            }

            if (IsStencilFormat(createInfo.Format))
            {
                description.Usage |= (uint)FSRApiResourceUsage.StencilTarget;
            }

            if ((createInfo.Usage & (uint)FSRVkImageUsageFlagBits.StorageBit) is not 0)
            {
                description.Usage |= (uint)FSRApiResourceUsage.Uav;
            }

            if ((createInfo.Flags & (uint)FSRVkImageCreateFlagBits.MutableFormatBit) is not 0)
            {
                description.Format = (uint)GetSurfaceFormatToGamma((FSRApiSurfaceFormat)description.Format);
            }

            switch (createInfo.ImageType)
            {
                case FSRVkImageType._1D:
                    description.Type = (uint)FSRApiResourceType.Texture1D;
                    break;
                case FSRVkImageType._2D:
                    description.Depth = createInfo.ArrayLayers;
                    description.Type = (uint)((additionalUsages & FSRApiResourceUsage.ArrayView) is 0 && (createInfo.Flags & (uint)FSRVkImageCreateFlagBits.CubeCompatibleBit) is not 0 ? FSRApiResourceType.TextureCube : FSRApiResourceType.Texture2D);
                    break;
                case FSRVkImageType._3D:
                    description.Depth = createInfo.Extent.Depth;
                    description.Type = (uint)FSRApiResourceType.Texture3D;
                    break;
            }

            return description;
        }

        public static FSRApiResource GetResource(nint resource, FSRApiResourceDescription description, FSRApiResourceState state)
        {
            return new()
            {
                Resource = (void*)resource,
                Description = description,
                State = (uint)state
            };
        }
    }
}
