using System.Runtime.CompilerServices;

namespace Tripleslash.Infrastructure;

/// <summary>
/// Asynchronous lazy synchronization.
/// </summary>
/// <typeparam name="T">Type of object being asynchronously initialized</typeparam>
public class AsyncLazy<T>
{
    private readonly Lazy<Task<T>> _underlyingLazy;

    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="factory">Factory function that returns a task that completes with the lazy value.</param>
    public AsyncLazy(Func<Task<T>> factory)
    {
        _underlyingLazy = new(() => Task.Run(factory));
    }

    /// <summary>
    /// Provides await support.
    /// </summary>
    /// <returns><see cref="TaskAwaiter{T}"/></returns>
    public TaskAwaiter<T> GetAwaiter() => _underlyingLazy.Value.GetAwaiter();
}