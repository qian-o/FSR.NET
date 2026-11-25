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
    public static FSRApiMatrix4x4 ConvertToMatrix4x4(float* source, FSRApiMatrixLayout layout, FSRApiMatrixVectorConvention convention)
    {
        FSRApiMatrix4x4 result;
        bool transpose = (layout is FSRApiMatrixLayout.RowMajor) != (convention is FSRApiMatrixVectorConvention.RowVectors);

        if (transpose)
        {
            result = default;

            for (int row = 0; row < 4; row++)
            {
                result.Rows[row] = new()
                {
                    X = source[row],
                    Y = source[4 + row],
                    Z = source[8 + row],
                    W = source[12 + row]
                };
            }
        }
        else
        {
            result = Unsafe.ReadUnaligned<FSRApiMatrix4x4>(source);
        }

        return result;
    }

    public static FSRFunctions LoadFunctions(nint module)
    {
        if (module is 0)
        {
            return default;
        }

        NativeLibrary.TryGetExport(module, "ffxCreateContext", out nint create);
        NativeLibrary.TryGetExport(module, "ffxDestroyContext", out nint destroy);
        NativeLibrary.TryGetExport(module, "ffxConfigure", out nint configure);
        NativeLibrary.TryGetExport(module, "ffxQuery", out nint query);
        NativeLibrary.TryGetExport(module, "ffxDispatch", out nint dispatch);

        return new()
        {
            CreateContext = (delegate* unmanaged[Cdecl]<FSRContext*, FSRApiHeader*, FSRAllocationCallbacks*, FSRApiReturnCodes>)create,
            DestroyContext = (delegate* unmanaged[Cdecl]<FSRContext*, FSRAllocationCallbacks*, FSRApiReturnCodes>)destroy,
            Configure = (delegate* unmanaged[Cdecl]<FSRContext*, FSRApiHeader*, FSRApiReturnCodes>)configure,
            Query = (delegate* unmanaged[Cdecl]<FSRContext*, FSRApiHeader*, FSRApiReturnCodes>)query,
            Dispatch = (delegate* unmanaged[Cdecl]<FSRContext*, FSRApiHeader*, FSRApiReturnCodes>)dispatch
        };
    }
}
