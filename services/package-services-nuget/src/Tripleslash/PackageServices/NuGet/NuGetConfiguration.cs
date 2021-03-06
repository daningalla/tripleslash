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

namespace Tripleslash.PackageServices.NuGet;

/// <summary>
/// Represents a configuration to connect to NuGet.
/// </summary>
public class NuGetConfiguration
{
    /// <summary>
    /// Gets or sets the service key.
    /// </summary>
    public string ProviderKey { get; set; } = default!;

    /// <summary>
    /// Gets a description of the service.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Gets or sets the service index URI for the NuGet repository.
    /// </summary>
    public string ServiceIndexUrl { get; set; } = default!;
}