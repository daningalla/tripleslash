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
/// Aggregates search results across multiple package services.
/// </summary>
public interface IPackageSearchAggregator
{
    /// <summary>
    /// Searches a term.
    /// </summary>
    /// <param name="ecosystem">Ecosystem to search</param>
    /// <param name="term">Full or partial term to search</param>
    /// <param name="page">Zero-based page index</param>
    /// <param name="size">Maximum number of results per page</param>
    /// <param name="prerelease">Whether to include pre-release/unstable packages</param>
    /// <param name="cancellationToken">Token that can be observed for cancellation requests</param>
    /// <returns>Task</returns>
    Task<IReadOnlyCollection<PackageMetadata>> SearchAsync(
        string ecosystem, 
        string term, 
        int page, 
        int size,
        bool prerelease,
        CancellationToken cancellationToken);
}