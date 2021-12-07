using System.Collections.Concurrent;

namespace Tripleslash.DependencyInjection;

internal sealed class KeyedService<TKey, TService> : IKeyedService<TKey, TService> where TKey : notnull
{
    private readonly Dictionary<TKey, Func<IServiceProvider, TService>> _implementationFactories;
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentDictionary<TKey, TService> _instanceCache = new();

    public KeyedService(Dictionary<TKey, Func<IServiceProvider, TService>> implementationFactories,
        IServiceProvider serviceProvider)
    {
        _implementationFactories = implementationFactories;
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public TService this[TKey key] => _instanceCache.GetOrAdd(key, k => _implementationFactories[k](_serviceProvider));
}