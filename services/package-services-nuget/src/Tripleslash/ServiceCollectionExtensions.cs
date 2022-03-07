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

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tripleslash.Core.DependencyInjection;
using Tripleslash.PackageServices;
using Tripleslash.PackageServices.NuGet;

namespace Tripleslash;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds NuGet package services to the service collection.
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="key">A unique key for the provider.</param>
    /// <param name="configureAction">A delegate used to customize the NuGet configuration.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddNuGetPackageServices(
        this IServiceCollection serviceCollection,
        string key,
        Action<NuGetConfiguration> configureAction)
    {
        serviceCollection.AddPackageServices();

        var httpClientKey = $"{key}{Guid.NewGuid().ToString("N")[..8]}";

        serviceCollection.AddHttpClient(httpClientKey, httpClient =>
        {
            // TODO: Configure auth headers
        });

        serviceCollection.Configure(key, configureAction);
        
        serviceCollection.AddKeyedSingleton<IPackageService>(key, provider => new NuGetPackageService(
            provider.GetRequiredService<IOptionsSnapshot<NuGetConfiguration>>().Get(key),
            () => provider.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientKey),
            provider.GetRequiredService<ILogger<NuGetPackageService>>()));

        return serviceCollection;
    }
}