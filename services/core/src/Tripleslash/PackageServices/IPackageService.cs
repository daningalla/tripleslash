namespace Tripleslash.PackageServices;

/// <summary>
/// Base interface for package services.
/// </summary>
public interface IPackageService
{
    /// <summary>
    /// Determines whether a particular ecosystem is supported.
    /// </summary>
    /// <param name="ecosystem">Ecosystem</param>
    /// <returns><c>true</c> if the service supports searches for the given ecosystem.</returns>
    bool SupportsEcosystem(string ecosystem);
    
    /// <summary>
    /// Gets a source identifier (e.g. nuget.org)
    /// </summary>
    string SourceId { get; }
    
    /// <summary>
    /// Gets a description for the service implementation.
    /// </summary>
    string Description { get; }
}