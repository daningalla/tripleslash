using Tripleslash.Tools.Infrastructure;
using Vertical.CommandLine.Configuration;
using Vertical.CommandLine.Help;

namespace Tripleslash.Tools.Features;

public class Options
{
    public string SearchTerm { get; set; } = default!;

    public string Ecosystem { get; set; } = Ecosystems.Dotnet;

    public int Skip { get; set; } = 0;

    public int Take { get; set; } = 10;

    public bool IncludePrerelease { get; set; } = false;

    public static ICommandLineConfiguration MapArguments(Func<Options,CancellationToken,Task> executeAsync) => new
            ApplicationConfiguration<Options>()
        .HelpOption("--help", InteractiveConsoleHelpWriter.Default)
        .Help.UseContent(Help)
        .PositionArgument(arg => arg.Map.ToProperty(opt => opt.SearchTerm))
        .Option("--ecosystem", arg => arg.Map.ToProperty(opt => opt.Ecosystem))
        .Option<int>("--skip", arg => arg.Map.ToProperty(opt => opt.Skip))
        .Option<int>("--take", arg => arg.Map.ToProperty(opt => opt.Take))
        .Switch("--pre|--prerelease", arg => arg.Map.ToProperty(opt => opt.IncludePrerelease))
        .OnExecuteAsync(executeAsync);

    private static IEnumerable<string> Help =
        new HelpBuilder("package search", "invokes the package search aggregator service").Build();
}