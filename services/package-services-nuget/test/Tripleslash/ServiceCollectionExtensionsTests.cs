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

using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tripleslash.PackageServices;
using Xunit;

namespace Tripleslash;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddNugetPackageServicesReadConfiguration()
    {
        var configJson = @"{
                'PackageServices': {
                    'nuget': {
                        'nuget.org': {
                            'Description': 'Microsoft NuGet Provider',
                            'ServiceIndexUrl': 'https://api.nuget.org/v3/index.json'
                        },
                        'myGet' : {
                            'Description': 'Private MyGet feed',
                            'ServiceIndexUrl': 'https://tripleslash.net/myget/index.json'
                        }
                    }
                }
            }".Replace('\'', '"');
        
        using var jsonStream = new MemoryStream();
        using var streamWriter = new StreamWriter(jsonStream);
        streamWriter.Write(configJson);
        streamWriter.Flush();
        jsonStream.Position = 0;

        var configuration = new ConfigurationBuilder().AddJsonStream(jsonStream).Build();
        var section = configuration.GetSection("PackageServices:nuget");
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddNuGetPackageServices(section);

        var provider = serviceCollection.BuildServiceProvider();
        var packageServices = provider.GetServices<IPackageService>().ToArray();
        
        packageServices.Length.ShouldBe(2);
        packageServices.Count(ps => ps.ProviderKey == "nuget.org").ShouldBe(1);
        packageServices.Count(ps => ps.ProviderKey == "myGet").ShouldBe(1);
    }
}