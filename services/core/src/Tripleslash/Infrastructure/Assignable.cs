namespace Tripleslash.Infrastructure;

/// <summary>
/// Represents a simple reference type wrapper around a <typeparamref name="T"/> value
/// that allows for value changes.
/// </summary>
/// <typeparam name="T">Type to wrap</typeparam>
public class Assignable<T>
{
    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="value">Initial value to assign.</param>
    public Assignable(T? value = default) => Value = value;
    
    /// <summary>
    /// Gets or sets the assignable value.
    /// </summary>
    public T? Value { get; set; }

    /// <inheritdoc />
    public override string ToString() => Value?.ToString() ?? string.Empty;
}