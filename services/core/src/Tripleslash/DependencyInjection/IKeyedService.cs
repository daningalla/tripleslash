namespace Tripleslash.DependencyInjection;

/// <summary>
/// Represents a keyed service locator.
/// </summary>
/// <typeparam name="TKey">Key type</typeparam>
/// <typeparam name="TService">Service type</typeparam>
public interface IKeyedService<in TKey, out TService> where TKey : notnull
{
    /// <summary>
    /// Retrieves a service that matches the given key.
    /// </summary>
    /// <param name="key">A key that uniquely identifies the service.</param>
    /// <returns>The service instance.</returns>
    /// <exception cref="InvalidOperationException">A suitable service was not located.</exception>
    TService this[TKey key] { get; }
}