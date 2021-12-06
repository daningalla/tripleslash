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

namespace Tripleslash.Infrastructure;

/// <summary>
/// Represents a wrapper around an object that is asynchronously
/// disposable.
/// </summary>
/// <typeparam name="T"></typeparam>
public class AsyncDisposable<T> : IAsyncDisposable where T : class
{
    private readonly T _target;
    private readonly Func<T, ValueTask> _asyncDisposeAction;
    private int _disposeCount;

    public AsyncDisposable(T target, Func<T, ValueTask> asyncDisposeAction)
    {
        _target = target;
        _asyncDisposeAction = asyncDisposeAction;
    }

    /// <summary>
    /// Releases the underlying resource asynchronously.
    /// </summary>
    /// <returns><see cref="ValueTask"/></returns>
    public ValueTask DisposeAsync()
    {
        if (Interlocked.Increment(ref _disposeCount) == 1)
        {
            return _asyncDisposeAction(_target);
        }
        
        return ValueTask.CompletedTask;
    }
}