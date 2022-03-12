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
using Tripleslash.PackageServices.NuGet.ServiceEntities;

namespace Tripleslash.PackageServices.NuGet.Mappers;

internal static class PackageMetadataExtensions
{
    public static PackageMetadata AsPackageMetadata(
        this SearchResultEntry entry,
        NuGetConfiguration configuration)
    {
        return new PackageMetadata
        {
            Authors = entry.Authors,
            Description = entry.Description,
            Tags = entry.Tags,
            PackageId = new PackageId(Ecosystem.Dotnet, entry.PackageId!, SemVersion.Parse(entry.Version!)),
            IconUrl = entry.IconUrl,
            PackageSource = configuration.ProviderKey,
            ProjectUri = entry.ProjectUrl,
            SourceProperties = new()
            {
                { "@id", entry.Id! },
                { "registration", entry.Registration! }
            },
            Title = entry.Title,
            Summary = entry.Summary,
            LicenseUrl = entry.LicenseUrl
        };
    }
}