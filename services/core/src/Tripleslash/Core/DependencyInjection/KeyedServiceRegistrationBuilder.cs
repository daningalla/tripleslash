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

using Microsoft.Extensions.DependencyInjection;

namespace Tripleslash.Core.DependencyInjection;

/// <summary>
/// Used to build keyed services.
/// </summary>
public class KeyedServiceRegistrationBuilder<TService> where TService : class
{
    private readonly ServiceLifetime _lifetime;
    private readonly string _key;

    internal KeyedServiceRegistrationBuilder(
        IServiceCollection serviceCollection, 
        string key,
        ServiceLifetime lifetime)
    {
        _key = key;
        _lifetime = lifetime;
        ServiceCollection = serviceCollection;
    }

    /// <summary>
    /// Gets the service collection.
    /// </summary>
    public IServiceCollection ServiceCollection { get; }

    /// <summary>
    /// Configures the implementation factory for the service.
    /// </summary>
    /// <param name="implementationFactory">Implementation factory</param>
    /// <returns>A reference to this instance</returns>
    public KeyedServiceRegistrationBuilder<TService> Factory(Func<IServiceProvider, TService> implementationFactory)
    {
        ServiceCollection.Add(ServiceDescriptor.Describe(
            typeof(KeyedService<TService>),
            provider => new KeyedService<TService>(_key, implementationFactory(provider)),
            _lifetime));

        Decorate<TService>();

        return this;
    }

    /// <summary>
    /// Configures options for the service.
    /// </summary>
    /// <param name="configureOptions">A delegate used to configure the options object.</param>
    /// <typeparam name="TOptions">Options type</typeparam>
    /// <returns>A reference to this instance.</returns>
    public KeyedServiceRegistrationBuilder<TService> Configure<TOptions>(Action<TOptions> configureOptions)
        where TOptions : class
    {
        ServiceCollection.Configure(_key, configureOptions);
        return this;
    }
    
    /// <summary>
    /// Configures options for the service.
    /// </summary>
    /// <param name="configureOptions">A delegate used to configure the options object.</param>
    /// <typeparam name="TOptions">Options type</typeparam>
    /// <returns>A reference to this instance.</returns>
    public KeyedServiceRegistrationBuilder<TService> PostConfigure<TOptions>(Action<TOptions> configureOptions)
        where TOptions : class
    {
        ServiceCollection.PostConfigure(_key, configureOptions);
        return this;
    }

    /// <summary>
    /// Decorates a registration as the given type.
    /// </summary>
    /// <typeparam name="TDecorator">Decorator type</typeparam>
    /// <returns>A reference to this instance.</returns>
    public KeyedServiceRegistrationBuilder<TService> Decorate<TDecorator>() where TDecorator : class
    {
        ServiceCollection.Add(ServiceDescriptor.Describe(
            typeof(TDecorator),
            provider => provider.GetKeyedService<TService>(_key),
            _lifetime));

        return this;
    }
}