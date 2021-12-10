using Shouldly;
using Tripleslash.IO;
using Xunit;

namespace TripleSlash.IO;

public class GlobPatternTests
{
    [Theory]
    [InlineData("*.*", "", "*.*")]
    [InlineData("**/*.json", "", "**/*.json")]
    [InlineData("**\\*.json", "", "**\\*.json")]
    [InlineData("/lib/*.txt", "/lib/", "*.txt")]
    [InlineData("/lib/**/*.txt", "/lib/", "**/*.txt")]
    [InlineData("c:\\users\\*.txt", "c:\\users\\", "*.txt")]
    [InlineData("c:\\users\\**\\*.txt", "c:\\users\\", "**\\*.txt")]
    public void TryParseReturnsValueWithExpectedProperties(string path,
        string expectedBasePath,
        string expectedPattern)
    {
        GlobPattern.TryParse(path, out var value).ShouldBeTrue();
        
        value.BasePath.ShouldBe(expectedBasePath);
        value.Pattern.ShouldBe(expectedPattern);
    }

    [Theory]
    [InlineData("")]
    [InlineData("dir")]
    [InlineData("dir/path")]
    [InlineData("dir\\path")]
    [InlineData("dir\\path\\path")]
    [InlineData("/dir/path/path")]
    [InlineData("file.txt")]
    public void TryParseReturnsFalse(string path)
    {
        GlobPattern.TryParse(path, out _).ShouldBeFalse();
    }
}