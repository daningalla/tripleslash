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

namespace Tripleslash.PackageServices;

/// <summary>
/// Represents basic information about a package.
/// </summary>
public class PackageMetadata
{
    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="packageId">Package id.</param>
    public PackageMetadata(PackageId packageId)
    {
        PackageId = Guard.Argument(packageId, nameof(packageId)).NotNull();
    }

    /// <summary>
    /// Gets the package id
    /// </summary>
    public PackageId PackageId { get; }

    /// <summary>
    /// Gets the source provider
    /// </summary>
    public string? SourceProvider { get; init; }
    
    /// <summary>
    /// Gets the title
    /// </summary>
    public string? Title { get; init; }
    
    /// <summary>
    /// Gets the package description
    /// </summary>
    public string? Description { get; init; }
    
    /// <summary>
    /// Gets the authors
    /// </summary>
    public string? Authors { get; init; }
    
    /// <summary>
    /// Gets the package owners
    /// </summary>
    public string? Owners { get; init; }
    
    /// <summary>
    /// Gets package tags
    /// </summary>
    public string[]? Tags { get; init; }
    
    /// <summary>
    /// Gets the project website url
    /// </summary>
    public string? ProjectUrl { get; init; }
    
    /// <summary>
    /// Gets the company site url
    /// </summary>
    public string? CompanyUrl { get; init; }
    
    /// <summary>
    /// Gets the source repository url
    /// </summary>
    public string? SourceUrl { get; init; }
    
    /// <summary>
    /// Gets the license type
    /// </summary>
    public string? LicenseType { get; init; }
    
    /// <summary>
    /// Gets the license version
    /// </summary>
    public string? LicenseVersion { get; init; }
    
    /// <summary>
    /// Gets the dependency groups
    /// </summary>
    public IReadOnlyCollection<PackageGroup>? DependencyGroups { get; init; }

    /// <inheritdoc />
    public override string ToString() => PackageId.ToString();
}