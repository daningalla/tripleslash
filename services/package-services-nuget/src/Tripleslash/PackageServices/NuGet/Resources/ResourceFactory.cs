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

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Tripleslash.PackageServices.NuGet.Resources;

internal class ResourceFactory
{
    private readonly NuGetConfiguration _configuration;
    private readonly Func<HttpClient> _httpClientFactory;
    private readonly IMemoryCache? _memoryCache;
    private readonly ILoggerFactory? _loggerFactory;

    internal ResourceFactory(
        NuGetConfiguration configuration,
        Func<HttpClient> httpClientFactory,
        IMemoryCache? memoryCache = null,
        ILoggerFactory? loggerFactory = null)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _memoryCache = memoryCache;
        _loggerFactory = loggerFactory;
    }

    internal ServiceIndexResource GetServiceIndexResource() => new(
        _configuration,
        _httpClientFactory,
        _memoryCache,
        _loggerFactory);
}