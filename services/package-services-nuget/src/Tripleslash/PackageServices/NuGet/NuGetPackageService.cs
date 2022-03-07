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

using Microsoft.Extensions.Logging;
using Tripleslash.Core;

namespace Tripleslash.PackageServices.NuGet;

public class NuGetPackageService : IPackageService
{
    private readonly NuGetConfiguration _configuration;
    private readonly Func<HttpClient> _httpClientFactory;
    private readonly ILogger<NuGetPackageService>? _logger;

    public NuGetPackageService(
        NuGetConfiguration configuration,
        Func<HttpClient> httpClientFactory,
        ILogger<NuGetPackageService>? logger = null)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    /// <inheritdoc />
    public string Description => _configuration.Description ?? "NuGet repository";

    /// <inheritdoc />
    public bool IsEcosystemSupported(Ecosystem ecosystem) => ecosystem == Ecosystem.Dotnet;
}