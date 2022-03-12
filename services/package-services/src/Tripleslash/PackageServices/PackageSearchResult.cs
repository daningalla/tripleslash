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

namespace Tripleslash.PackageServices;

/// <summary>
/// Represents an aggregated search result.
/// </summary>
public class PackageSearchResult
{
    /// <summary>
    /// Gets the total number of results.
    /// </summary>
    public int TotalHits { get; init; }
    
    /// <summary>
    /// Gets the number of provider errors.
    /// </summary>
    public int ProviderErrors { get; init; }
    
    /// <summary>
    /// Gets the package search result groups.
    /// </summary>
    public IReadOnlyCollection<PackageSearchResultGroup> Groups { get; init; } =
        Array.Empty<PackageSearchResultGroup>();
}