using System.Security;

namespace FSR.NET.Generator;

internal class ManifestEmitter(AstModel model, Dictionary<string, string> files)
{
    internal void WriteManifest()
    {
        AstSource source = model.Source;
        string repository = source.Repository;
        const string GitHubPrefix = "https://github.com/";
        if (!repository.StartsWith(GitHubPrefix, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException($"Unsupported source repository URL: {repository}.");
        }

        string repoPath = repository[GitHubPrefix.Length..].TrimEnd('/');
        string defaultBaseUrl = $"https://raw.githubusercontent.com/{repoPath}/{source.Commit}";
        StringBuilder text = new();
        AppendLine(text, "<Project>");
        AppendLine(text);
        AppendLine(text, "    <PropertyGroup>");
        AppendLine(text, $"        <_FsrSdkRelease>{Escape(source.Release)}</_FsrSdkRelease>");
        AppendLine(text, $"        <_FsrSdkCommit>{Escape(source.Commit)}</_FsrSdkCommit>");
        AppendLine(text, $"        <_FsrDefaultBaseUrl>{Escape(defaultBaseUrl)}</_FsrDefaultBaseUrl>");
        AppendLine(text, "    </PropertyGroup>");
        AppendLine(text);
        AppendLine(text, "    <ItemDefinitionGroup>");
        AppendLine(text, "        <FSRRuntime>");
        AppendLine(text, "            <Path />");
        AppendLine(text, "        </FSRRuntime>");
        AppendLine(text, "    </ItemDefinitionGroup>");
        AppendLine(text);
        AppendLine(text, "    <ItemGroup>");

        foreach (AstRuntimeAsset asset in source.Runtimes)
        {
            string include = $"{asset.Rid}/{asset.AssetName}";
            AppendLine(text, $"        <_FsrManifestEntry Include=\"{Escape(include)}\" Rid=\"{Escape(asset.Rid)}\" AssetName=\"{Escape(asset.AssetName)}\" RelativeUrl=\"{Escape(asset.RelativeUrl)}\" Sha256=\"{Escape(asset.Sha256)}\" />");
        }

        AppendLine(text, "    </ItemGroup>");
        AppendLine(text);
        AppendLine(text, "</Project>");

        files.Add("buildTransitive/FSR.NET.props", text.ToString());
    }

    private static void AppendLine(StringBuilder text, string value = "")
    {
        text.Append(value);
        text.Append('\n');
    }

    private static string Escape(string value)
    {
        return SecurityElement.Escape(value)!;
    }
}
