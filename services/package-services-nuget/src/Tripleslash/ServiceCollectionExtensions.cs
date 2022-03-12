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

using Dawn;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
    /// <param name="configuration">
    /// A configuration section that contains one or more NuGet service configurations.
    /// </param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddNuGetPackageServices(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        Guard.Argument(serviceCollection, nameof(serviceCollection)).NotNull();
        Guard.Argument(configuration, nameof(configuration)).NotNull();

        var configDictionary = new Dictionary<string, NuGetConfiguration>();
        configuration.Bind(configDictionary);

        foreach (var (key, config) in configDictionary)
        {
            serviceCollection.AddNuGetPackageService(key, target =>
            {
                target.Description = config.Description;
                target.ServiceIndexUrl = config.ServiceIndexUrl;
                target.ProviderKey = key;
            });
        }

        return serviceCollection;
    }
    
    /// <summary>
    /// Adds NuGet package services to the service collection.
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="key">A unique key for the provider.</param>
    /// <param name="configureAction">A delegate used to customize the NuGet configuration.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddNuGetPackageService(
        this IServiceCollection serviceCollection,
        string key,
        Action<NuGetConfiguration> configureAction)
    {
        Guard.Argument(serviceCollection, nameof(serviceCollection)).NotNull();
        Guard.Argument(key, nameof(key)).NotNull().NotWhiteSpace();
        Guard.Argument(configureAction, nameof(configureAction)).NotNull();

        serviceCollection.AddPackageServices();

        serviceCollection.AddHttpClient(key);

        serviceCollection.AddMemoryCache();

        serviceCollection.Configure(key, configureAction);

        serviceCollection.PostConfigure<NuGetConfiguration>(options => ValidateConfiguration(key, options));

        serviceCollection.AddKeyedScoped<NuGetPackageService>(key)
            .Factory(provider => new NuGetPackageService(
                provider.GetKeyedOptions<NuGetConfiguration>(key),
                () => provider.GetRequiredService<IHttpClientFactory>().CreateClient(key),
                provider.GetService<IMemoryCache>(),
                provider.GetService<ILoggerFactory>()))
            .Decorate<IPackageService>();

        return serviceCollection;
    }

    private static void ValidateConfiguration(string key, NuGetConfiguration obj)
    {
        if (obj.ServiceIndexUrl == null)
        {
            throw new InvalidOperationException("Missing required NuGet configuration value 'ServiceIndexUri'");
        }

        obj.ProviderKey = key;
    }
}