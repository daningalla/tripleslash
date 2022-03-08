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
using Microsoft.Extensions.Options;

namespace Tripleslash.Core.DependencyInjection;

public static class ServiceProviderExtensions
{
    /// <summary>
    /// Gets a keyed service.
    /// </summary>
    /// <param name="serviceProvider">Service provider</param>
    /// <param name="key">Key</param>
    /// <typeparam name="TService">Service type</typeparam>
    /// <returns>An instance of this service</returns>
    public static TService GetKeyedService<TService>(this IServiceProvider serviceProvider, string key)
        where TService : class
    {
        return serviceProvider
            .GetServices<KeyedService<TService>>()
            .First(keyed => keyed.Key == key)
            .Service;
    }

    /// <summary>
    /// Gets options for a keyed service.
    /// </summary>
    /// <param name="serviceProvider">Service provider</param>
    /// <param name="key">Service key</param>
    /// <typeparam name="TService">Service type</typeparam>
    /// <typeparam name="TOptions">Options type</typeparam>
    /// <returns><see cref="TOptions"/> instance</returns>
    public static TOptions GetKeyedOptions<TService, TOptions>(this IServiceProvider serviceProvider, string key)
        where TOptions : class
    {
        return serviceProvider
            .GetRequiredService<IOptionsSnapshot<TOptions>>()
            .Get(key);
    }
}