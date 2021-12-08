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

using System.Net.Http.Json;
using Dawn;
using Microsoft.Extensions.Logging;
using Polly;
using Tripleslash.Infrastructure;
using Tripleslash.Logging;

namespace Tripleslash.PackageServices.NuGet;

/// <summary>
/// Represents the index resource
/// </summary>
internal class IndexResource
{
    private readonly string _serviceKey;
    private readonly ILogger? _logger;
    private readonly NuGetOptions _options;
    private readonly HttpClient _httpClient;
    private AsyncLazy<ServiceIndex> _lazyServiceIndex;

    internal IndexResource(
        string serviceKey,
        NuGetOptions options,
        HttpClient httpClient,
        ILogger? logger = null)
    {
        Guard.Argument(serviceKey, nameof(serviceKey)).NotNull().NotWhiteSpace();
        Guard.Argument(options, nameof(options)).NotNull();
        Guard.Argument(httpClient, nameof(httpClient)).NotNull();
        
        _serviceKey = serviceKey;
        _logger = logger;
        _options = options;
        _httpClient = httpClient;
        _lazyServiceIndex = new AsyncLazy<ServiceIndex>(GetEndpointsAsync);
    }

    public async Task<ServiceIndex> GetResourceAsync()
    {
        var resource = await _lazyServiceIndex;

        if (resource == ServiceIndex.Empty)
        {
            // Re-initialize lazy for next time
            Interlocked.Exchange(ref _lazyServiceIndex, new AsyncLazy<ServiceIndex>(GetEndpointsAsync));
        }

        return resource;
    }

    private async Task<ServiceIndex> GetEndpointsAsync()
    {
        var url = _options.ServiceIndexUrl;
        
        _logger?.LogInformation("Fetching service index for {serviceKey}, url={Url}",
            _serviceKey,
            url);

        var policy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(5)
            }, (exception, _) => _logger?.LogError(EventRegistry.FailedHttpRequest,
                exception,
                "Request to {Url} failed with status code {StatusCode}", url, ((HttpRequestException)exception).StatusCode));

        var response = await policy.ExecuteAsync(cancellation => _httpClient.GetFromJsonAsync(
                url,
                typeof(ServiceIndex),
                cancellation),
            CancellationToken.None);

        if (response is ServiceIndex serviceIndex)
        {
            _logger?.LogInformation("Service index retrieved from {Url} successfully", url);
            return serviceIndex;
        }

        return ServiceIndex.Empty;
    }
}