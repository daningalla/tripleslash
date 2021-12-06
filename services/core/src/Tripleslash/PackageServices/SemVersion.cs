// Copyright (c) 2021 Tripleslash project contributors
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Text.RegularExpressions;
using Dawn;
using Tripleslash.Infrastructure;

namespace Tripleslash.PackageServices;

/// <summary>
/// Represents a semver 2.0 value.
/// </summary>
public sealed class SemVersion : IComparable<SemVersion>, IEquatable<SemVersion>
{
    /// <summary>
    /// Defines a comparer for the version, complies with https://semver.org/
    /// </summary>
    private class ComparerImpl : IComparer<SemVersion>
    {
        internal static readonly ComparerImpl Default = new();
        
        private static readonly IComparer<byte> ByteComparer = Comparer<byte>.Default;
        private static readonly IComparer<uint> UIntComparer = Comparer<uint>.Default;

        /// <inheritdoc />
        public int Compare(SemVersion? x, SemVersion? y)
        {
            var uintComparer = UIntComparer;
            var byteComparer = ByteComparer;

            int c;

            if (ReferenceEquals(x, y))
                return 0;

            if (ReferenceEquals(x, null) && ReferenceEquals(y, null))
                return 0;

            if (ReferenceEquals(x, null))
                return -1;

            if (ReferenceEquals(y, null))
                return 1;

            if ((c = byteComparer.Compare(x.Major, y.Major)) != 0)
                return c;

            if ((c = uintComparer.Compare(x.Minor, y.Minor)) != 0)
                return c;

            if ((c = uintComparer.Compare(x.Patch, y.Patch)) != 0)
                return c;

            if (x.Prerelease == y.Prerelease)
                return 0;

            if (x.Prerelease == null)
                return 1;

            if (y.Prerelease == null)
                return -1;

            var identifiersX = x.Prerelease.PartitionBy('.');
            var identifiersY = y.Prerelease.PartitionBy('.');

            for (;;)
            {
                var hasNextX = identifiersX.MoveNext();
                var hasNextY = identifiersY.MoveNext();

                // Rule 11.4
                if (!hasNextX && !hasNextY)
                    return 0;

                // Rule 11.4.4
                if (!hasNextX)
                    return -1;

                // Rule 11.4.4
                if (!hasNextY)
                    return 1;

                var idX = identifiersX.Current;
                var idY = identifiersY.Current;
                
                // Rule 11.4.1
                var xIsNumeric = uint.TryParse(idX, out var numericX);
                var yIsNumeric = uint.TryParse(idY, out var numericY);

                if (xIsNumeric && yIsNumeric && (c = uintComparer.Compare(numericX, numericY)) != 0)
                    return c;
                
                // Rule 11.4.3
                if (xIsNumeric)
                    return -1;

                if (yIsNumeric)
                    return 1;
                
                // Rule 11.4.2
                if ((c = idX.CompareTo(idY, StringComparison.Ordinal)) != 0)
                    return c;
            }
        }
    }

    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="major">Major version</param>
    /// <param name="minor">Minor version</param>
    /// <param name="patch">Path version</param>
    /// <param name="prerelease">Optional pre-release label.</param>
    /// <param name="buildMetadata">Optional build metadata</param>
    public SemVersion(byte major
        , uint minor
        , uint patch
        , string? prerelease = null
        , string? buildMetadata = null)
    {
        Major = major;
        Minor = minor;
        Patch = patch;
        Prerelease = prerelease;
        BuildMetadata = buildMetadata;
    }

    /// <summary>
    /// Gets the major version
    /// </summary>
    public byte Major { get; }

    /// <summary>
    /// Gets the minor version
    /// </summary>
    public uint Minor { get; }

    /// <summary>
    /// Gets the patch version
    /// </summary>
    public uint Patch { get; }

    /// <summary>
    /// Gets the prerelease version
    /// </summary>
    public string? Prerelease { get; }

    public string? BuildMetadata { get; }

    /// <summary>
    /// Parses a semantic version string.
    /// </summary>
    /// <param name="s">String to parse</param>
    /// <returns><see cref="SemVersion"/></returns>
    /// <exception cref="FormatException"><paramref name="s"/> is not a valid semantic version string.</exception>
    public static SemVersion Parse(string s)
    {
        Guard.Argument(s, nameof(s)).NotNull().NotWhiteSpace();
        
        return TryParse(s, out var semver)
            ? semver!
            : throw new FormatException($"Invalid semver string '{s}' (see https://semver.org/)");
    }

    /// <summary>
    /// Implicitly converts a string to semantic version.
    /// </summary>
    /// <param name="s">String to parse</param>
    /// <returns><see cref="SemVersion"/></returns>
    /// <exception cref="FormatException"><paramref name="s"/> is not a valid semantic version string.</exception>
    public static implicit operator SemVersion(string s) => Parse(s);

    /// <summary>
    /// Attempts to parse a version string.
    /// </summary>
    /// <param name="s">String to parse</param>
    /// <param name="semver">A <see cref="SemVersion"/> or <c>undefined</c> if the method was not successful.</param>
    /// <returns><c>true</c> if <paramref name="s"/> is a valid semantic version string.</returns>
    public static bool TryParse(string s, out SemVersion? semver)
    {
        Guard.Argument(s, nameof(s)).NotNull();
        
        semver = default;
        const string pattern = @"^(?<major>\d+).(?<minor>\d+).(?<patch>\d+)(-(?<pre>[\da-zA-Z\.-]+))?(\+(?<build>[\da-zA-Z\.-]+))?";
        
        var match = Regex.Match(s, pattern);

        if (!match.Success)
            return false;

        semver = new(byte.Parse(match.Groups["major"].Value)
            , uint.Parse(match.Groups["minor"].Value)
            , uint.Parse(match.Groups["patch"].Value)
            , StringUtilities.NullIfWhitespace(match.Groups["pre"].Value)
            , StringUtilities.NullIfWhitespace(match.Groups["build"].Value));

        return true;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return true switch
        {
            true when BuildMetadata == null && Prerelease == null => $"{Major}.{Minor}.{Patch}",
            true when BuildMetadata != null && Prerelease != null =>
                $"{Major}.{Minor}.{Patch}-{Prerelease}+{BuildMetadata}",
            true when Prerelease != null => $"{Major}.{Minor}.{Patch}-{Prerelease}",
            _ => $"{Major}.{Minor}.{Patch}+{BuildMetadata}"
        };
    }

    /// <inheritdoc />
    public int CompareTo(SemVersion? other) => ComparerImpl.Default.Compare(this, other);

    /// <inheritdoc />
    public bool Equals(SemVersion? other) => CompareTo(other) == 0;

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Major, Minor, Patch, Prerelease);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is SemVersion other && Equals(other);

    /// <summary>
    /// Performs an equality comparison of two <see cref="SemVersion"/> instances.
    /// </summary>
    /// <param name="x">The first instance to compare</param>
    /// <param name="y">The second instance to compare</param>
    /// <returns><c>true</c> if <paramref name="x"/> is equal to <paramref name="y"/></returns>
    /// <remarks>
    /// This method implements the rules for v2 of the semantic version specification as defined
    /// at https://semver.org/
    /// </remarks>
    public static bool operator ==(SemVersion? x, SemVersion? y) => ComparerImpl.Default.Compare(x, y) == 0;
    
    /// <summary>
    /// Performs an equality comparison of two <see cref="SemVersion"/> instances.
    /// </summary>
    /// <param name="x">The first instance to compare</param>
    /// <param name="y">The second instance to compare</param>
    /// <returns><c>true</c> if <paramref name="x"/> is not equal to <paramref name="y"/></returns>
    /// <remarks>
    /// This method implements the rules for v2 of the semantic version specification as defined
    /// at https://semver.org/
    /// </remarks>
    public static bool operator !=(SemVersion? x, SemVersion? y) => ComparerImpl.Default.Compare(x, y) != 0;
    
    /// <summary>
    /// Performs a relational value comparison of two <see cref="SemVersion"/> instances.
    /// </summary>
    /// <param name="x">The first instance to compare</param>
    /// <param name="y">The second instance to compare</param>
    /// <returns><c>true</c> if <paramref name="x"/> is less than <paramref name="y"/></returns>
    /// <remarks>
    /// This method implements the rules for v2 of the semantic version specification as defined
    /// at https://semver.org/
    /// </remarks>
    public static bool operator <(SemVersion? x, SemVersion? y) => ComparerImpl.Default.Compare(x, y) < 0;
    
    /// <summary>
    /// Performs a relational value comparison of two <see cref="SemVersion"/> instances.
    /// </summary>
    /// <param name="x">The first instance to compare</param>
    /// <param name="y">The second instance to compare</param>
    /// <returns><c>true</c> if <paramref name="x"/> is less than or equal to <paramref name="y"/></returns>
    /// <remarks>
    /// This method implements the rules for v2 of the semantic version specification as defined
    /// at https://semver.org/
    /// </remarks>
    public static bool operator <=(SemVersion? x, SemVersion? y) => ComparerImpl.Default.Compare(x, y) <= 0;
    
    /// <summary>
    /// Performs a relational value comparison of two <see cref="SemVersion"/> instances.
    /// </summary>
    /// <param name="x">The first instance to compare</param>
    /// <param name="y">The second instance to compare</param>
    /// <returns><c>true</c> if <paramref name="x"/> is greater than <paramref name="y"/></returns>
    /// <remarks>
    /// This method implements the rules for v2 of the semantic version specification as defined
    /// at https://semver.org/
    /// </remarks>
    public static bool operator >(SemVersion? x, SemVersion? y) => ComparerImpl.Default.Compare(x, y) > 0;
    
    /// <summary>
    /// Performs a relational value comparison of two <see cref="SemVersion"/> instances.
    /// </summary>
    /// <param name="x">The first instance to compare</param>
    /// <param name="y">The second instance to compare</param>
    /// <returns><c>true</c> if <paramref name="x"/> is greater than or equal to <paramref name="y"/></returns>
    /// <remarks>
    /// This method implements the rules for v2 of the semantic version specification as defined
    /// at https://semver.org/
    /// </remarks>
    public static bool operator >=(SemVersion? x, SemVersion? y) => ComparerImpl.Default.Compare(x, y) >= 0;
}