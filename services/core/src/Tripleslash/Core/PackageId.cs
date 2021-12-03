namespace Tripleslash.Core;

/// <summary>
/// Represents a basic package identifier.
/// </summary>
public class PackageId
{
    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="ecosystem">Package ecosystem.</param>
    /// <param name="id">Unique package id.</param>
    /// <param name="version">Semantic package version.</param>
    public PackageId(string ecosystem
        , string id
        , SemVersion version)
    {
        Ecosystem = ecosystem;
        Id = id;
        Version = version;
    }

    /// <summary>
    /// Gets the package ecosystem.
    /// </summary>
    public string Ecosystem { get; }
    
    /// <summary>
    /// Gets the unique package id.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// Gets the semantic version.
    /// </summary>
    public SemVersion Version { get; }

    /// <inheritdoc />
    public override string ToString() => $"{Ecosystem}: {Id} {Version}";

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Ecosystem, Id, Version);
}