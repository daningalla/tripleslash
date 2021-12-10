using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Microsoft.Extensions.Logging;
using Tripleslash.Infrastructure;
using Tripleslash.IO;

namespace Tripleslash.Tools.Infrastructure;

internal static class ConfigurationExtensions
{
    internal static IConfigurationBuilder AddJsonFiles(
        this IConfigurationBuilder builder,
        IEnumerable<string> paths)
    {
        foreach (var path in paths.Select(ReplaceTokens))
        {
            if (GlobPattern.TryParse(path, out var glob))
            {
                builder.AddJsonFilesForPattern(glob);
            }
            else
            {
                Logging.Default.LogDebug("Adding configuring file {Path}", path);
                builder.AddJsonFile(path);
            }
        }
        
        return builder;
    }

    private static string ReplaceTokens(string arg)
    {
        return TokenHelpers.ReplaceEnvironmentVariables(TokenHelpers.ReplaceSpecialFolderPaths(arg));
    }

    private static void AddJsonFilesForPattern(
        this IConfigurationBuilder builder,
        in GlobPattern globPattern)
    {
        var matcher = new Matcher();

        matcher.AddInclude(globPattern.Pattern);

        var fullPath = Path.GetFullPath(globPattern.BasePath);
        var results = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(fullPath)));

        foreach (var file in results.Files.Select(result => Path.Combine(fullPath, result.Path)))
        { 
            Logging.Default.LogDebug("Adding configuring file {Path}", file);
            builder.AddJsonFile(file);
        }
    }
}