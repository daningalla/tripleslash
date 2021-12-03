using System.Collections.Generic;
using Shouldly;
using Tripleslash.Core;
using Xunit;

namespace TripleSlash.Core;

public class SemVersionTests
{
    [Theory]
    [InlineData("1.0.0", 1, 0, 0, null, null)]
    [InlineData("1.2.0", 1, 2, 0, null, null)]
    [InlineData("1.2.3", 1, 2, 3, null, null)]
    [InlineData("1.2.3-alpha", 1, 2, 3, "alpha", null)]
    [InlineData("1.2.3+build", 1, 2, 3, null, "build")]
    [InlineData("1.2.3-alpha+build", 1, 2, 3, "alpha", "build")]
    public void ParseIsEqualToExpectedValue(string s
        , byte expectedMajor
        , uint expectedMinor
        , uint expectedPatch
        , string? expectedPreRelease
        , string? expectedBuild)
    {
        var semver = SemVersion.Parse(s);
        
        semver.Major.ShouldBe(expectedMajor);
        semver.Minor.ShouldBe(expectedMinor);
        semver.Patch.ShouldBe(expectedPatch);
        semver.Prerelease.ShouldBe(expectedPreRelease);
        semver.BuildMetadata.ShouldBe(expectedBuild);
        
        semver.ToString().ShouldBe(s);
    }

    [Theory]
    [InlineData("1.0.0", "2.0.0")]
    [InlineData("1.0.0", "1.1.0")]
    [InlineData("1.0.0", "1.0.1")]
    [InlineData("1.0.0-alpha", "1.0.0")]
    [InlineData("1.0.0-alpha", "1.0.0-alpha.1")]
    [InlineData("1.0.0-alpha.1", "1.0.0-alpha.beta")]
    [InlineData("1.0.0-alpha.beta", "1.0.0-beta")]
    [InlineData("1.0.0-beta", "1.0.0-beta.2")]
    [InlineData("1.0.0-beta.2", "1.0.0-beta.11")]
    [InlineData("1.0.0-beta.11", "1.0.0-rc.1")]
    [InlineData("1.0.0-rc.1", "1.0.0")]
    public void LessThanComparisonShouldBeTrue(string x, string y)
    {
        var lesser = SemVersion.Parse(x);
        var greater = SemVersion.Parse(y);
        
        Comparer<SemVersion>.Default.Compare(lesser, greater).ShouldBeLessThan(0);
        
        (lesser < greater).ShouldBeTrue();
        (lesser <= greater).ShouldBeTrue();
        (lesser != greater).ShouldBeTrue();
        (lesser == greater).ShouldBeFalse();
        (lesser > greater).ShouldBeFalse();
        (lesser >= greater).ShouldBeFalse();
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("1.2.3", "1.2.3")]
    [InlineData("1.2.3-pre", "1.2.3-pre")]
    [InlineData("1.2.3+build", "1.2.3+build")]
    [InlineData("1.2.3-pre+build", "1.2.3-pre+build")]
    [InlineData("1.2.3-pre+build.001", "1.2.3-pre+build.002")]
    public void EqualsComparisonShouldBeTrue(string? x, string? y)
    {
        var left = x != null ? SemVersion.Parse(x) : null;
        var right = y != null ? SemVersion.Parse(y) : null;
        
        Comparer<SemVersion>.Default.Compare(left, right).ShouldBe(0);

        (left == right).ShouldBeTrue();
        (left < right).ShouldBeFalse();
        (left <= right).ShouldBeTrue();
        (left != right).ShouldBeFalse();
        (left > right).ShouldBeFalse();
        (left >= right).ShouldBeTrue();
    }
}