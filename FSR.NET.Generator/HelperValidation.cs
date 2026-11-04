namespace FSR.NET.Generator;

internal static class HelperValidation
{
    // The handwritten helpers in API/*.cs follow these public header bodies.
    // Fail on SDK changes so that helper behavior is reviewed alongside regenerated ABI data.
    private static readonly Dictionary<string, string> Implementations = new(StringComparer.Ordinal)
    {
        ["ffxApiGetBufferResourceDescriptionVK"] = "7f7217d5dc2ae00f6242ab095f065244583ef419db265bdb3905b70e6d624cd4",
        ["ffxApiGetImageResourceDescriptionVK"] = "6f3985dab93142c336930616f948e3776c8134d0f431a14847925e3357d5056e",
        ["ffxApiGetResourceDX12"] = "bc42075941fb120066a912cdb6f31451c6263604d6e078f81450c34cef7ad7e8",
        ["ffxApiGetResourceVK"] = "d04081f49c81f1d5e8ca9714c26e1b3e8cef39d32f09f42902c97a8132133d34",
        ["ffxApiGetSurfaceFormatDX12"] = "b9d72eecad01cdcf33d068d3a1503f223c1edb8d23b2aba0454920810db2d2a7",
        ["ffxApiGetSurfaceFormatToGamma"] = "4113aaee1200206d950312b0b4b3738b43a861159e007e4500381a86e3a232c4",
        ["ffxApiGetSurfaceFormatVK"] = "d2412d93037acd730679052e7fd0729709805a0cedb7eec874273fa84cbae7c9",
        ["ffxApiIsDepthFormat"] = "ddd28b93364810f7be907c3b19f533d320c6bff6b4763912b1aab9f9aa2ab69e",
        ["ffxApiIsStencilFormat"] = "883c6bab868068c89fee33f498f2acb2f6261bdd10d969b2a6e4cfe6dcffab63",
        ["ffxConvertToMatrix4x4"] = "31a26d07bbd06a72f41f8730e590ec5557f96a99f419419da433b2cdf9365c06",
        ["ffxLoadFunctions"] = "9cafb3926e9dcf5943c78ff94a5847081ddd7ba7afd0dac2e9e2ad41225aae04",
    };

    internal static void Verify(JsonElement ast)
    {
        foreach (JsonProperty platform in ast.GetProperty("platforms").EnumerateObject())
        {
            HashSet<string> seen = [];
            foreach (JsonElement helper in platform.Value.GetProperty("helpers").EnumerateArray())
            {
                string name = helper.GetProperty("name").GetString()!;
                string hash = helper.GetProperty("implementationSha256").GetString()!;
                if (!Implementations.TryGetValue(name, out string? expected) || hash != expected)
                {
                    throw new InvalidOperationException($"Review the managed implementation of changed SDK helper {name} before updating its baseline.");
                }

                seen.Add(name);
            }

            if (!seen.SetEquals(Implementations.Keys))
            {
                throw new InvalidOperationException("The SDK helper set changed; review the managed helper coverage.");
            }
        }
    }
}
