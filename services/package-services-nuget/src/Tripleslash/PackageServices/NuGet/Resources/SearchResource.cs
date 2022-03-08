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
using Tripleslash.PackageServices.NuGet.ServiceEntities;

namespace Tripleslash.PackageServices.NuGet.Resources;

internal class SearchResource
{
    private readonly NuGetConfiguration _configuration;
    private readonly ServiceIndexResource _serviceIndexResource;
    private readonly Func<HttpClient> _httpClientFactory;
    private readonly ILogger? _logger;

    internal SearchResource(
        NuGetConfiguration configuration,
        ServiceIndexResource serviceIndexResource,
        Func<HttpClient> httpClientFactory,
        ILoggerFactory? loggerFactory)
    {
        _configuration = configuration;
        _serviceIndexResource = serviceIndexResource;
        _httpClientFactory = httpClientFactory;
        _logger = loggerFactory?.CreateLogger<SearchResource>();
    }

    internal async Task<SearchResultRoot> GetResourceResultAsync(
        string term,
        int page,
        int size,
        bool prerelease,
        CancellationToken cancellationToken)
    {
        var serviceIndex = await _serviceIndexResource.GetResourceAsync(cancellationToken);
        var searchUris = GetSearchResources(serviceIndex);

        return null;
    }

    private IEnumerable<string> GetSearchResources(ServiceIndex serviceIndex)
    {
        if ((_configuration.SearchResources?.Length ?? 0) == 0)
        {
            var msg = $"NuGet package service '{_configuration.ServiceIndexUri}' does not define any search " +
                      "resources in configuration.";

            throw new NotSupportedException(msg);
        }

        var configuredResources = _configuration.SearchResources!;
        var matchedResources = serviceIndex
            .Resources?
            .Where(resource => configuredResources.Any(conf => conf == resource.Type))
            .ToArray();

        if ((matchedResources?.Length ?? 0) == 0)
        {
            var msg = $"NuGet package service '{_configuration.ServiceIndexUri}' does not have any compatible " +
                      "search resources that match configured values";

            throw new NotSupportedException(msg);
        }

        return matchedResources!.Select(res => res.Id!);
    }
} 