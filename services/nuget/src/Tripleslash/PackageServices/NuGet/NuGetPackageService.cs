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

using Dawn;
using Tripleslash.Infrastructure;

namespace Tripleslash.PackageServices.NuGet;

public class NuGetPackageService : IPackageSearchService
{
    /// <summary>
    /// Creates a new instance.
    /// </summary>
    public NuGetPackageService(NuGetOptions options)
    {
        Guard.Argument(options, nameof(options)).NotNull();
        
        SourceId = options.SourceId ?? throw new InvalidOperationException("Invalid source id");
        Description = options.Description ?? "NuGet service provider";
    }   
    
    /// <inheritdoc />
    public bool SupportsEcosystem(string ecosystem) => "dotnet".EqualsIgnoreCase(Ecosystems.Dotnet);

    /// <inheritdoc />
    public string SourceId { get; }

    /// <inheritdoc />
    public string Description { get; }

    /// <inheritdoc />
    public Task<IEnumerable<PackageMetadata>> Search(
        string searchTerm
        , bool includePreRelease
        , int skip
        , int take
        , CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}