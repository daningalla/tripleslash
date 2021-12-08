using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Shouldly;
using Xunit;

namespace Tripleslash.PackageServices.NuGet;

public class IndexResourceTests
{
    [Fact, Trait("Category","Integration")]
    public async Task GetResourceAsyncReturnsValue()
    {
        using var httpClient = new HttpClient();

        var testInstance = new IndexResource(
            "nuget.org",
            new NuGetOptions
            {
                ServiceIndexUrl = "https://api.nuget.org/v3/index.json"
            },
            httpClient,
            NullLogger.Instance);

        var index = await testInstance.GetResourceAsync();
        
        index.Resources.Length.ShouldBeGreaterThan(0);
    }
}