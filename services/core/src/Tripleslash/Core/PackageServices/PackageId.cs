//  Copyright 2022 Tripleslash contributors
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

namespace Tripleslash.Core.PackageServices;

/// <summary>
/// Represents a unique package id.
/// </summary>
public class PackageId : IEquatable<PackageId>, IComparable<PackageId>
{
    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="ecosystem">Ecosystem</param>
    /// <param name="id">Package id</param>
    /// <param name="version">Version</param>
    public PackageId(Ecosystem ecosystem, string id, SemVersion version)
    {
        Ecosystem = ecosystem;
        Id = id;
        Version = version;
    }

    /// <summary>
    /// Gets the package ecosystem.
    /// </summary>
    public Ecosystem Ecosystem { get; }

    /// <summary>
    /// Gets the package id.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Gets the sem version.
    /// </summary>
    public SemVersion Version { get; }

    /// <inheritdoc />
    public override string ToString() => $"{Id}-{Version} ({Ecosystem})";

    /// <inheritdoc />
    public bool Equals(PackageId? other)
    {
        return other != null &&
               Ecosystem == other.Ecosystem &&
               Id == other.Id &&
               Version == other.Version;
    }

    /// <inheritdoc />
    public int CompareTo(PackageId? other)
    {
        int compare;

        return other == null ? 1
            : (compare = Comparer<Ecosystem>.Default.Compare(Ecosystem, other.Ecosystem)) != 0 ? compare
            : (compare = StringComparer.OrdinalIgnoreCase.Compare(Id, other.Id)) != 0 ? compare
            : Version.CompareTo(other.Version);
    }

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Ecosystem, Id, Version);
}