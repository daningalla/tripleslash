using Dawn;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tripleslash.PackageServices.NuGet;

namespace Tripleslash.PackageServices;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds a NuGet service provider.
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="serviceKey">Service key</param>
    /// <param name="configureOptions">Action used to configure options</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddNuGetProvider(
        this IServiceCollection serviceCollection,
        string serviceKey,
        Action<NuGetOptions> configureOptions)
    {
        Guard.Argument(serviceCollection, nameof(serviceCollection)).NotNull();
        Guard.Argument(serviceKey, nameof(serviceKey)).NotNull().NotWhiteSpace();
        Guard.Argument(configureOptions, nameof(configureOptions)).NotNull();

        var options = new NuGetOptions();
        configureOptions(options);

        serviceCollection.AddHttpClient(serviceKey, client =>
        {
            
        });
        
        serviceCollection.AddSingleton(sp => new NuGetPackageService(
            serviceKey,
            options,
            sp.GetRequiredService<IHttpClientFactory>().CreateClient(serviceKey),
            sp.GetService<ILoggerFactory>()));
        
        // Expose services as IPackageService
        serviceCollection.TryAddSingleton(sp => sp
            .GetServices<NuGetPackageService>()
            .Cast<IPackageService>());
        
        // Expose services as IPackageSearchService
        serviceCollection.TryAddSingleton(sp => sp
            .GetServices<NuGetPackageService>()
            .Cast<IPackageSearchService>());

        return serviceCollection;
    }
}