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
/// Represents a generic collection for a group of package dependencies.
/// </summary>
public class PackageGroup
{
    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="groupName">Group name</param>
    /// <param name="dependencies">
    /// Dependencies to include. If this parameter is null, an empty array is assigned.
    /// </param>
    public PackageGroup(string groupName, IReadOnlyCollection<PackageDependency>? dependencies)
    {
        Guard.Argument(groupName, nameof(groupName)).NotNull().NotWhiteSpace();
        
        GroupName = groupName;
        Dependencies = dependencies ?? Array.Empty<PackageDependency>();
    }

    /// <summary>
    /// Gets the group name.
    /// </summary>
    public string GroupName { get; }

    /// <summary>
    /// Gets the dependencies.
    /// </summary>
    public IReadOnlyCollection<PackageDependency> Dependencies { get; }

    /// <inheritdoc />
    public override string ToString() => $"{GroupName} {string.Join('+', Dependencies.Select(d => d.ToString()))}";
}