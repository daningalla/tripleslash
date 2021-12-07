﻿// Copyright (c) 2021 Tripleslash project contributors
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

namespace Tripleslash.Infrastructure;

public static class StringExtensions
{
    public static string? NullIfEmpty(string? s) => string.IsNullOrEmpty(s) ? null : s;

    public static string? NullIfWhitespace(string? s) => string.IsNullOrWhiteSpace(s) ? null : s;

    /// <summary>
    /// Gets whether a string is equal without regard to case
    /// </summary>
    /// <param name="str">String to compare</param>
    /// <param name="other">String to compare</param>
    /// <returns></returns>
    public static bool EqualsIgnoreCase(this string str, string other) => string.Compare(str
        , other
        , StringComparison.OrdinalIgnoreCase) == 0;
}