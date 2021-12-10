using Dawn;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Tripleslash.DependencyInjection;
using Tripleslash.PackageServices.NuGet;

namespace Tripleslash.PackageServices;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds a collection of NuGet providers
    /// </summary>
    /// <param name="packageBuilder">Package service builder</param>
    /// <param name="configurationSection">Configuration section that contains the providers sections</param>
    /// <returns><see cref="ServiceCollection"/></returns>
    public static ServiceBuilder<IPackageService> AddNuGetProviders(
        this ServiceBuilder<IPackageService> packageBuilder,
        IConfiguration configurationSection)
    {
        Guard.Argument(packageBuilder, nameof(packageBuilder)).NotNull();
        Guard.Argument(configurationSection, nameof(configurationSection)).NotNull();

        var providers = new Dictionary<string, NuGetOptions>();
        configurationSection.Bind(providers);

        foreach (var (key, value) in providers)
        {
            AddNuGetProvider(packageBuilder.ApplicationServices, key, value);
        }

        return packageBuilder;
    }

    /// <summary>
    /// Adds an individual NuGet service provider.
    /// </summary>
    /// <param name="packageBuilder">Package service builder</param>
    /// <param name="serviceKey">Service key</param>
    /// <param name="configureOptions">Action used to configure options</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static ServiceBuilder<IPackageService> AddNuGetProvider(
        this ServiceBuilder<IPackageService> packageBuilder,
        string serviceKey,
        Action<NuGetOptions> configureOptions)
    {
        Guard.Argument(configureOptions, nameof(configureOptions)).NotNull();

        var options = new NuGetOptions();
        configureOptions(options);

        AddNuGetProvider(packageBuilder.ApplicationServices, serviceKey, options);
        
        return packageBuilder;
    }

    private static IServiceCollection AddNuGetProvider(
        IServiceCollection serviceCollection,
        string serviceKey,
        NuGetOptions options)
    {
        Guard.Argument(serviceCollection, nameof(serviceCollection)).NotNull();
        Guard.Argument(serviceKey, nameof(serviceKey)).NotNull().NotWhiteSpace();
        Guard.Argument(options, nameof(options)).NotNull();
        
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