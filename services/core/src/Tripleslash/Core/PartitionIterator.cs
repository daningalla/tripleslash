// Copyright (c) 2021 Tripleslash project contributors
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace Tripleslash.Core;

/// <summary>
/// Represents an iterator that efficiently splits arrays of <typeparamref name="T"/>
/// elements without allocating additional heap objects (like string.Split())
/// </summary>
/// <typeparam name="T">The array element type.</typeparam>
public ref struct PartitionIterator<T>
{
    private readonly ReadOnlySpan<T> _span;
    private readonly T _splitValue;
    private readonly IEqualityComparer<T> _equalityComparer;
    private int _position;

    internal PartitionIterator(ReadOnlySpan<T> span
        , T splitValue
        , IEqualityComparer<T>? equalityComparer = null)
    {
        Current = ReadOnlySpan<T>.Empty;

        _span = span;
        _splitValue = splitValue;
        _equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
        _position = -1;
    }

    /// <summary>
    /// Gets the span of <typeparamref name="T"/> elements in the current partition. 
    /// </summary>
    public ReadOnlySpan<T> Current { get; private set; }

    /// <summary>
    /// Advances the iterator to the next partition identified by the split
    /// element value.
    /// </summary>
    /// <returns><c>true</c> if the partition is not empty</returns>
    public bool MoveNext()
    {
        var marker = ++_position;

        for (; _position < _span.Length; _position++)
        {
            if (!_equalityComparer.Equals(_span[_position], _splitValue))
                continue;

            Current = _span[marker.._position];
            return true;
        }

        Current = marker < _span.Length ? _span[marker..] : ReadOnlySpan<T>.Empty;
        
        return !Current.IsEmpty;
    }

    /// <inheritdoc />
    public override string ToString() => $"Current={Current.ToString()}";
}