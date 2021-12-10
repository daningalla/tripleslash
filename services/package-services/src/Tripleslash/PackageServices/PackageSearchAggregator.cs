// Copyright (c) 2021 Tripleslash contributors
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using Dawn;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Tripleslash.PackageServices;

internal class PackageSearchAggregator : IPackageSearchAggregator
{
    private readonly IEnumerable<IPackageSearchService> _searchServices;
    private readonly ILogger<PackageSearchAggregator>? _logger;
    private readonly PackageSearchOptions _options;

    public PackageSearchAggregator(
        IEnumerable<IPackageSearchService> searchServices,
        IOptions<PackageSearchOptions> optionsProvider, 
        ILogger<PackageSearchAggregator>? logger = null)
    {
        _searchServices = searchServices;
        _logger = logger;
        _options = optionsProvider.Value;
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<PackageMetadata>> SearchAsync(
        string ecosystem,
        string searchTerm, 
        bool includePreRelease, 
        int skip, 
        int take, 
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Enumerable.Empty<PackageMetadata>();
        }

        Guard.Argument(skip, nameof(skip)).GreaterThan(-1);
        Guard.Argument(take, nameof(take)).GreaterThan(-1);

        _logger?.LogDebug("Start package search (query='{Ecosystem}:{SearchTerm}', skip={Skip}, take={Take})",
            ecosystem,
            searchTerm,
            skip,
            take);
        
        take = Math.Min(take, _options.MaxTakeCount);

        var services = _searchServices
            .Where(svc => svc.SupportsEcosystem(ecosystem))
            .ToArray();

        if (services.Length == 0)
        {   
            _logger?.LogWarning("No package search services registered for ecosystem '{Ecosystem}'",
                ecosystem);
            return Enumerable.Empty<PackageMetadata>();
        }

        var searchTasks = services.Select(service => service.SearchAsync(
            searchTerm,
            includePreRelease,
            skip,
            take,
            cancellationToken));

        var taskResults = await Task.WhenAll(searchTasks);

        return taskResults
            .SelectMany(result => result)
            .OrderBy(package => package.PackageId);
    }
}