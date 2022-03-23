//  Copyright 2022 Tripleslash contributors
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using Microsoft.Extensions.Logging;
using Tripleslash.Core;

namespace Tripleslash.PackageServices;

/// <summary>
/// Default implementation of <see cref="IPackageServiceAggregator"/>
/// </summary>
public class PackageServiceAggregator : IPackageServiceAggregator
{
    private readonly IEnumerable<IPackageService> _packageServices;
    private readonly ILogger<PackageServiceAggregator>? _logger;

    public PackageServiceAggregator(
        IEnumerable<IPackageService> packageServices,
        ILogger<PackageServiceAggregator>? logger = null)
    {
        _packageServices = packageServices;
        _logger = logger;
    }
    
    /// <inheritdoc />
    public async Task<PackageSearchResult> SearchAsync(
        Ecosystem ecosystem, 
        string term,
        int page,
        int size,
        bool prerelease,
        CancellationToken cancellationToken)
    {
        var appliedServices = _packageServices.Where(service => service.IsEcosystemSupported(ecosystem));
        var searchTasks = appliedServices.Select(service => GetGroupResultAsync(
            service, 
            term, 
            page, 
            size, 
            prerelease, 
            cancellationToken));
        var resultGroups = await Task.WhenAll(searchTasks);

        return new PackageSearchResult
        {
            ProviderErrors = resultGroups.Count(r => r.Faulted),
            Groups = resultGroups,
            TotalHits = resultGroups.Sum(r => r.Hits)
        };
    }

    private async Task<PackageSearchResultGroup> GetGroupResultAsync(
        IPackageService packageService,
        string term,
        int page,
        int size,
        bool prerelease,
        CancellationToken cancellationToken)
    {
        try
        {
            var results = await packageService.SearchAsync(term, page, size, prerelease, cancellationToken);
            return new PackageSearchResultGroup
            {
                Hits = results.Count,
                Results = results,
                ProviderKey = packageService.ProviderKey
            };
        }
        catch (Exception exception)
        {
            _logger?.LogError(
                exception,
                "Package provider '{ProviderKey}' threw an exception during a search operation",
                packageService.ProviderKey);

            return new PackageSearchResultGroup
            {
                Error = exception.Message,
                ProviderKey = packageService.ProviderKey,
                Faulted = true
            };
        }
    }
}