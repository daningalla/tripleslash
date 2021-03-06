using System.Text.Json.Serialization;

namespace Tripleslash.PackageServices.NuGet.ServiceEntities;

/// <summmary>
///   Represents a structural entity in the source JSON document.
///   Path: /
/// </summmary>
public class SearchResultRoot
{
	/// <summmary>
	///   Gets or sets the '@context' value.
	/// </summmary>
	/// <remarks>
	///   Path: /@context
	/// </remarks>
	[JsonPropertyName("@context")]
	public RootContext? Context { get; set; }
	
	/// <summmary>
	///   Gets or sets the 'totalHits' value.
	/// </summmary>
	/// <remarks>
	///   Path: /totalHits
	///   Sample value: '1135'
	/// </remarks>
	[JsonPropertyName("totalHits")]
	public int TotalHits { get; set; }
	
	/// <summmary>
	///   Gets or sets the 'data' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data
	/// </remarks>
	[JsonPropertyName("data")]
	public SearchResultEntry[]? Data { get; set; } = Array.Empty<SearchResultEntry>();
}

/// <summmary>
///   Represents a structural entity in the source JSON document.
///   Path: /data/$(item)
/// </summmary>
public class SearchResultEntry
{
	/// <summmary>
	///   Gets or sets the '@id' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/@id
	///   Sample value: 'https://api.nuget.org/v3/registration5-semver1/serilog/index.json'
	/// </remarks>
	[JsonPropertyName("@id")]
	public string? Id { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the '@type' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/@type
	///   Sample value: 'Package'
	/// </remarks>
	[JsonPropertyName("@type")]
	public string? Type { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'registration' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/registration
	///   Sample value: 'https://api.nuget.org/v3/registration5-semver1/serilog/index.json'
	/// </remarks>
	[JsonPropertyName("registration")]
	public string? Registration { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'id' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/id
	///   Sample value: 'Serilog'
	/// </remarks>
	[JsonPropertyName("id")]
	public string? PackageId { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'version' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/version
	///   Sample value: '2.10.0'
	/// </remarks>
	[JsonPropertyName("version")]
	public string? Version { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'description' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/description
	///   Sample value: 'Simple .NET logging with fully-structured events'
	/// </remarks>
	[JsonPropertyName("description")]
	public string? Description { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'summary' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/summary
	///   Sample value: ''
	/// </remarks>
	[JsonPropertyName("summary")]
	public string? Summary { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'title' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/title
	///   Sample value: 'Serilog'
	/// </remarks>
	[JsonPropertyName("title")]
	public string? Title { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'iconUrl' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/iconUrl
	///   Sample value: 'https://api.nuget.org/v3-flatcontainer/serilog/2.10.0/icon'
	/// </remarks>
	[JsonPropertyName("iconUrl")]
	public string? IconUrl { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'licenseUrl' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/licenseUrl
	///   Sample value: 'https://www.nuget.org/packages/Serilog/2.10.0/license'
	/// </remarks>
	[JsonPropertyName("licenseUrl")]
	public string? LicenseUrl { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'projectUrl' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/projectUrl
	///   Sample value: 'https://serilog.net/'
	/// </remarks>
	[JsonPropertyName("projectUrl")]
	public string? ProjectUrl { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'tags' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/tags
	/// </remarks>
	[JsonPropertyName("tags")]
	public string[]? Tags { get; set; } = Array.Empty<string>();
	
	/// <summmary>
	///   Gets or sets the 'authors' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/authors
	/// </remarks>
	[JsonPropertyName("authors")]
	public string[]? Authors { get; set; } = Array.Empty<string>();
	
	/// <summmary>
	///   Gets or sets the 'owners' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/owners
	/// </remarks>
	[JsonPropertyName("owners")]
	public string[]? Owners { get; set; } = Array.Empty<string>();
	
	/// <summmary>
	///   Gets or sets the 'totalDownloads' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/totalDownloads
	///   Sample value: '392245396'
	/// </remarks>
	[JsonPropertyName("totalDownloads")]
	public int TotalDownloads { get; set; }
	
	/// <summmary>
	///   Gets or sets the 'verified' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/verified
	///   Sample value: 'True'
	/// </remarks>
	[JsonPropertyName("verified")]
	public bool Verified { get; set; }
	
	/// <summmary>
	///   Gets or sets the 'packageTypes' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/packageTypes
	/// </remarks>
	[JsonPropertyName("packageTypes")]
	public SearchItemPackageType[]? PackageTypes { get; set; } = Array.Empty<SearchItemPackageType>();
	
	/// <summmary>
	///   Gets or sets the 'versions' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/versions
	/// </remarks>
	[JsonPropertyName("versions")]
	public SearchItemVersion[]? Versions { get; set; } = Array.Empty<SearchItemVersion>();
}

/// <summmary>
///   Represents a structural entity in the source JSON document.
///   Path: /data/$(item)/packageTypes/$(item)
/// </summmary>
public class SearchItemPackageType
{
	/// <summmary>
	///   Gets or sets the 'name' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/packageTypes/$(item)/name
	///   Sample value: 'Dependency'
	/// </remarks>
	[JsonPropertyName("name")]
	public string? Name { get; set; } = string.Empty;
}

/// <summmary>
///   Represents a structural entity in the source JSON document.
///   Path: /data/$(item)/versions/$(item)
/// </summmary>
public class SearchItemVersion
{
	/// <summmary>
	///   Gets or sets the 'version' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/versions/$(item)/version
	///   Sample value: '0.1.6'
	/// </remarks>
	[JsonPropertyName("version")]
	public string? Version { get; set; } = string.Empty;
	
	/// <summmary>
	///   Gets or sets the 'downloads' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/versions/$(item)/downloads
	///   Sample value: '187060'
	/// </remarks>
	[JsonPropertyName("downloads")]
	public int Downloads { get; set; }
	
	/// <summmary>
	///   Gets or sets the '@id' value.
	/// </summmary>
	/// <remarks>
	///   Path: /data/$(item)/versions/$(item)/@id
	///   Sample value: 'https://api.nuget.org/v3/registration5-semver1/serilog/0.1.6.json'
	/// </remarks>
	[JsonPropertyName("@id")]
	public string? Id { get; set; } = string.Empty;
}
