// Copyright 2022 Tripleslash contributors
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

using Dawn;

namespace Tripleslash.Core.PackageServices;

/// <summary>
/// Represents a dependency version.
/// </summary>
public class DependencyVersion
{
    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="version">Base version value.</param>
    /// <param name="constraint">The bound constraint.</param>
    public DependencyVersion(SemVersion version, VersionConstraint constraint)
    {
        Version = Guard.Argument(version, nameof(version)).NotNull();
        Constraint = constraint;
    }

    /// <summary>
    /// Gets the dependency version.
    /// </summary>
    public SemVersion Version { get; }

    /// <summary>
    /// Gets the constraint on the version.
    /// </summary>
    public VersionConstraint Constraint { get; }

    /// <inheritdoc />
    public override string ToString() => $"{Version} ({Constraint})";
}