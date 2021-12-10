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

using System.Text.Json.Serialization;

namespace Tripleslash.PackageServices.NuGet;

/// <summary>
/// Represents the service index.
/// </summary>
public class ServiceIndex
{
    /// <summary>
    /// Defines an empty index
    /// </summary>
    public static ServiceIndex Empty { get; } = new ServiceIndex();
    
    [JsonPropertyName("version")]
    public string Version { get; set; } = default!;

    [JsonPropertyName("resources")]
    public ServiceIndexEntry[] Resources { get; set; } = Array.Empty<ServiceIndexEntry>();

    /// <inheritdoc />
    public override string ToString() => $"version {Version}, resources=[{Resources.Length}]";
}