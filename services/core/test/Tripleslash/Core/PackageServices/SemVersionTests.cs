//    Copyright 2022 Tripleslash contributors
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using System.Linq;
using Shouldly;
using Xunit;

namespace Tripleslash.Core.PackageServices;

public class SemVersionTests
{
    [Theory]
    [InlineData("1.2.3", 1, 2, 3, null, null)]
    [InlineData("1.2.3-alpha", 1, 2, 3, "alpha", null)]
    [InlineData("1.2.3+meta", 1, 2, 3, null, "meta")]
    public void ParseMatchesExpectedComponents(string s, int major, int minor, int patch, string? pre, string? meta)
    {
        var value = SemVersion.Parse(s);
        
        value.Major.ShouldBe(major);
        value.Minor.ShouldBe(minor);
        value.Patch.ShouldBe(patch);
        value.PreRelease.ShouldBe(pre);
        value.Metadata.ShouldBe(meta);
    }

    [Fact]
    public void CompareGreaterThanPrevious()
    {
        var versions = new[]
            {
                "1.0.0",
                "1.1.0",
                "1.1.1",
                "1.1.2-alpha",
                "1.1.2-alpha.1",
                "1.1.2-alpha.beta",
                "1.1.2-beta",
                "1.1.2-beta.2",
                "1.1.2-beta.11",
                "1.1.2-rc.1",
                "1.1.2"
            }
            .Select(SemVersion.Parse)
            .ToArray();

        for (var c = 1; c < versions.Length; c++)
        {
            versions[c-1].CompareTo(versions[c]).ShouldBe(-1);
        }
    }
}