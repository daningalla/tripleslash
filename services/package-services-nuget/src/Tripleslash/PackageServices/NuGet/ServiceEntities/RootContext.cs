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
///   Path: /@context
/// </summmary>
public class RootContext
{
    /// <summmary>
    ///   Gets or sets the '@vocab' value.
    /// </summmary>
    /// <remarks>
    ///   Path: /@context/@vocab
    ///   Sample value: 'http://schema.nuget.org/services#'
    /// </remarks>
    [JsonPropertyName("@vocab")]
    public string? Vocab { get; set; } = string.Empty;
	
    /// <summmary>
    ///   Gets or sets the 'comment' value.
    /// </summmary>
    /// <remarks>
    ///   Path: /@context/comment
    ///   Sample value: 'http://www.w3.org/2000/01/rdf-schema#comment'
    /// </remarks>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; } = string.Empty;
}