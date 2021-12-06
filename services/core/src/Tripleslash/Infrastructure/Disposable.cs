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

namespace Tripleslash.Infrastructure;

/// <summary>
/// Represents a wrapper around a disposable object.
/// </summary>
public class Disposable<T> : IDisposable where T : class
{
    private readonly T _target;
    private readonly Action<T> _disposeAction;
    private int _disposeCount;

    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="target">The target to dispose.</param>
    /// <param name="disposeAction">The action used to dispose of the resource.</param>
    public Disposable(T target, Action<T> disposeAction)
    {
        Guard.Argument(target, nameof(target)).NotNull();
        Guard.Argument(disposeAction, nameof(disposeAction)).NotNull();
        
        _target = target;
        _disposeAction = disposeAction;
    }
    
    /// <summary>
    /// Releases the underlying resource.
    /// </summary>
    public void Dispose()
    {
        if (Interlocked.Increment(ref _disposeCount) == 1)
        {
            _disposeAction(_target);
        }
    }

    /// <inheritdoc />
    public override string ToString() => $"Target={_target}";
}