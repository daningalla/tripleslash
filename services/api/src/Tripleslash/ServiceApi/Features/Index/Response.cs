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
using Tripleslash.Core.PackageServices;

namespace Tripleslash.ServiceApi.Features.Index;

public class Response
{
    /// <summary>
    /// Creates the response.
    /// </summary>
    private Response()
    {
        Version = SemVersion.Default;
        Ecosystems = Ecosystem.All;
        Resources = new[]
        {
            new Resource("ServiceIndex", "/index"),
            new Resource("SearchService", "/search")
        };
    }

    /// <summary>
    /// Gets the current service metadata.
    /// </summary>
    public static readonly Response Current = new Response();
    
    /// <summary>
    /// Gets the api version.
    /// </summary>
    public SemVersion Version { get; }
    
    /// <summary>
    /// Gets the supported ecosystems.
    /// </summary>
    public IReadOnlyCollection<Ecosystem> Ecosystems { get; }

    /// <summary>
    /// Gets the resources.
    /// </summary>
    public IReadOnlyCollection<Resource> Resources { get; }
}

/// <summary>
/// Defines a resource
/// </summary>
/// <param name="Id">Resource id</param>
/// <param name="Type">Type</param>
public record Resource(string Type, string Id);