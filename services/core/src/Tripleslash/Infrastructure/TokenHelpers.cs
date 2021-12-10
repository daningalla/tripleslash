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

namespace Tripleslash.Infrastructure;

/// <summary>
/// Defines methods for tokens
/// </summary>
public static class TokenHelpers
{
    private const string TokenPattern = @"(?<!\\)\$\((\w+)\)";
    
    /// <summary>
    /// Replaces tokens in the given string with environment variables.
    /// </summary>
    /// <param name="arg">String argument to parse.</param>
    /// <returns>String that contains the replaced tokens</returns>
    public static string ReplaceEnvironmentVariables(string arg)
    {
        Guard.Argument(arg, nameof(arg)).NotNull();
        
        return Regex.Replace(arg, TokenPattern, match => Environment.GetEnvironmentVariable(
            match.Groups[1].Value) ?? arg);
    }

    /// <summary>
    /// Replaces tokens in the given string with special folder locations.
    /// </summary>
    /// <param name="arg">String argument to parse.</param>
    /// <returns>String that contains the replaced tokens</returns>
    public static string ReplaceSpecialFolderPaths(string arg)
    {
        Guard.Argument(arg, nameof(arg)).NotNull();

        return Regex.Replace(arg, TokenPattern, match =>
            Enum.TryParse(match.Groups[1].Value, ignoreCase: true, out Environment.SpecialFolder specialFolder)
                ? Environment.GetFolderPath(specialFolder)
                : arg);
    }
}