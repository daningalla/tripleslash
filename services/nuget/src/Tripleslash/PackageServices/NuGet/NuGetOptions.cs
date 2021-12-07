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

namespace Tripleslash.PackageServices.NuGet;

/// <summary>
/// Defines options used to connect to NuGet.
/// </summary>
public class NuGetOptions
{
    /// <summary>
    /// Gets or sets the source id.
    /// </summary>
    public string? SourceId { get; set; }
    
    /// <summary>
    /// Gets or sets the service description.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Gets or sets the service index url.
    /// </summary>
    public string? ServiceIndexUrl { get; set; }
    
    /// <summary>
    /// Gets or sets the authorization type.
    /// </summary>
    public string? AuthorizationType { get; set; }
    
    /// <summary>
    /// Gets or sets a dictionary of secrets used to connect to the nuget server
    /// (depends on authorization type).
    /// </summary>
    public IDictionary<string, string>? Secrets { get; set; }
}