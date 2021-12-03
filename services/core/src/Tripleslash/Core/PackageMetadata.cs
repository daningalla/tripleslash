namespace Tripleslash.Core;

/// <summary>
/// Represents basic information about a package.
/// </summary>
public class PackageMetadata
{
    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="packageId">Unique package id.</param>
    public PackageMetadata(
        PackageId packageId
        , string sourceProvider
        , string title
        , string description
        , string authors
        , string owners
        , string[] tags
        , string projectUrl
        , string companySiteUrl
        , string sourceUrl
        , string licenseType
        , string licenseVersion, 
        , IReadOnlyCollection<PackageGroup> dependencyGroups)
    {
        PackageId = packageId;
        SourceProvider = sourceProvider;
        Title = title;
        Description = description;
        Authors = authors;
        Owners = owners;
        Tags = tags;
        ProjectUrl = projectUrl;
        CompanySiteUrl = companySiteUrl;
        SourceUrl = sourceUrl;
        LicenseType = licenseType;
        LicenseVersion = licenseVersion;
        DependencyGroups = dependencyGroups;
    }

    /// <summary>
    /// Gets the package id
    /// </summary>
    public PackageId PackageId { get; }

    /// <summary>
    /// Gets the source provider
    /// </summary>
    public string SourceProvider { get; }
    
    /// <summary>
    /// Gets the title
    /// </summary>
    public string Title { get; }
    
    /// <summary>
    /// Gets the package description
    /// </summary>
    public string Description { get; }
    
    /// <summary>
    /// Gets the authors
    /// </summary>
    public string Authors { get; }
    
    /// <summary>
    /// Gets the package owners
    /// </summary>
    public string Owners { get; }
    
    /// <summary>
    /// Gets package tags
    /// </summary>
    public string[] Tags { get; }
    
    /// <summary>
    /// Gets the project website url
    /// </summary>
    public string ProjectUrl { get; }
    
    /// <summary>
    /// Gets the company site url
    /// </summary>
    public string CompanySiteUrl { get; }
    
    /// <summary>
    /// Gets the source repository url
    /// </summary>
    public string SourceUrl { get; }
    
    /// <summary>
    /// Gets the license type
    /// </summary>
    public string LicenseType { get; }
    
    /// <summary>
    /// Gets the license version
    /// </summary>
    public string LicenseVersion { get; }
    
    /// <summary>
    /// Gets the dependency groups
    /// </summary>
    public IReadOnlyCollection<PackageGroup> DependencyGroups { get; }
}