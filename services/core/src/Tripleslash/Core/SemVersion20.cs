// Tripleslash Software
// Copyright (C) 2021 Dan Ingalla
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

using System.Runtime.InteropServices.ComTypes;

namespace Tripleslash.Core;

/// <summary>
/// Represents a semver 2.0 value.
/// </summary>
public sealed class SemVersion20
{
    private class Comparer : IComparer<SemVersion20>
    {
        /// <inheritdoc />
        public int Compare(SemVersion20? x, SemVersion20? y)
        {
            var c = 0;

            if (ReferenceEquals(x, y))
                return 0;

            if (x == null && y == null)
                return 0;

            if (x == null)
                return -1;

            if (y == null)
                return 1;

            if ((c = Comparer<byte>.Default.Compare(x.Major, y.Major)) != 0)
                return c;

            if ((c = Comparer<uint>.Default.Compare(x.Minor, y.Minor)) != 0)
                return c;

            if ((c = Comparer<uint>.Default.Compare(x.Patch, y.Patch)) != 0)
                return c;

            if (x.Prerelease == y.Prerelease)
                return 0;

            if (x.Prerelease == null)
                return 1;

            if (y.Prerelease == null)
                return -1;

            var componentsOfX = x.Prerelease.Split('.');
            var componentsOfY = y.Prerelease.Split('.');

            for (var d = 0; d < Math.Min(componentsOfX.Length, componentsOfY.Length); d++)
            {
                
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
    public SemVersion20(byte major, uint minor, uint patch, string? prerelease = null)
    {
        Major = major;
        Minor = minor;
        Patch = patch;
        Prerelease = prerelease;
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

    /// <inheritdoc />
    public override string ToString()
    {
        return Prerelease != null
            ? $"{Major}.{Minor}.{Patch}{Prerelease}"
            : $"{Major}.{Minor}.{Patch}";
    }

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Major, Minor, Patch, Prerelease);
}