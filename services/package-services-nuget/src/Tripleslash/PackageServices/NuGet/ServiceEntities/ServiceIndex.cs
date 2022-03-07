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

using System.Text.Json.Serialization;

namespace Tripleslash.PackageServices.NuGet.ServiceEntities;

/// <summmary>
///   Represents a structural entity in the source JSON document.
///   Path: /
/// </summmary>
public class ServiceIndex
{
	/// <summmary>
	///   Gets or sets the 'version' value.
	/// </summmary>
	/// <remarks>
	///   Path: /version
	///   Sample value: '3.0.0'
	/// </remarks>
	[JsonPropertyName("version")]
	public string? Version { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'resources' value.
	/// </summmary>
	/// <remarks>
	///   Path: /resources
	/// </remarks>
	[JsonPropertyName("resources")]
	public RootResourcesItem[]? Resources { get; set; } = Array.Empty<RootResourcesItem>();
	
	/// <summmary>
	///   Gets or sets the '@context' value.
	/// </summmary>
	/// <remarks>
	///   Path: /@context
	/// </remarks>
	[JsonPropertyName("@context")]
	public RootContext? Context { get; set; }
}