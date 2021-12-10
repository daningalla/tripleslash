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

using Microsoft.Extensions.DependencyInjection;

namespace Tripleslash.DependencyInjection;

/// <summary>
/// Represents an object used to build application services of a specific type.
/// </summary>
public class ServiceBuilder<TService>
{
    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="applicationServices">Applications services.</param>
    public ServiceBuilder(IServiceCollection applicationServices)
    {
        ApplicationServices = applicationServices;
    }

    /// <summary>
    /// Gets the application's service collection.
    /// </summary>
    public IServiceCollection ApplicationServices { get; }
}