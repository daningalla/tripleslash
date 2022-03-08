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
    /// <typeparam name="TService">Service type</typeparam>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static KeyedServiceRegistrationBuilder<TService> AddKeyedTransient<TService>(
        this IServiceCollection serviceCollection,
        string key)
        where TService : class => new(serviceCollection, key, ServiceLifetime.Transient);
    
    /// <summary>
    /// Adds a keyed service to the service collection with a <see cref="ServiceLifetime.Transient"/> lifetime.
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="key">Service key</param>
    /// <typeparam name="TService">Service type</typeparam>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static KeyedServiceRegistrationBuilder<TService> AddKeyedScoped<TService>(
        this IServiceCollection serviceCollection,
        string key)
        where TService : class => new(serviceCollection, key, ServiceLifetime.Scoped);
    
    /// <summary>
    /// Adds a keyed service to the service collection with a <see cref="ServiceLifetime.Transient"/> lifetime.
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="key">Service key</param>
    /// <typeparam name="TService">Service type</typeparam>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static KeyedServiceRegistrationBuilder<TService> AddKeyedSingleton<TService>(
        this IServiceCollection serviceCollection,
        string key)
        where TService : class => new(serviceCollection, key, ServiceLifetime.Singleton);
}