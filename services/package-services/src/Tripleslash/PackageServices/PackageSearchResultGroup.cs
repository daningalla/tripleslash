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

using Tripleslash.Core.PackageServices;

namespace Tripleslash.PackageServices;

/// <summary>
/// Defines a group of search results.
/// </summary>
public class PackageSearchResultGroup
{
    /// <summary>
    /// Gets the provider key.
    /// </summary>
    public string ProviderKey { get; init; } = default!;
    
    /// <summary>
    /// Gets the number of results produced by the provider.
    /// </summary>
    public int Hits { get; init; }
    
    /// <summary>
    /// Gets whether the search for this provider faulted.
    /// </summary>
    public bool Faulted { get; init; }
    
    /// <summary>
    /// If faulted, provides a description of the error that occurred.
    /// </summary>
    public string? Error { get; init; }

    /// <summary>
    /// Gets the results of this provider.
    /// </summary>
    public IReadOnlyCollection<PackageMetadata> Results { get; init; } = Array.Empty<PackageMetadata>();
}