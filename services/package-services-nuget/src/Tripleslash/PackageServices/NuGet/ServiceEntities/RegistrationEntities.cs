using System.Text.Json.Serialization;

namespace Tripleslash.PackageServices.NuGet.ServiceEntities;

/// <summmary>
///   Represents a structural entity in the source JSON document.
///   Path: /
/// </summmary>
public class RegistrationRoot
{
	/// <summmary>
	///   Gets or sets the '@id' value.
	/// </summmary>
	/// <remarks>
	///   Path: /@id
	///   Sample value: 'https://api.nuget.org/v3/registration5-gz-semver1/serilog/page/2.8.1-dev-01054/2.11.0-dev-01380.json'
	/// </remarks>
	[JsonPropertyName("@id")]
	public string? Id { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the '@type' value.
	/// </summmary>
	/// <remarks>
	///   Path: /@type
	///   Sample value: 'catalog:CatalogPage'
	/// </remarks>
	[JsonPropertyName("@type")]
	public string? Type { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'commitId' value.
	/// </summmary>
	/// <remarks>
	///   Path: /commitId
	///   Sample value: '94e9276c-050a-40ba-9e69-a522fe9666b8'
	/// </remarks>
	[JsonPropertyName("commitId")]
	public string? CommitId { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'commitTimeStamp' value.
	/// </summmary>
	/// <remarks>
	///   Path: /commitTimeStamp
	///   Sample value: '2022-01-23T22:56:06.1332946+00:00'
	/// </remarks>
	[JsonPropertyName("commitTimeStamp")]
	public string? CommitTimeStamp { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'count' value.
	/// </summmary>
	/// <remarks>
	///   Path: /count
	///   Sample value: '58'
	/// </remarks>
	[JsonPropertyName("count")]
	public int Count { get; set; }
	
	/// <summmary>
	///   Gets or sets the 'items' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items
	/// </remarks>
	[JsonPropertyName("items")]
	public RegistrationItem[]? Items { get; set; } = Array.Empty<RegistrationItem>();
	
	/// <summmary>
	///   Gets or sets the 'parent' value.
	/// </summmary>
	/// <remarks>
	///   Path: /parent
	///   Sample value: 'https://api.nuget.org/v3/registration5-gz-semver1/serilog/index.json'
	/// </remarks>
	[JsonPropertyName("parent")]
	public string? Parent { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'lower' value.
	/// </summmary>
	/// <remarks>
	///   Path: /lower
	///   Sample value: '2.8.1-dev-01054'
	/// </remarks>
	[JsonPropertyName("lower")]
	public string? Lower { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'upper' value.
	/// </summmary>
	/// <remarks>
	///   Path: /upper
	///   Sample value: '2.11.0-dev-01380'
	/// </remarks>
	[JsonPropertyName("upper")]
	public string? Upper { get; set; } = string.Empty;
}

/// <summmary>
///   Represents a structural entity in the source JSON document.
///   Path: /items/$(item)
/// </summmary>
public class RegistrationItem
{
	/// <summmary>
	///   Gets or sets the '@id' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/@id
	///   Sample value: 'https://api.nuget.org/v3/registration5-gz-semver1/serilog/2.8.1-dev-01054.json'
	/// </remarks>
	[JsonPropertyName("@id")]
	public string? Id { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the '@type' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/@type
	///   Sample value: 'Package'
	/// </remarks>
	[JsonPropertyName("@type")]
	public string? Type { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'commitId' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/commitId
	///   Sample value: '2374943e-e91b-48dc-8729-85c42dd84eba'
	/// </remarks>
	[JsonPropertyName("commitId")]
	public string? CommitId { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'commitTimeStamp' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/commitTimeStamp
	///   Sample value: '2020-02-08T11:22:26.3801305+00:00'
	/// </remarks>
	[JsonPropertyName("commitTimeStamp")]
	public string? CommitTimeStamp { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'catalogEntry' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry
	/// </remarks>
	[JsonPropertyName("catalogEntry")]
	public RegistrationCatalogEntry? CatalogEntry { get; set; }
	
	/// <summmary>
	///   Gets or sets the 'packageContent' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/packageContent
	///   Sample value: 'https://api.nuget.org/v3-flatcontainer/serilog/2.8.1-dev-01054/serilog.2.8.1-dev-01054.nupkg'
	/// </remarks>
	[JsonPropertyName("packageContent")]
	public string? PackageContent { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'registration' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/registration
	///   Sample value: 'https://api.nuget.org/v3/registration5-gz-semver1/serilog/index.json'
	/// </remarks>
	[JsonPropertyName("registration")]
	public string? Registration { get; set; } = string.Empty;
}

/// <summmary>
///   Represents a structural entity in the source JSON document.
///   Path: /items/$(item)/catalogEntry
/// </summmary>
public class RegistrationCatalogEntry
{
	/// <summmary>
	///   Gets or sets the '@id' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/@id
	///   Sample value: 'https://api.nuget.org/v3/catalog0/data/2019.05.02.21.53.02/serilog.2.8.1-dev-01054.json'
	/// </remarks>
	[JsonPropertyName("@id")]
	public string? Id { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the '@type' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/@type
	///   Sample value: 'PackageDetails'
	/// </remarks>
	[JsonPropertyName("@type")]
	public string? Type { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'authors' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/authors
	///   Sample value: 'Serilog Contributors'
	/// </remarks>
	[JsonPropertyName("authors")]
	public string? Authors { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'dependencyGroups' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/dependencyGroups
	/// </remarks>
	[JsonPropertyName("dependencyGroups")]
	public RegistrationDependencyGroup[]? DependencyGroups { get; set; } = Array.Empty<RegistrationDependencyGroup>();
	
	/// <summmary>
	///   Gets or sets the 'description' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/description
	///   Sample value: 'Simple .NET logging with fully-structured events'
	/// </remarks>
	[JsonPropertyName("description")]
	public string? Description { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'iconUrl' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/iconUrl
	///   Sample value: 'https://api.nuget.org/v3-flatcontainer/serilog/2.8.1-dev-01054/icon'
	/// </remarks>
	[JsonPropertyName("iconUrl")]
	public string? IconUrl { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'id' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/id
	///   Sample value: 'Serilog'
	/// </remarks>
	[JsonPropertyName("id")]
	public string? PackageId { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'language' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/language
	///   Sample value: ''
	/// </remarks>
	[JsonPropertyName("language")]
	public string? Language { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'licenseExpression' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/licenseExpression
	///   Sample value: ''
	/// </remarks>
	[JsonPropertyName("licenseExpression")]
	public string? LicenseExpression { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'licenseUrl' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/licenseUrl
	///   Sample value: 'https://www.apache.org/licenses/LICENSE-2.0'
	/// </remarks>
	[JsonPropertyName("licenseUrl")]
	public string? LicenseUrl { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'listed' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/listed
	///   Sample value: 'True'
	/// </remarks>
	[JsonPropertyName("listed")]
	public bool Listed { get; set; }
	
	/// <summmary>
	///   Gets or sets the 'minClientVersion' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/minClientVersion
	///   Sample value: ''
	/// </remarks>
	[JsonPropertyName("minClientVersion")]
	public string? MinClientVersion { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'packageContent' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/packageContent
	///   Sample value: 'https://api.nuget.org/v3-flatcontainer/serilog/2.8.1-dev-01054/serilog.2.8.1-dev-01054.nupkg'
	/// </remarks>
	[JsonPropertyName("packageContent")]
	public string? PackageContent { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'projectUrl' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/projectUrl
	///   Sample value: 'https://github.com/serilog/serilog'
	/// </remarks>
	[JsonPropertyName("projectUrl")]
	public string? ProjectUrl { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'published' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/published
	///   Sample value: '2019-05-02T21:48:50.433+00:00'
	/// </remarks>
	[JsonPropertyName("published")]
	public string? Published { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'requireLicenseAcceptance' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/requireLicenseAcceptance
	///   Sample value: 'False'
	/// </remarks>
	[JsonPropertyName("requireLicenseAcceptance")]
	public bool RequireLicenseAcceptance { get; set; }
	
	/// <summmary>
	///   Gets or sets the 'summary' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/summary
	///   Sample value: ''
	/// </remarks>
	[JsonPropertyName("summary")]
	public string? Summary { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'tags' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/tags
	/// </remarks>
	[JsonPropertyName("tags")]
	public string[]? Tags { get; set; } = Array.Empty<string>();
	
	/// <summmary>
	///   Gets or sets the 'title' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/title
	///   Sample value: ''
	/// </remarks>
	[JsonPropertyName("title")]
	public string? Title { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'version' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/version
	///   Sample value: '2.8.1-dev-01054'
	/// </remarks>
	[JsonPropertyName("version")]
	public string? Version { get; set; } = string.Empty;
}

/// <summmary>
///   Represents a structural entity in the source JSON document.
///   Path: /items/$(item)/catalogEntry/dependencyGroups/$(item)
/// </summmary>
public class RegistrationDependencyGroup
{
	/// <summmary>
	///   Gets or sets the '@id' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/dependencyGroups/$(item)/@id
	///   Sample value: 'https://api.nuget.org/v3/catalog0/data/2019.05.02.21.53.02/serilog.2.8.1-dev-01054.json#dependencygroup/.netframework4.5'
	/// </remarks>
	[JsonPropertyName("@id")]
	public string? Id { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the '@type' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/dependencyGroups/$(item)/@type
	///   Sample value: 'PackageDependencyGroup'
	/// </remarks>
	[JsonPropertyName("@type")]
	public string? Type { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'targetFramework' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/dependencyGroups/$(item)/targetFramework
	///   Sample value: '.NETFramework4.5'
	/// </remarks>
	[JsonPropertyName("targetFramework")]
	public string? TargetFramework { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'dependencies' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/dependencyGroups/$(item)/dependencies
	/// </remarks>
	[JsonPropertyName("dependencies")]
	public RegistrationDependency[]? Dependencies { get; set; } = Array.Empty<RegistrationDependency>();
}

/// <summmary>
///   Represents a structural entity in the source JSON document.
///   Path: /items/$(item)/catalogEntry/dependencyGroups/$(item)/dependencies/$(item)
/// </summmary>
public class RegistrationDependency
{
	/// <summmary>
	///   Gets or sets the '@id' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/dependencyGroups/$(item)/dependencies/$(item)/@id
	///   Sample value: 'https://api.nuget.org/v3/catalog0/data/2019.05.02.21.53.02/serilog.2.8.1-dev-01054.json#dependencygroup/.netstandard1.0/netstandard.library'
	/// </remarks>
	[JsonPropertyName("@id")]
	public string? Id { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the '@type' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/dependencyGroups/$(item)/dependencies/$(item)/@type
	///   Sample value: 'PackageDependency'
	/// </remarks>
	[JsonPropertyName("@type")]
	public string? Type { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'id' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/dependencyGroups/$(item)/dependencies/$(item)/id
	///   Sample value: 'NETStandard.Library'
	/// </remarks>
	[JsonPropertyName("id")]
	public string? PackageId { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'range' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/dependencyGroups/$(item)/dependencies/$(item)/range
	///   Sample value: '[1.6.1, )'
	/// </remarks>
	[JsonPropertyName("range")]
	public string? Range { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'registration' value.
	/// </summmary>
	/// <remarks>
	///   Path: /items/$(item)/catalogEntry/dependencyGroups/$(item)/dependencies/$(item)/registration
	///   Sample value: 'https://api.nuget.org/v3/registration5-gz-semver1/netstandard.library/index.json'
	/// </remarks>
	[JsonPropertyName("registration")]
	public string? Registration { get; set; } = string.Empty;
}