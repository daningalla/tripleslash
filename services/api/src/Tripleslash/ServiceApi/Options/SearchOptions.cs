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

namespace Tripleslash.ServiceApi.Options;

/// <summary>
/// Defines search options
/// </summary>
public class SearchOptions
{
    /// <summary>
    /// Gets or sets the default result size.
    /// </summary>
    public int DefaultResultSize { get; set; } = 25;

    /// <summary>
    /// Gets or sets the maximum number of results that can be requested in search.
    /// </summary>
    public int MaxResultSize { get; set; } = 50;

    /// <summary>
    /// Gets or sets the minimum search term length.
    /// </summary>
    public int MinTermLength { get; set; } = 3;
}