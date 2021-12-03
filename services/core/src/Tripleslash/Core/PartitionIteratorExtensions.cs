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

using Dawn;

namespace Tripleslash.Core;

public static class PartitionIteratorExtensions
{
    /// <summary>
    /// Creates a <see cref="PartitionIterator{T}"/> for the given span.
    /// </summary>
    /// <param name="span">Span to split</param>
    /// <param name="splitValue">The value used to partition the array</param>
    /// <param name="equalityComparer">Optional equality comparer to evaluate matches to the split value</param>
    /// <typeparam name="T">Element type</typeparam>
    /// <returns><see cref="PartitionIterator{T}"/></returns>
    public static PartitionIterator<T> PartitionBy<T>(this ReadOnlySpan<T> span
        , T splitValue
        , IEqualityComparer<T>? equalityComparer = null)
    {
        return new PartitionIterator<T>(span, splitValue, equalityComparer);
    }
    
    /// <summary>
    /// Creates a <see cref="PartitionIterator{T}"/> for the given span.
    /// </summary>
    /// <param name="source">Array to split</param>
    /// <param name="splitValue">The value used to partition the array</param>
    /// <param name="equalityComparer">Optional equality comparer to evaluate matches to the split value</param>
    /// <typeparam name="T">Element type</typeparam>
    /// <returns><see cref="PartitionIterator{T}"/></returns>
    public static PartitionIterator<T> PartitionBy<T>(this T[] source
        , T splitValue
        , IEqualityComparer<T>? equalityComparer = null)
    {
        Guard.Argument(source, nameof(source)).NotNull();
        
        return new PartitionIterator<T>(source.AsSpan(), splitValue, equalityComparer);
    }
    
    /// <summary>
    /// Creates a <see cref="PartitionIterator{T}"/> for the given span.
    /// </summary>
    /// <param name="str">String to split</param>
    /// <param name="splitValue">The value used to partition the array</param>
    /// <param name="equalityComparer">Optional equality comparer to evaluate matches to the split character</param>
    /// <returns><see cref="PartitionIterator{T}"/></returns>
    public static PartitionIterator<char> PartitionBy(this string str
        , char splitValue
        , IEqualityComparer<char>? equalityComparer = null)
    {
        return new PartitionIterator<char>(str.AsSpan(), splitValue, equalityComparer);
    }
}