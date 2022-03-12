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

using Tripleslash.Core;

namespace Tripleslash.PackageServices;

/// <summary>
/// Represents a service that aggregates package operations from multiple
/// package sources.
/// </summary>
public interface IPackageServiceAggregator
{
    /// <summary>
    /// Searches the registered package services for metadata matching a term.
    /// </summary>
    /// <param name="ecosystem">Ecosystem</param>
    /// <param name="term">Search term</param>
    /// <param name="page">Page index</param>
    /// <param name="size">Maximum number of results to show per provider</param>
    /// <param name="prerelease">Whether to include prerelease/unstable packages in the results</param>
    /// <param name="cancellationToken">Token that can be observed for cancellation requests</param>
    /// <returns>A task that completes with a <see cref="PackageSearchResult"/> object</returns>
    Task<PackageSearchResult> SearchAsync(
        Ecosystem ecosystem,
        string term,
        int page,
        int size,
        bool prerelease,
        CancellationToken cancellationToken);
}