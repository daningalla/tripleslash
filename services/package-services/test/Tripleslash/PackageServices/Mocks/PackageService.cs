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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Tripleslash.Core;
using Tripleslash.Core.PackageServices;

namespace Tripleslash.PackageServices.Mocks;

public class PackageService : IPackageService
{
    public Ecosystem[] Ecosystems { get; set; } = { Ecosystem.Dotnet };

    /// <inheritdoc />
    public string ProviderKey { get; set; } = default!;

    /// <inheritdoc />
    public string DisplayName { get; set; } = default!;

    /// <inheritdoc />
    public string Description { get; set; } = default!;

    public PackageMetadata[] Packages { get; set; } = Array.Empty<PackageMetadata>();

    /// <inheritdoc />
    public bool IsEcosystemSupported(Ecosystem ecosystem) => Ecosystems.Contains(ecosystem);

    /// <inheritdoc />
    public Task<IReadOnlyCollection<PackageMetadata>> SearchAsync(
        string term, 
        int page, 
        int size, 
        bool prerelease, 
        CancellationToken cancellationToken)
    {
        var result = Packages
            .Where(p => Regex.IsMatch(p.PackageId.Id, term) && (prerelease || p.PackageId.Version.PreRelease == null))
            .Skip(page * size)
            .Take(size)
            .ToList();

        return Task.FromResult<IReadOnlyCollection<PackageMetadata>>(result);
    }
}