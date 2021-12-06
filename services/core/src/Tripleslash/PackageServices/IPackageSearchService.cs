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

namespace Tripleslash.PackageServices;

/// <summary>
/// Defines the interface of service that exposes package searching functionality.
/// </summary>
public interface IPackageSearchService
{
    /// <summary>
    /// Determines whether a particular ecosystem is supported.
    /// </summary>
    /// <param name="ecosystem">Ecosystem</param>
    /// <returns><c>true</c> if the service supports searches for the given ecosystem.</returns>
    bool SupportsEcosystem(string ecosystem);
    
    /// <summary>
    /// Gets a source identifier (e.g. nuget.org)
    /// </summary>
    string SourceId { get; }

    /// <summary>
    /// When implemented by a class, executes an asynchronous search operation on
    /// the given keyword.
    /// </summary>
    /// <param name="searchTerm">The search term</param>
    /// <param name="skip">The number of search results to skip</param>
    /// <param name="take">The maximum number of search results to return</param>
    /// <param name="cancellationToken">A token that can be observed for cancellation requests</param>
    /// <returns>A task that completes with search results</returns>
    Task<IEnumerable<PackageMetadata>> Search(
        string searchTerm
        , int skip
        , int take
        , CancellationToken cancellationToken);
}