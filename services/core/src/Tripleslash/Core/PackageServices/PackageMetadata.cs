// Copyright 2022 Tripleslash contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Tripleslash.Core.PackageServices;

/// <summary>
/// Represents metadata about a package.
/// </summary>
public class PackageMetadata
{
    /// <summary>
    /// Gets the package ecosystem.
    /// </summary>
    public Ecosystem Ecosystem { get; init; }

    /// <summary>
    /// Gets the package source.
    /// </summary>
    public string PackageSource { get; init; } = default!;

    /// <summary>
    /// Gets the package id.
    /// </summary>
    public string PackageId { get; init; } = default!;

    /// <summary>
    /// Gets the package version.
    /// </summary>
    public SemVersion Version { get; init; } = default!;

    /// <summary>
    /// Gets the package authors.
    /// </summary>
    public string[]? Authors { get; init; } = Array.Empty<string>();
    
    /// <summary>
    /// Gets the project uri.
    /// </summary>
    public Uri? ProjectUri { get; init; }
    
    /// <summary>
    /// Gets the source repository uri.
    /// </summary>
    public Uri? RepositoryUri { get; init; }
    
    /// <summary>
    /// Gets the repository type.
    /// </summary>
    public RepositoryType RepositoryType { get; init; }
    
    /// <summary>
    /// Gets the package description.
    /// </summary>
    public string? Description { get; init; }
    
    /// <summary>
    /// Gets the package icon url.
    /// </summary>
    public string? IconUrl { get; init; }
    
    /// <summary>
    /// Gets package tags.
    /// </summary>
    public string[]? Tags { get; init; }
    
    /// <summary>
    /// Gets properties meaningful to the package source.
    /// </summary>
    public Dictionary<string, string>? SourceProperties { get; init; }

    /// <inheritdoc />
    public override string ToString() => $"[{Ecosystem}] {PackageId}-{Version}";

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Ecosystem, PackageId, Version);
}