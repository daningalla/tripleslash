namespace Tripleslash.PackageServices.NuGet;

/// <summary>
/// Represents a resource that executes search queries.
/// </summary>
public interface ISearchResource
{
    /// <summary>
    /// Executes the search.
    /// </summary>
    /// <param name="searchTerm">Search term</param>
    /// <param name="includePreRelease">Whether to include pre-release packages</param>
    /// <param name="skip">Number of results to skip</param>
    /// <param name="take">Maximum number of results to return</param>
    /// <param name="cancellationToken">Token that can be observed for cancel requests</param>
    /// <returns>A task that completes with the discovered package metadata</returns>
    Task<IEnumerable<PackageMetadata>> GetResultsAsync(
        string searchTerm
        , bool includePreRelease
        , int skip
        , int take
        , CancellationToken cancellationToken);
}