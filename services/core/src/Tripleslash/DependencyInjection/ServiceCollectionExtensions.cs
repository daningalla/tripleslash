using Dawn;
using Microsoft.Extensions.DependencyInjection;

namespace Tripleslash.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKeyedServices<TKey, TService>(
        this IServiceCollection serviceCollection,
        Action<KeyedServiceBuilder<TKey, TService>> builder)
        where TKey : notnull
    {
        Guard.Argument(serviceCollection, nameof(serviceCollection)).NotNull();
        Guard.Argument(builder, nameof(builder)).NotNull();
        
        builder(new KeyedServiceBuilder<TKey, TService>(serviceCollection));
        return serviceCollection;
    }
}