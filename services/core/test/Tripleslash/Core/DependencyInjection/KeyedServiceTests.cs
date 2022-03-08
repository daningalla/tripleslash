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

using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Tripleslash.Core.DependencyInjection;

public class KeyedServiceTests
{
    public interface IServiceInterface {}
    public interface IServiceInterface2 {}

    public class ServiceImplementation : IServiceInterface, IServiceInterface2
    {
        public ServiceImplementation(string key) => Key = key;
        
        public string Key { get; set; }
    }

    [Fact]
    public void RegisterProvidesServiceImplementation()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection
            .AddKeyedSingleton<ServiceImplementation>("a")
            .Factory(_ => new ServiceImplementation("a"));

        var services = serviceCollection.BuildServiceProvider();

        services.GetRequiredService<ServiceImplementation>().Key.ShouldBe("a");
    }

    [Fact]
    public void RegisterProvidesMultipleServiceImplementations()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection
            .AddKeyedSingleton<ServiceImplementation>("a")
            .Factory(_ => new ServiceImplementation("a"));

        serviceCollection
            .AddKeyedSingleton<ServiceImplementation>("b")
            .Factory(_ => new ServiceImplementation("b"));

        var services = serviceCollection
            .BuildServiceProvider()
            .GetServices<ServiceImplementation>()
            .ToArray();
        
        services[0].Key.ShouldBe("a");
        services[1].Key.ShouldBe("b");
    }

    [Fact]
    public void RegisterDecorates()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection
            .AddKeyedSingleton<ServiceImplementation>("a")
            .Factory(_ => new ServiceImplementation("a"))
            .Decorate<IServiceInterface>();

        serviceCollection
            .AddKeyedSingleton<ServiceImplementation>("b")
            .Factory(_ => new ServiceImplementation("b"))
            .Decorate<IServiceInterface2>();

        var services = serviceCollection.BuildServiceProvider();

        var a = (ServiceImplementation) services
            .GetServices<IServiceInterface>()
            .Single();

        a.Key.ShouldBe("a");

        var b = (ServiceImplementation)services
            .GetServices<IServiceInterface2>()
            .Single();
        
        b.Key.ShouldBe("b");
    }
}