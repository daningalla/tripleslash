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

using MediatR;
using Tripleslash.Core;

namespace Tripleslash.ServiceApi.Features.Search;

/// <summary>
/// Defines the search request.
/// </summary>
public class Request : IRequest<ResponseContext>
{
    /// <summary>
    /// Gets the ecosystem filter.
    /// </summary>
    public string Ecosystem { get; set; } = default!;

    /// <summary>
    /// Gets the search term.
    /// </summary>
    public string Term { get; set; } = default!;
    
    /// <summary>
    /// Gets the page id for pagination.
    /// </summary>
    public int Page { get; set; }
    
    /// <summary>
    /// Gets the maximum number of results to return.
    /// </summary>
    public int Size { get; set; }
    
    /// <summary>
    /// Gets whether to include prerelease/unstable packages in results.
    /// </summary>
    public bool PreRelease { get; set; }
}