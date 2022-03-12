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
    /// Gets the package source friendly name.
    /// </summary>
    public string PackageSource { get; init; } = default!;

    /// <summary>
    /// Gets the package id.
    /// </summary>
    public PackageId PackageId { get; init; } = default!;

    /// <summary>
    /// Gets the package title.
    /// </summary>
    public string? Title { get; init; } = default!;

    /// <summary>
    /// Gets the package summary.
    /// </summary>
    public string? Summary { get; init; } = default!;

    /// <summary>
    /// Gets the package authors.
    /// </summary>
    public string[]? Authors { get; init; } = Array.Empty<string>();
    
    /// <summary>
    /// Gets the project uri.
    /// </summary>
    public string? ProjectUri { get; init; }
    
    /// <summary>
    /// Gets the source repository uri.
    /// </summary>
    public string? RepositoryUri { get; init; }
    
    /// <summary>
    /// Gets the repository type.
    /// </summary>
    public string? RepositoryType { get; init; }
    
    /// <summary>
    /// Gets the license url.
    /// </summary>
    public string? LicenseUrl { get; init; }
    
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
    /// Gets the total download count.
    /// </summary>
    public int? TotalDownloads { get; init; }
    
    
    
    /// <summary>
    /// Gets properties meaningful to the package source.
    /// </summary>
    public Dictionary<string, string>? SourceProperties { get; init; }

    /// <inheritdoc />
    public override string ToString() => $"{PackageId}";
}