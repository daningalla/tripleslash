// Copyright (c) 2021 Tripleslash contributors
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

using Dawn;
using Microsoft.Extensions.DependencyInjection;
using Tripleslash.DependencyInjection;

namespace Tripleslash.PackageServices;

/// <summary>
/// Configures package services
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds package services.
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="configureServices">A delegate used to configure providers.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddPackageServices(
        this IServiceCollection serviceCollection,
        Action<ServiceBuilder<IPackageService>> configureServices)
    {
        Guard.Argument(serviceCollection, nameof(serviceCollection)).NotNull();
        Guard.Argument(configureServices, nameof(configureServices)).NotNull();
        
        configureServices(new ServiceBuilder<IPackageService>(serviceCollection));

        serviceCollection.AddSingleton<IPackageSearchAggregator, PackageSearchAggregator>();
        
        return serviceCollection;
    }
}