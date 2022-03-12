// Copyright 2022 Tripleslash contributors
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Tripleslash.Core;
using Tripleslash.Core.PackageServices;
using Tripleslash.PackageServices.NuGet.Mappers;
using Tripleslash.PackageServices.NuGet.Resources;

namespace Tripleslash.PackageServices.NuGet;

/// <summary>
/// Implements <see cref="IPackageService"/> using a NuGet service.
/// </summary>
public class NuGetPackageService : IPackageService
{
    private readonly NuGetConfiguration _configuration;
    private readonly Func<HttpClient> _httpClientFactory;
    private readonly IMemoryCache? _memoryCache;
    private readonly ILoggerFactory? _loggerFactory;

    /// <summary>
    /// Creates a new instance of this type
    /// </summary>
    /// <param name="configuration">NuGet configuration</param>
    /// <param name="httpClientFactory">Function that provides <see cref="HttpContent"/></param>
    /// <param name="memoryCache">Optional configured memory cache</param>
    /// <param name="loggerFactory">Logger factory</param>
    public NuGetPackageService(
        NuGetConfiguration configuration,
        Func<HttpClient> httpClientFactory,
        IMemoryCache? memoryCache = null,
        ILoggerFactory? loggerFactory = null)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _memoryCache = memoryCache;
        _loggerFactory = loggerFactory;
    }

    /// <inheritdoc />
    public string ProviderKey => _configuration.ProviderKey;

    /// <inheritdoc />
    public string Description => _configuration.Description ?? "NuGet repository";

    /// <inheritdoc />
    public bool IsEcosystemSupported(Ecosystem ecosystem) => ecosystem == Ecosystem.Dotnet;

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<PackageMetadata>> SearchAsync(
        string term, 
        int page, 
        int size, 
        bool prerelease,
        CancellationToken cancellationToken)
    {
        using var requestScope = new RequestScope(
            _configuration,
            _httpClientFactory(),
            _memoryCache,
            _loggerFactory);

        var searchResource = await requestScope.GetSearchResourceAsync(cancellationToken);
        var result = await searchResource.GetResourceResultAsync(
            term,
            page,
            size,
            prerelease,
            cancellationToken);

        return result
            .Data?
            .Select(item => item.AsPackageMetadata(_configuration))
            .ToArray() ?? Array.Empty<PackageMetadata>();
    }
}