// Copyright 2022 Tripleslash contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Text.RegularExpressions;

namespace Tripleslash.Core.Utilities;

public static class StringExtensions
{
    /// <summary>
    /// Returns <c>null</c> if <paramref name="s"/> is null or whitespace.
    /// </summary>
    /// <param name="s">Input string</param>
    /// <returns>String value or <c>null</c></returns>
    public static string? NullIfWhiteSpace(this string? s) => string.IsNullOrWhiteSpace(s)
        ? null
        : s;

    /// <summary>
    /// Tries to get a group value as a <typeparamref name="T"/> value.
    /// </summary>
    /// <param name="match">Regex <see cref="Match"/></param>
    /// <param name="group">Group name</param>
    /// <param name="converter">Function that converts the group value to <typeparamref name="T"/></param>
    /// <param name="value">When the method returns, the converted group value</param>
    /// <typeparam name="T">Converted value type</typeparam>
    /// <returns><c>true</c> if the group match was successful; otherwise false</returns>
    public static bool TryGetGroupValue<T>(this Match match,
        string group,
        Func<string, T> converter, 
        out T value)
    {
        var matchGroup = match.Groups[group];

        value = matchGroup.Success
            ? converter(matchGroup.Value)
            : default!;

        return matchGroup.Success;
    }
}