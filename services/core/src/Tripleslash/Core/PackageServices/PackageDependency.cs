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
/// Represents a package dependency.
/// </summary>
public class PackageDependency
{
    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="packageId">Dependency package id</param>
    /// <param name="lowerBound">Lower bound for the version requirement</param>
    /// <param name="upperBound">Upper bound for the version requirement</param>
    /// <exception cref="ArgumentException"><paramref name="packageId"/> is null.</exception>
    public PackageDependency(
        string packageId,
        DependencyVersion? lowerBound,
        DependencyVersion? upperBound)
    {
        PackageId = Guard.Argument(packageId, nameof(packageId)).NotNull().NotWhiteSpace();
        LowerBound = lowerBound;
        UpperBound = upperBound;

        if (lowerBound != null && upperBound != null && lowerBound.Version > upperBound.Version)
            throw new ArgumentException("Lower bound cannot be greater than upper bound");
    }
    
    /// <summary>
    /// Gets the package dependency id.
    /// </summary>
    public string PackageId { get; }

    /// <summary>
    /// Gets the dependency lower bound.
    /// </summary>
    public DependencyVersion? LowerBound { get; }
    
    /// <summary>
    /// Gets the dependency upper bound.
    /// </summary>
    public DependencyVersion? UpperBound { get; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{PackageId} (lower={LowerBound}, upper={UpperBound})";
    }
}