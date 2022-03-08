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

using System.Text.Json.Serialization;
using Dawn;

namespace Tripleslash.ServiceApi.Shared;

/// <summary>
/// Describes a hypermedia link.
/// </summary>
public class HypermediaLink
{
    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="id">Link resource id</param>
    /// <param name="relation">Relation type</param>
    public HypermediaLink(string id, string relation)
    {
        Id = Guard.Argument(id, nameof(id)).NotNull().NotWhiteSpace();
        Relation = Guard.Argument(relation, nameof(relation)).NotNull().NotWhiteSpace();
    }
    
    /// <summary>
    /// Gets the link id.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// Gets the relation.
    /// </summary>
    [JsonPropertyName("rel")]
    public string Relation { get; }
}