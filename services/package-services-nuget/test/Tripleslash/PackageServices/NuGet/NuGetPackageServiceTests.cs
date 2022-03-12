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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Tripleslash.PackageServices.NuGet;

public class NuGetPackageServiceTests
{
    [Fact]
    public async Task CallPackageServicesFromManagedDI()
    {
        var services = new ServiceCollection()
            .AddNuGetPackageService(
                "nuget.org",
                config =>
                {
                    config.ProviderKey = "nuget.org";
                    config.ServiceIndexUrl = "https://api.nuget.org/v3/index.json";
                });

        var provider = services.BuildServiceProvider();
        var packageService = provider.GetRequiredService<IPackageService>();
        var results = await packageService.SearchAsync(
            "serilog",
            0,
            10,
            false,
            CancellationToken.None);

        results.Count.ShouldBe(10);
        results.Any(r => r.PackageId.Id.Equals("serilog", StringComparison.OrdinalIgnoreCase)).ShouldBeTrue();
    }
}