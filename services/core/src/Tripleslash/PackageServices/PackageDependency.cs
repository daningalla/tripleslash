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

namespace Tripleslash.PackageServices;

/// <summary>
/// Represents the metadata of a package dependency.
/// </summary>
public class PackageDependency
{
    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="ecosystem">Ecosystem</param>
    /// <param name="id">Package id</param>
    /// <param name="lowerBound">Optional lower version bounds</param>
    /// <param name="upperBound">Option upper version bounds</param>
    public PackageDependency(string ecosystem
        , string id
        , VersionConstraint? lowerBound
        , VersionConstraint? upperBound)
    {
        Ecosystem = ecosystem;
        Id = id;
        LowerBound = lowerBound;
        UpperBound = upperBound;
    }

    /// <summary>
    /// Gets the ecosystem
    /// </summary>
    public string Ecosystem { get; }

    /// <summary>
    /// Gets the package id
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Gets the lower bound
    /// </summary>
    public VersionConstraint? LowerBound { get; }

    /// <summary>
    /// Gets the upper bound
    /// </summary>
    public VersionConstraint? UpperBound { get; }

    /// <summary>
    /// Determines whether the given package is suitable for this dependency.
    /// </summary>
    /// <param name="packageId">Package id</param>
    /// <returns>
    /// <c>true</c> if <paramref name="packageId"/> identifies a package with the same
    /// name, ecosystem, and compatible semantic version that complies with the
    /// constraints of this instance.
    /// </returns>
    public bool IsCompatibleWith(PackageId packageId)
    {
        return packageId.Ecosystem == Ecosystem
               && packageId.Id == Id
               && LowerBound switch
               {
                   { BoundsConstraint: BoundsConstraint.Exclusive } => LowerBound.Version < packageId.Version,
                   { BoundsConstraint: BoundsConstraint.Inclusive } => LowerBound.Version <= packageId.Version,
                   { BoundsConstraint: BoundsConstraint.ExactMatch } => LowerBound.Version == packageId.Version,
                   _ => true // null
               }
               && UpperBound switch
               {
                   { BoundsConstraint: BoundsConstraint.Exclusive } => packageId.Version < UpperBound.Version,
                   { BoundsConstraint: BoundsConstraint.Inclusive } => packageId.Version <= UpperBound.Version,
                   { BoundsConstraint: BoundsConstraint.ExactMatch } => packageId.Version == UpperBound.Version,
                   _ => true // null
               };
    }

    /// <inheritdoc />
    public override string ToString()
    {
        var relation = LowerBound?.BoundsConstraint switch
        {
            BoundsConstraint.Exclusive => "<",
            BoundsConstraint.Inclusive => "<=",
            _ => "="
        };
        
        return this switch
        {
            { LowerBound:{}, UpperBound: {}} when LowerBound == UpperBound => $"{Id}: [version]={LowerBound}",
            { LowerBound:{}, UpperBound: {}} => $"{Id}: {LowerBound} {relation} [version] {relation} {UpperBound}",
            { LowerBound: {}} => $"{Id}: {LowerBound} {relation} [version] <= *",
            { UpperBound: {}} => $"{Id}: * <= [version] {relation} {UpperBound}",
            _ => $"{Id}L version = *"
        };
    }
}