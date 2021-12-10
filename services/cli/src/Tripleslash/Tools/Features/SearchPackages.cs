using Microsoft.Extensions.Logging;
using Tripleslash.PackageServices;
using Vertical.CommandLine;
using Vertical.CommandLine.Configuration;
using Vertical.ConsoleApplications.Pipeline;
using Vertical.ConsoleApplications.Routing;

namespace Tripleslash.Tools.Features;

[Command("package search")]
public class SearchPackages : ICommandHandler
{
    private readonly ILogger<SearchPackages> _logger;
    private readonly IPackageSearchAggregator _searchAggregator;
    private readonly ICommandLineConfiguration _argumentConfiguration;
    
    public SearchPackages(ILogger<SearchPackages> logger,
        IPackageSearchAggregator searchAggregator)
    {
        _logger = logger;
        _searchAggregator = searchAggregator;
        _argumentConfiguration = Options.MapArguments(HandleWithArgumentsAsync);
    }

    private async Task HandleWithArgumentsAsync(Options options, CancellationToken cancellationToken)
    {
        
    }

    /// <inheritdoc />
    public Task HandleAsync(RequestContext context, CancellationToken cancellationToken) =>
        CommandLineApplication.RunAsync(_argumentConfiguration, context.Arguments, cancellationToken);
}