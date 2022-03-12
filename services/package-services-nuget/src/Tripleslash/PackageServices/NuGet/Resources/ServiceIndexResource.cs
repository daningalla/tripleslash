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

using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Tripleslash.PackageServices.NuGet.ServiceEntities;

namespace Tripleslash.PackageServices.NuGet.Resources;

internal sealed class ServiceIndexResource
{
    private readonly NuGetConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache? _memoryCache;
    private readonly ILogger? _logger;
    private readonly string _cachedIndexKey = $"serviceIndex__{Guid.NewGuid():N}";

    internal ServiceIndexResource(
        NuGetConfiguration configuration,
        HttpClient httpClient,
        IMemoryCache? memoryCache = null,
        ILoggerFactory? loggerFactory = null)
    {
        _configuration = configuration;
        _httpClient = httpClient;
        _memoryCache = memoryCache;
        _logger = loggerFactory?.CreateLogger<ServiceIndexResource>();
    }

    internal async Task<ServiceIndex> GetResourceAsync(CancellationToken cancellationToken)
    {
        if (_memoryCache?.TryGetValue(_cachedIndexKey, out var cachedIndex) == true)
        {
            _logger?.LogTrace("Using cached service index");
            return (ServiceIndex)cachedIndex;
        }

        var uri = _configuration.ServiceIndexUrl;
        
        var index = await _httpClient.GetFromJsonAsync<ServiceIndex>(
            uri,
            cancellationToken);

        if (index == null)
        {
            throw new InvalidOperationException($"Could not retrieve service index from {uri}");
        }
        
        _logger?.LogTrace("Caching service index (key={CacheKey)}", _cachedIndexKey);
        _memoryCache?.Set(_cachedIndexKey, index);

        return index;
    }
}