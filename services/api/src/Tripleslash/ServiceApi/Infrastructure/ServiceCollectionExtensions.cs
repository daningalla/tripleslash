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

using Dawn;
using FluentValidation;
using MediatR;
using Tripleslash.ServiceApi.Mediator;
using Tripleslash.ServiceApi.Options;

namespace Tripleslash.ServiceApi.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTripleslashServices(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        Guard.Argument(serviceCollection, nameof(serviceCollection)).NotNull();
        Guard.Argument(configuration, nameof(configuration)).NotNull();

        var assembly = typeof(Program).Assembly;
        
        // Options
        serviceCollection.Configure<SearchOptions>(configuration.GetSection("Search").Bind);
        
        // Request services
        serviceCollection.AddMediatR(assembly);
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        serviceCollection.AddValidatorsFromAssembly(assembly);
        serviceCollection.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

        // Tripleslash services
        serviceCollection.AddNuGetPackageServices(configuration.GetSection("PackageServices:nuget"));

        return serviceCollection;
    }
}