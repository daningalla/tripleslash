using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Tripleslash.DependencyInjection;

/// <summary>
/// Represents an object used to register keyed services.
/// </summary>
/// <typeparam name="TKey">The key type</typeparam>
/// <typeparam name="TService">The service type</typeparam>
public class KeyedServiceBuilder<TKey, TService> where TKey : notnull
{
    internal KeyedServiceBuilder(IServiceCollection applicationServices)
    {
        ApplicationServices = applicationServices;
    }

    /// <summary>
    /// Gets the service collection.
    /// </summary>
    public IServiceCollection ApplicationServices { get; }
    
    /// <summary>
    /// Adds a service registration that uses the default factory for the
    /// given implementation type.
    /// </summary>
    /// <param name="key">Service key</param>
    /// <typeparam name="TImplementation">Implementation type</typeparam>
    /// <returns>A reference to this instance</returns>
    public KeyedServiceBuilder<TKey, TService> AddTransient<TImplementation>(TKey key)
        where TImplementation : class, TService
    {
        return AddDescriptor(
            ServiceLifetime.Transient,
            key,
            provider => provider.GetRequiredService<TImplementation>());
    }

    /// <summary>
    /// Adds a service registration that uses a factory delegate to provide service
    /// instances.
    /// </summary>
    /// <param name="key">Service key</param>
    /// <param name="implementationFactory">Instance of this service</param>
    /// <returns>A reference to this instance</returns>
    public KeyedServiceBuilder<TKey, TService> AddTransient(
        TKey key,
        Func<IServiceProvider, TService> implementationFactory)
    {
        return AddDescriptor(
            ServiceLifetime.Transient,
            key,
            implementationFactory);
    }
    
    /// <summary>
    /// Adds a service registration that uses the default factory for the
    /// given implementation type.
    /// </summary>
    /// <param name="key">Service key</param>
    /// <typeparam name="TImplementation">Implementation type</typeparam>
    /// <returns>A reference to this instance</returns>
    public KeyedServiceBuilder<TKey, TService> AddScoped<TImplementation>(TKey key)
        where TImplementation : class, TService
    {
        return AddDescriptor(
            ServiceLifetime.Scoped,
            key,
            provider => provider.GetRequiredService<TImplementation>());
    }

    /// <summary>
    /// Adds a service registration that uses a factory delegate to provide service
    /// instances.
    /// </summary>
    /// <param name="key">Service key</param>
    /// <param name="implementationFactory">Instance of this service</param>
    /// <returns>A reference to this instance</returns>
    public KeyedServiceBuilder<TKey, TService> AddScoped(
        TKey key,
        Func<IServiceProvider, TService> implementationFactory)
    {
        return AddDescriptor(
            ServiceLifetime.Scoped,
            key,
            implementationFactory);
    }

    /// <summary>
    /// Adds a service registration that uses the default factory for the
    /// given implementation type.
    /// </summary>
    /// <param name="key">Service key</param>
    /// <typeparam name="TImplementation">Implementation type</typeparam>
    /// <returns>A reference to this instance</returns>
    public KeyedServiceBuilder<TKey, TService> AddSingleton<TImplementation>(TKey key)
        where TImplementation : class, TService
    {
        return AddDescriptor(
            ServiceLifetime.Singleton,
            key,
            provider => provider.GetRequiredService<TImplementation>());
    }

    /// <summary>
    /// Adds a service registration that uses a factory delegate to provide service
    /// instances.
    /// </summary>
    /// <param name="key">Service key</param>
    /// <param name="implementationFactory">Instance of this service</param>
    /// <returns>A reference to this instance</returns>
    public KeyedServiceBuilder<TKey, TService> AddSingleton(
        TKey key,
        Func<IServiceProvider, TService> implementationFactory)
    {
        return AddDescriptor(
            ServiceLifetime.Singleton,
            key,
            implementationFactory);
    }
    
    /// <summary>
    /// Adds a service registration that uses a specific instance of the service type.
    /// </summary>
    /// <param name="key">Service key</param>
    /// <param name="instance">Instance of the service</param>
    /// <returns>A reference to this instance</returns>
    public KeyedServiceBuilder<TKey, TService> AddSingleton(
        TKey key,
        TService instance)
    {
        return AddDescriptor(
            ServiceLifetime.Singleton,
            key,
            _ => instance);
    }

    private KeyedServiceBuilder<TKey, TService> AddDescriptor(
        ServiceLifetime serviceLifetime,
        TKey key,
        Func<IServiceProvider, TService> implementationFactory)
    {
        ApplicationServices.AddSingleton(new KeyedServiceFactory<TKey, TService>(key, implementationFactory));
        
        ApplicationServices.TryAddSingleton(serviceProvider => serviceProvider
            .GetServices<KeyedServiceFactory<TKey, TService>>()
            .ToDictionary(kv => kv.Key, kv => kv.Factory));

        ApplicationServices.TryAdd(new ServiceDescriptor(
            typeof(IKeyedService<TKey, TService>),
            typeof(KeyedService<TKey, TService>),
            serviceLifetime));

        return this;
    }
}