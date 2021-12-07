namespace Tripleslash.DependencyInjection;

internal sealed record KeyedServiceFactory<TKey, TService>(
    TKey Key, 
    Func<IServiceProvider, TService> Factory);