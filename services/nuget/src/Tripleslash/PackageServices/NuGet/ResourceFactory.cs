// Copyright (c) 2021 Tripleslash project contributors
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Tripleslash.PackageServices.NuGet;

/// <summary>
/// Defines a factory object used to create NuGet REST resources.
/// </summary>
public class ResourceFactory
{
    private readonly string _key;
    private readonly NuGetOptions _options;
    private readonly HttpClient _httpClient;
    private readonly ILoggerFactory? _loggerFactory;

    internal ResourceFactory(string key, 
        NuGetOptions options,
        HttpClient httpClient,
        ILoggerFactory? loggerFactory = null)
    {
        _key = key;
        _options = options;
        _httpClient = httpClient;
        _loggerFactory = loggerFactory;

        Index = new IndexResource(_key, options, httpClient, _loggerFactory?.CreateLogger<IndexResource>());
    }

    internal IndexResource Index { get; }
}