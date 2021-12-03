// Copyright (c) 2021 Tripleslash contributors
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

/// <summary>
/// Represents a version bound constraint.
/// </summary>
public class VersionConstraint
{
    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="version">Version of the bounds</param>
    /// <param name="boundsConstraint">Bounds constraint type</param>
    public VersionConstraint(SemVersion version, BoundsConstraint boundsConstraint)
    {
        Guard.Argument(version, nameof(version)).NotNull();
        
        Version = version;
        BoundsConstraint = boundsConstraint;
    }

    /// <summary>
    /// Gets the version.
    /// </summary>
    public SemVersion Version { get; }

    /// <summary>
    /// Gets the bounds constraint.
    /// </summary>
    public BoundsConstraint BoundsConstraint { get; }

    /// <inheritdoc />
    public override string ToString() => $"{BoundsConstraint}:{Version}";
}