using System.Text.RegularExpressions;
using Shouldly;
using Tripleslash.PackageServices;
using Xunit;

namespace TripleSlash.PackageServices;

public class PackageDependencyTests
{
    [Theory]
    [InlineData("*<dotnet:tripleslash<*", "dotnet:tripleslash:1.0.0")]
    [InlineData("1.0.0<dotnet:tripleslash<*", "dotnet:tripleslash:1.0.1")]
    [InlineData("1.0.0<=dotnet:tripleslash<*", "dotnet:tripleslash:1.0.0")]
    [InlineData("1.0.0<=dotnet:tripleslash<*", "dotnet:tripleslash:1.0.1")]
    [InlineData("1.0.0=dotnet:tripleslash<*", "dotnet:tripleslash:1.0.0")]
    [InlineData("*<dotnet:tripleslash<1.1.0", "dotnet:tripleslash:1.0.1")]
    [InlineData("*<dotnet:tripleslash<=1.1.0", "dotnet:tripleslash:1.0.1")]
    [InlineData("*<dotnet:tripleslash<=1.1.0", "dotnet:tripleslash:1.1.0")]
    [InlineData("*<dotnet:tripleslash=1.1.0", "dotnet:tripleslash:1.1.0")]
    public static void IsCompatibleWithIsTrue(string dep, string id)
    {
        CreateDependency(dep).IsCompatibleWith(CreateId(id)).ShouldBeTrue();
    }

    [Theory]
    [InlineData("*<dotnet:tripleslash<*", "npm:tripleslash:1.0.0")]
    [InlineData("*<dotnet:tripleslash<*", "dotnet:api-docs:1.0.0")]
    [InlineData("2.0.0<dotnet:tripleslash<*", "dotnet:tripleslash:2.0.0")]
    [InlineData("2.0.0<=dotnet:tripleslash<*", "dotnet:tripleslash:1.9.0")]
    [InlineData("2.0.0=dotnet:tripleslash<*", "dotnet:tripleslash:1.9.0")]
    [InlineData("*<dotnet:tripleslash<2.0.0", "dotnet:tripleslash:2.0.0")]
    [InlineData("*<dotnet:tripleslash<=2.0.0", "dotnet:tripleslash:2.0.1")]
    [InlineData("*<dotnet:tripleslash=2.0.0", "dotnet:tripleslash:2.0.1")]
    public static void IsCompatibleWithIsFalse(string dep, string id)
    {
        CreateDependency(dep).IsCompatibleWith(CreateId(id)).ShouldBeFalse();
    }
    
    private static PackageId CreateId(string s)
    {
        var split = s.Split(":");
        return new PackageId(split[0], split[1], SemVersion.Parse(split[2]));
    }

    private static PackageDependency CreateDependency(string s)
    {
        var match = Regex.Match(s, @"(?<lower>(\*|[\d\.]+))(?<lop><?=?)(?<eco>\w+):(?<id>\w+)(?<rop><?=?)(?<upper>(\*|[\d\.]+))");
        return new PackageDependency(match.Groups["eco"].Value
            , match.Groups["id"].Value
            , CreateVersionConstraint(match.Groups["lower"].Value, match.Groups["lop"].Value)
            , CreateVersionConstraint(match.Groups["upper"].Value, match.Groups["rop"].Value));
    }

    private static VersionConstraint? CreateVersionConstraint(string version, string op)
    {
        return version != "*"
            ? new VersionConstraint(version, op switch
            {
                "<" => BoundsConstraint.Exclusive,
                "<=" => BoundsConstraint.Inclusive,
                _ => BoundsConstraint.ExactMatch
            })
            : null;
    }
}