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

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tripleslash.Core;

/// <summary>
/// Defines the supported ecosystem monikers.
/// </summary>
[JsonConverter(typeof(JsonConverter))]
public sealed class Ecosystem : IEquatable<Ecosystem>
{
    private Ecosystem(string moniker, string displayName)
    {
        Moniker = moniker;
        DisplayName = displayName;
    }

    /// <summary>
    /// Defines a JsonConverter for this type.
    /// </summary>
    public class JsonConverter : JsonConverter<Ecosystem>
    {
        /// <inheritdoc />
        public override Ecosystem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var serialized = reader.GetString();
            return serialized != null ? Parse(serialized) : null;
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, Ecosystem value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Moniker);
        }
    }

    /// <summary>
    /// Defines the dotnet ecosystem.
    /// </summary>
    public static readonly Ecosystem Dotnet = new Ecosystem("dotnet", ".NET");

    /// <summary>
    /// Defines all ecosystems.
    /// </summary>
    public static readonly IReadOnlyCollection<Ecosystem> All = new[]
    {
        Dotnet
    };

    /// <summary>
    /// Parses the given string.
    /// </summary>
    /// <param name="s">Moniker to match.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static Ecosystem Parse(string s) => TryParse(s, out var ecosystem)
        ? ecosystem!
        : throw new ArgumentException($"\"{s}\" is not a valid ecosystem moniker", nameof(s));
    
    /// <summary>
    /// Tries to parse the given string.
    /// </summary>
    /// <param name="s">The moniker to match.</param>
    /// <param name="ecosystem">Ecosystem</param>
    /// <returns><c>true</c> if <paramref name="s"/> was matched to a supported ecosystem.</returns>
    public static bool TryParse(string s, out Ecosystem? ecosystem)
    {
        ecosystem = All.FirstOrDefault(e => e.Moniker.Equals(s, StringComparison.OrdinalIgnoreCase));
        return ecosystem != null;
    }

    /// <summary>
    /// Gets the ecosystem moniker.
    /// </summary>
    public string Moniker { get; }
    
    /// <summary>
    /// Gets the display name.
    /// </summary>
    public string DisplayName { get; }

    /// <inheritdoc />
    public bool Equals(Ecosystem? other) => Moniker.Equals(other?.Moniker);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Ecosystem other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => Moniker.GetHashCode();

    /// <inheritdoc />
    public override string ToString() => $"{Moniker}: {DisplayName}";
} 