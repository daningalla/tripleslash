// Copyright (c) 2021 Tripleslash project contributors
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

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Tripleslash.PackageServices.NuGet;

/// <summary>
/// Defines a factory object used to create NuGet REST resources.
/// </summary>
public class ResourceFactory
{
    private readonly string _key;
    private readonly NuGetOptions _options;
    private readonly HttpClient _httpClient;
    private readonly ILoggerFactory? _loggerFactory;
    private readonly IndexResource _indexResource;

    internal ResourceFactory(string key, 
        NuGetOptions options,
        HttpClient httpClient,
        ILoggerFactory? loggerFactory = null)
    {
        _key = key;
        _options = options;
        _httpClient = httpClient;
        _loggerFactory = loggerFactory;
        _indexResource = new IndexResource(key, 
            options, 
            httpClient, 
            loggerFactory?.CreateLogger<IndexResource>());
    }

    /// <summary>
    /// Acquires the search resource.
    /// </summary>
    /// <returns><see cref="ISearchResource"/></returns>
    /// <exception cref="NotImplementedException">A search resource was not found in the service index.</exception>
    public async Task<ISearchResource> GetSearchResourceAsync()
    {
        var serviceIndex = await _indexResource.GetResourceAsync();
        
        var searchQueryEntry = serviceIndex
            .Resources
            .FirstOrDefault(svc => 
                serviceIndex.Version == "3.0.0"
                && svc.Type == "SearchQueryService");

        if (searchQueryEntry == null)
        {
            var message = $"Search functionality is not currently available (type={typeof(NuGetPackageService)}, provider={_key}))";
            throw new NotImplementedException(message);
        }

        return new SearchResource(_httpClient, searchQueryEntry.ResourceId, _loggerFactory?.CreateLogger<SearchResource>());
    }
}