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

namespace Tripleslash.Core.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds a keyed service to the service collection with a <see cref="ServiceLifetime.Transient"/> lifetime.
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="key">Service key</param>
    /// <param name="implementationFactory">Implementation factory</param>
    /// <typeparam name="TService">Service type</typeparam>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddKeyedTransient<TService>(
        this IServiceCollection serviceCollection,
        string key,
        Func<IServiceProvider, TService> implementationFactory)
        where TService: class => serviceCollection.AddKeyedService(key, implementationFactory, ServiceLifetime.Transient);
    
    /// <summary>
    /// Adds a keyed service to the service collection with a <see cref="ServiceLifetime.Scoped"/> lifetime.
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="key">Service key</param>
    /// <param name="implementationFactory">Implementation factory</param>
    /// <typeparam name="TService">Service type</typeparam>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddKeyedScoped<TService>(
        this IServiceCollection serviceCollection,
        string key,
        Func<IServiceProvider, TService> implementationFactory)
        where TService: class => serviceCollection.AddKeyedService(key, implementationFactory, ServiceLifetime.Scoped);
    
    /// <summary>
    /// Adds a keyed service to the service collection with a <see cref="ServiceLifetime.Singleton"/> lifetime.
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="key">Service key</param>
    /// <param name="implementationFactory">Implementation factory</param>
    /// <typeparam name="TService">Service type</typeparam>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddKeyedSingleton<TService>(
        this IServiceCollection serviceCollection,
        string key,
        Func<IServiceProvider, TService> implementationFactory)
        where TService: class => serviceCollection.AddKeyedService(key, implementationFactory, ServiceLifetime.Singleton);
    
    private static IServiceCollection AddKeyedService<TService>(
        this IServiceCollection serviceCollection,
        string key,
        Func<IServiceProvider, TService> implementationFactory,
        ServiceLifetime serviceLifetime)
        where TService : class
    {
        serviceCollection.Add(ServiceDescriptor.Describe(
            typeof(KeyedService<TService>),
            provider => new KeyedService<TService>(key, implementationFactory(provider)),
            serviceLifetime));

        serviceCollection.Add(ServiceDescriptor.Describe(
            typeof(TService),
            provider => provider.GetServices<KeyedService<TService>>().First(ks => ks.Key == key).Service,
            serviceLifetime));

        return serviceCollection;
    }
}