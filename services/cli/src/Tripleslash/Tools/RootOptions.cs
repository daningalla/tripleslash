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

using Microsoft.Extensions.Logging;
using Vertical.CommandLine.Configuration;

namespace Tripleslash.Tools;

/// <summary>
/// Defines root options common to all features
/// </summary>
public class RootOptions
{
    /// <summary>
    /// Gets or sets the logging level.
    /// </summary>
    public LogLevel LoggingLevel { get; set; } = LogLevel.Information;
    
    /// <summary>
    /// Gets a collection of configuration globs
    /// </summary>
    public List<string> ConfigurationPaths { get; } = new();

    internal static ApplicationConfiguration<RootOptions> BuildConfiguration()
    {
        var configuration = new ApplicationConfiguration<RootOptions>();

        configuration
            .Option<LogLevel>("--log-verbosity=", arg => arg.Map.ToProperty(opt => opt.LoggingLevel))
            .Option("-c=|--config=", arg => arg.MapMany.ToCollection(opt => opt.ConfigurationPaths));

        return configuration;
    }
}