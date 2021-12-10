using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Vertical.SpectreLogger;

namespace Tripleslash.Tools.Infrastructure;

internal static class Logging
{
    private static ILogger? instance;

    internal static ILogger Default => instance ?? NullLogger.Instance; 

    internal static ILogger Initialize(LogLevel logLevel)
    {
        return instance = LoggerFactory
            .Create(builder => ConfigureConsoleLogging(builder, logLevel))
            .CreateLogger("Startup");
    }

    internal static IHostBuilder ConfigureConsoleLogging(this IHostBuilder hostBuilder, LogLevel logLevel)
    {
        hostBuilder.ConfigureLogging(builder => ConfigureConsoleLogging(builder, logLevel));
        return hostBuilder;
    }

    private static void ConfigureConsoleLogging(this ILoggingBuilder loggingBuilder, LogLevel logLevel)
    {
        loggingBuilder
            .SetMinimumLevel(logLevel)
            .ClearProviders()
            .AddFilter("Microsoft", LogLevel.Warning)
            .AddSpectreConsole(options =>
            {
                options.SetMinimumLevel(logLevel);

                options.ConfigureProfile(
                    LogLevel.Trace,
                    p => p.OutputTemplate = "[grey35]{Message}{NewLine}{Exception}[/]");

                options.ConfigureProfile(
                    LogLevel.Debug,
                    p => p.OutputTemplate = "[grey46]{Message}{NewLine}{Exception}[/]");

                options.ConfigureProfile(
                    LogLevel.Information,
                    p => p.OutputTemplate = "[grey85]{Message}{NewLine}{Exception}[/]");

                options.ConfigureProfile(
                    LogLevel.Warning,
                    p => p.OutputTemplate = "[grey85]{Message}{NewLine}{Exception}[/]");

                options.ConfigureProfile(
                    LogLevel.Error,
                    p => p.OutputTemplate = "[grey85][[[red1]fail[/]]] {Message}{NewLine}{Exception}[/]");

                options.ConfigureProfile(
                    LogLevel.Critical,
                    p => p.OutputTemplate =
                        "[[[red1][/] [white on red1]crit[/]]] [red3] {Message}{NewLine}{Exception}[/]");
            });
    }
}