using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tripleslash.DependencyInjection;
using Tripleslash.Infrastructure;
using Xunit;

namespace TripleSlash.DependencyInjection;

public class KeyedServiceBuilderTests
{
    public interface IManagedService
    {
        string Key { get; }
    }

    public class ManagedServiceType1 : IManagedService
    {
        /// <inheritdoc />
        public string Key { get; init; } = default!;
    }
    public class ManagedServiceType2 : IManagedService
    {
        /// <inheritdoc />
        public string Key { get; init; } = default!;
    }

    [Fact]
    public void AddTransientCreatesUniqueInstances()   
    {
        var services = new ServiceCollection()
            .AddKeyedServices<string, IManagedService>(builder => builder.AddTransient("a", 
                    _ => new ManagedServiceType1{ Key = "a"}))
            .BuildServiceProvider();

        var ks1 = services.GetRequiredService<IKeyedService<string, IManagedService>>();
        var ks2 = services.GetRequiredService<IKeyedService<string, IManagedService>>();

        ks1.ShouldNotBe(ks2);
        ks1["a"].ShouldNotBe(ks2["a"]);
    }

    [Fact]
    public void AddTransientCreatesKeyedInstances()
    {
        var services = new ServiceCollection()
            .AddKeyedServices<string, IManagedService>(builder => builder
                .AddTransient("a", _ => new ManagedServiceType1{Key="a"})
                .AddTransient("b", _ => new ManagedServiceType1{Key="b"}))
            .BuildServiceProvider();

        var keyedService = services.GetRequiredService<IKeyedService<string, IManagedService>>();
        keyedService["a"].Key.ShouldBe("a");
        keyedService["b"].Key.ShouldBe("b");
    }
    
    [Fact]
    public void AddTransientCreatesKeyedInstancesOfType()
    {
        var services = new ServiceCollection()
            .AddKeyedServices<string, IManagedService>(builder => builder
                .AddTransient("a", _ => new ManagedServiceType1{Key="a"})
                .AddTransient("b", _ => new ManagedServiceType2{Key="b"}))
            .BuildServiceProvider();

        var keyedService = services.GetRequiredService<IKeyedService<string, IManagedService>>();
        keyedService["a"].ShouldBeOfType<ManagedServiceType1>();
        keyedService["b"].ShouldBeOfType<ManagedServiceType2>();
    }
    
    [Fact]
    public void AddScopedCreatesScopedInstances()   
    {
        var services = new ServiceCollection()
            .AddKeyedServices<string, IManagedService>(builder => builder.AddScoped("a", 
                _ => new ManagedServiceType1{Key="a"}))
            .BuildServiceProvider();

        using var scope1 = services.CreateScope();
        var keyedServiceScope1 = scope1.ServiceProvider.GetRequiredService<IKeyedService<string, IManagedService>>();
        var service1 = keyedServiceScope1["a"];
        var service2 = keyedServiceScope1["a"];
        // Same scope, same service
        service1.ShouldBe(service2);

        using var scope2 = services.CreateScope();
        var keyedServiceScope2 = scope2.ServiceProvider.GetRequiredService<IKeyedService<string, IManagedService>>();
        // Different scope, different service
        keyedServiceScope1.ShouldNotBe(keyedServiceScope2);
    }

    [Fact]
    public void AddSingletonCreatesSingletonInstances()
    {
        var services = new ServiceCollection()
            .AddKeyedServices<string, IManagedService>(builder => builder.AddSingleton("a", new ManagedServiceType1{Key="a"}))
            .BuildServiceProvider();

        var keyed = services.GetRequiredService<IKeyedService<string, IManagedService>>();
        var instance = keyed["a"];
        keyed["a"].ShouldBe(instance);

        using var scope = services.CreateScope();
        scope.ServiceProvider.GetRequiredService<IKeyedService<string, IManagedService>>()["a"].ShouldBe(instance);
    }
}