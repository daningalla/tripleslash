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

using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using Dawn;
using Microsoft.Extensions.Logging;
using Tripleslash.PackageServices.NuGet.ServiceEntities;

namespace Tripleslash.PackageServices.NuGet.Resources;

internal class SearchResource
{
    private const string SearchResourceType = "SearchQueryService";
    
    private readonly NuGetConfiguration _configuration;
    private readonly ServiceIndex _serviceIndex;
    private readonly HttpClient _httpClient;
    private readonly ILogger? _logger;

    internal SearchResource(
        NuGetConfiguration configuration,
        ServiceIndex serviceIndex,
        HttpClient httpClient,
        ILoggerFactory? loggerFactory)
    {
        _configuration = configuration;
        _serviceIndex = serviceIndex;
        _httpClient = httpClient;
        _logger = loggerFactory?.CreateLogger<SearchResource>();
    }

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    internal async Task<SearchResultRoot> GetResourceResultAsync(
        string term,
        int page,
        int size,
        bool prerelease,
        CancellationToken cancellationToken)
    {
        Guard.Argument(term, nameof(term)).NotNull().NotWhiteSpace();
        Guard.Argument(page, nameof(page)).GreaterThan(-1);
        Guard.Argument(size, nameof(size)).GreaterThan(0);

        var searchUris = GetSearchResources();

        foreach (var uri in searchUris)
        {
            try
            {
                _logger?.LogDebug("Executing package search (resource={Resource}, term={Term})",
                    uri,
                    term);
                
                var request = $"{uri}?q={term}&skip={page * size}&take={size}&prerelease={prerelease}";
                var result = await _httpClient.GetFromJsonAsync<SearchResultRoot>(request, cancellationToken);
                
                if (result != null)
                {
                    _logger?.LogTrace("Received search result (count={ResultCount})", result.Data?.Length);
                    return result;
                }
            }
            catch (Exception exception)
            {
                _logger?.LogWarning(exception, "An error was caught during Http request processing");
            }
        }

        var msg = $"Could not obtain a result from search services (provider='{_configuration.ProviderKey}'). " +
                  $"The following resources were tried: [{string.Join(",", searchUris)}]";
        
        throw new InvalidOperationException(msg);
    }

    private IEnumerable<string> GetSearchResources()
    {
        var matchedResources = _serviceIndex
            .Resources?
            .Where(resource => resource.Type == SearchResourceType)
            .ToArray();

        if ((matchedResources?.Length ?? 0) == 0)
        {
            var msg = $"NuGet package service '{_configuration.ProviderKey}' does not have any compatible " +
                      "search resources that match configured values";

            throw new NotSupportedException(msg);
        }

        return matchedResources!.Select(res => res.Id!);
    }
} 