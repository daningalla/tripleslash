// Copyright 2022 Tripleslash contributors
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

using Tripleslash.Core;
using Tripleslash.Core.PackageServices;

namespace Tripleslash.PackageServices;

/// <summary>
/// Represents the interface of an object that
/// </summary>
public interface IPackageService
{
    /// <summary>
    /// Gets the service provider key.
    /// </summary>
    string ProviderKey { get; }
    
    /// <summary>
    /// Gets a description of the package service.
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// Determines if this service implementation supports the specified ecosystem.
    /// </summary>
    /// <param name="ecosystem">Ecosystem</param>
    /// <returns><c>true</c> if the ecosystem is supported, <c>false</c> otherwise.</returns>
    bool IsEcosystemSupported(Ecosystem ecosystem);

    /// <summary>
    /// Searches a term.
    /// </summary>
    /// <param name="term">Full or partial term to search</param>
    /// <param name="page">Zero-based page index</param>
    /// <param name="size">Maximum number of results per page</param>
    /// <param name="prerelease">Whether to include pre-release/unstable packages</param>
    /// <param name="cancellationToken">Token that can be observed for cancellation requests</param>
    /// <returns>Task</returns>
    Task<IReadOnlyCollection<PackageMetadata>> SearchAsync(
        string term, 
        int page, 
        int size,
        bool prerelease,
        CancellationToken cancellationToken);
}