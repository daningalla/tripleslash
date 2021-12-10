using Microsoft.Extensions.Logging;

namespace Tripleslash.PackageServices.NuGet;

public class SearchResource : ISearchResource
{
    private readonly HttpClient _httpClient;
    private readonly string _baseSearchUrl;
    private readonly ILogger<SearchResource>? _logger;

    internal SearchResource(HttpClient httpClient,
        string baseSearchUrl,
        ILogger<SearchResource>? logger = null)
    {
        _httpClient = httpClient;
        _baseSearchUrl = baseSearchUrl;
        _logger = logger;
    }
    
    /// <inheritdoc />
    public Task<IEnumerable<PackageMetadata>> GetResultsAsync(
        string searchTerm, 
        bool includePreRelease, 
        int skip, 
        int take, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}