//    Copyright 2022 Tripleslash contributors
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using System.Buffers;
using System.Text;
using System.Text.RegularExpressions;
using Dawn;
using Tripleslash.Core.Utilities;

namespace Tripleslash.Core.PackageServices;

/// <summary>
/// Represents an immutable semantic version (implements https://semver.org/ 2.0 spec)
/// </summary>
public sealed class SemVersion : IComparable<SemVersion>, IEquatable<SemVersion>
{
    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="major">Major version</param>
    /// <param name="minor">Minor version</param>
    /// <param name="patch">Patch version</param>
    /// <param name="preRelease">Optional pre-release label (exclude '-')</param>
    /// <param name="metadata">Optional build metadata label (exclude '+')</param>
    public SemVersion(
        int major,
        int minor,
        int patch = 0,
        string? preRelease = null,
        string? metadata = null)
    {
        Guard.Argument(major, nameof(major)).GreaterThan(-1);
        Guard.Argument(minor, nameof(minor)).GreaterThan(-1);
        Guard.Argument(patch, nameof(patch)).GreaterThan(-1);
        
        Major = major;
        Minor = minor;
        Patch = patch;
        PreRelease = preRelease.NullIfWhiteSpace();
        Metadata = metadata.NullIfWhiteSpace();
    }

    /// <summary>
    /// Gets the major version.
    /// </summary>
    public int Major { get; }

    /// <summary>
    /// Gets the minor version.
    /// </summary>
    public int Minor { get; }

    /// <summary>
    /// Gets the patch version.
    /// </summary>
    public int Patch { get; }

    /// <summary>
    /// Gets the optional pre-release label.
    /// </summary>
    public string? PreRelease { get; }

    /// <summary>
    /// Gets the optional build metadata label.
    /// </summary>
    public string? Metadata { get; }

    /// <inheritdoc />
    public int CompareTo(SemVersion? other)
    {
        if (other == null)
            return 1;

        var intComparer = Comparer<int>.Default;
        int result;

        // Rule 11.1
        if ((result = intComparer.Compare(Major, other.Major)) != 0)
            return result;

        // Rule 11.1
        if ((result = intComparer.Compare(Minor, other.Minor)) != 0)
            return result;

        // Rule 11.1
        if ((result = intComparer.Compare(Patch, other.Patch)) != 0)
            return result;

        // Rule 11.3
        if (PreRelease == other.PreRelease)
            return 0;

        if (PreRelease != null)
            return -1;

        if (other.PreRelease != null)
            return 1;

        var myPreSr = new SequenceReader<char>(new ReadOnlySequence<char>(PreRelease.AsMemory()));
        var otherPreSr = new SequenceReader<char>(new ReadOnlySequence<char>(other.PreRelease.AsMemory()));

        while (true)
        {
            var hasMySpan = myPreSr.TryReadTo(out ReadOnlySpan<char> mySpan, '.');
            var hasOtherSpan = otherPreSr.TryReadTo(out ReadOnlySpan<char> otherSpan, '.');
            
            // Rule 11.4.4
            if (hasMySpan && !hasOtherSpan)
                return 1;

            if (!hasMySpan && hasOtherSpan)
                return -1;

            var mySpanIsNumeric = int.TryParse(mySpan, out var myNumeric);
            var otherSpanIsNumeric = int.TryParse(otherSpan, out var otherNumeric);
            
            // Rule 11.4.1 + 11.4.3
            if (mySpanIsNumeric && !otherSpanIsNumeric)
                return -1;

            if (!mySpanIsNumeric && otherSpanIsNumeric)
                return 1;

            if (mySpanIsNumeric && otherSpanIsNumeric)
            {
                if ((result = intComparer.Compare(myNumeric, otherNumeric)) != 0)
                    return result;

                continue;
            }
            
            // Rule 11.4.2
            if ((result = mySpan.CompareTo(otherSpan, StringComparison.Ordinal)) != 0)
                return result;
        }
    }

    /// <inheritdoc />
    public bool Equals(SemVersion? other) => CompareTo(other) == 0;

    /// <inheritdoc />
    public override bool Equals(object? obj) => CompareTo(obj as SemVersion) == 0;

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(
            Major,
            Minor,
            Patch,
            PreRelease,
            Metadata);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        var sb = new StringBuilder($"{Major}.{Minor}.{Patch}");

        if (!string.IsNullOrWhiteSpace(PreRelease))
            sb.Append($"-{PreRelease}");

        if (!string.IsNullOrWhiteSpace(Metadata))
            sb.Append($"+{Metadata}");

        return sb.ToString();
    }

    /// <summary>
    /// Parses a semantic version string.
    /// </summary>
    /// <param name="s">String to parse</param>
    /// <returns><see cref="SemVersion"/></returns>
    /// <exception cref="FormatException"><paramref name="s"/> is an invalid semantic version string</exception>
    /// <exception cref="ArgumentNullException"><paramref name="s"/> is null</exception>
    /// <exception cref="ArgumentException"><paramref name="s"/> is whiespace</exception>
    public static SemVersion Parse(string s)
    {
        return TryParse(s, out var semVersion)
            ? semVersion!
            : throw new FormatException($"Value '{s}' is not a valid semantic version");
    }

    /// <summary>
    /// Tries to parse a semantic version string.
    /// </summary>
    /// <param name="s">String to parse</param>
    /// <param name="semVersion">When the method returns successfully, the parse <see cref="SemVersion"/></param>
    /// <returns><c>true</c> if the input was parsed, <c>false</c> otherwise</returns>
    public static bool TryParse(string s, out SemVersion? semVersion)
    {
        Guard.Argument(s, nameof(s)).NotNull().NotWhiteSpace();

        semVersion = null;
        
        var regexMatch = Regex.Match(
            s, 
            @"^(?<major>0|[1-9]\d*)\.(?<minor>0|[1-9]\d*)\.(?<patch>0|[1-9]\d*)" 
            + @"(?:-(?<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?"  
            + @"(?:\+(?<buildmetadata>[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$");

        if (!regexMatch.TryGetGroupValue("major", int.Parse, out var major))
            return false;

        if (!regexMatch.TryGetGroupValue("minor", int.Parse, out var minor))
            return false;

        if (!regexMatch.TryGetGroupValue("patch", int.Parse, out var patch))
            return false;

        semVersion = new SemVersion(
            major,
            minor,
            patch,
            regexMatch.Groups["prerelease"].Value,
            regexMatch.Groups["buildmetadata"].Value);

        return true;
    }

    private static readonly IComparer<SemVersion> StaticComparer = Comparer<SemVersion>.Default;

    /// <summary>
    /// Compares two instances for equality
    /// </summary>
    /// <param name="x">Value to compare to <paramref name="x"/></param>
    /// <param name="y">Value to compare to <paramref name="y"/></param>
    /// <returns>The comparison result</returns>
    public static bool operator ==(SemVersion? x, SemVersion? y) => StaticComparer.Compare(x, y) == 0;
    
    /// <summary>
    /// Compares two instances for inequality
    /// </summary>
    /// <param name="x">Value to compare to <paramref name="x"/></param>
    /// <param name="y">Value to compare to <paramref name="y"/></param>
    /// <returns>The comparison result</returns>
    public static bool operator !=(SemVersion? x, SemVersion? y) => StaticComparer.Compare(x, y) != 0;
    
    /// <summary>
    /// Determines the sortable relation between two instances
    /// </summary>
    /// <param name="x">Value to compare to <paramref name="x"/></param>
    /// <param name="y">Value to compare to <paramref name="y"/></param>
    /// <returns>The comparison result</returns>
    public static bool operator <(SemVersion? x, SemVersion? y) => StaticComparer.Compare(x, y) < 0;
    
    /// <summary>
    /// Determines the sortable relation between two instances
    /// </summary>
    /// <param name="x">Value to compare to <paramref name="x"/></param>
    /// <param name="y">Value to compare to <paramref name="y"/></param>
    /// <returns>The comparison result</returns>
    public static bool operator <=(SemVersion? x, SemVersion? y) => StaticComparer.Compare(x, y) <= 0;
    
    /// <summary>
    /// Determines the sortable relation between two instances
    /// </summary>
    /// <param name="x">Value to compare to <paramref name="x"/></param>
    /// <param name="y">Value to compare to <paramref name="y"/></param>
    /// <returns>The comparison result</returns>
    public static bool operator >(SemVersion? x, SemVersion? y) => StaticComparer.Compare(x, y) > 0;
    
    /// <summary>
    /// Determines the sortable relation between two instances
    /// </summary>
    /// <param name="x">Value to compare to <paramref name="x"/></param>
    /// <param name="y">Value to compare to <paramref name="y"/></param>
    /// <returns>The comparison result</returns>
    public static bool operator >=(SemVersion? x, SemVersion? y) => StaticComparer.Compare(x, y) >= 0;
}