// Copyright (c) 2021 Tripleslash contributors
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

namespace Tripleslash.IO;

/// <summary>
/// Represents a path that is split between a base location and a
/// file system globbing pattern.
/// </summary>
public readonly struct GlobPattern
{
    private GlobPattern(string basePath, string pattern)
    {
        ;
        BasePath = basePath;
        Pattern = pattern;
    }

    /// <summary>
    /// Parses a path.
    /// </summary>
    /// <param name="path">The path to parse</param>
    /// <param name="value">When the method returns, the <see cref="GlobPattern"/> value
    /// or default.</param>
    /// <returns>A <see cref="GlobPattern"/> value</returns>
    public static bool TryParse(string path, out GlobPattern value)
    {
        Guard.Argument(path, nameof(path)).NotNull();
        
        var match = Regex.Match(path, @"^([^*]+)?(.+)?");
        var stemMatched = match.Groups[2].Success;
        
        value = stemMatched
            ? new(match.Groups[1].Value, match.Groups[2].Value)
            : default;

        return stemMatched;
    }

    /// <summary>
    /// Gets the base part of the path
    /// </summary>
    /// <remarks>
    /// May return the entire original path if there are no glob patterns found.
    /// </remarks>
    public string BasePath { get; }

    /// <summary>
    /// Gets the stem.
    /// </summary>
    public string Pattern { get; }

    /// <summary>
    /// Gets whether 
    /// </summary>
    public bool IsMatch => Pattern.Length > 0;

    /// <inheritdoc />
    public override string ToString() => Path.Combine(BasePath, Pattern);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(BasePath, Pattern);
}