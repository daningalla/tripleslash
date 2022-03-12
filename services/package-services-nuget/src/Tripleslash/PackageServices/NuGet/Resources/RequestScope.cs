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

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Tripleslash.PackageServices.NuGet.Resources;

internal class RequestScope : IDisposable
{
    private readonly NuGetConfiguration _configuration;
    private readonly IMemoryCache? _memoryCache;
    private readonly ILoggerFactory? _loggerFactory;
    private readonly HttpClient _httpClient;

    internal RequestScope(
        NuGetConfiguration configuration,
        HttpClient httpClient,
        IMemoryCache? memoryCache = null,
        ILoggerFactory? loggerFactory = null)
    {
        _configuration = configuration;
        _httpClient = httpClient;
        _memoryCache = memoryCache;
        _loggerFactory = loggerFactory;
    }

    public void Dispose() => _httpClient.Dispose();

    internal ServiceIndexResource GetServiceIndexResource() => new(
        _configuration,
        _httpClient,
        _memoryCache,
        _loggerFactory);

    internal async Task<SearchResource> GetSearchResourceAsync(CancellationToken cancellationToken)
    {
        var serviceIndexResource = GetServiceIndexResource();
        var serviceIndex = await serviceIndexResource.GetResourceAsync(cancellationToken);

        return new SearchResource(_configuration, serviceIndex, _httpClient, _loggerFactory);
    }
}