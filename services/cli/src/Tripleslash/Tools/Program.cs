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

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tripleslash.PackageServices;
using Tripleslash.Tools;
using Tripleslash.Tools.Infrastructure;
using Vertical.CommandLine;
using Vertical.ConsoleApplications;

Console.WriteLine("Tripleslash CLI - v1.0");

var options = CommandLineApplication.ParseArguments<RootOptions>(
    RootOptions.BuildConfiguration(), args);

var logger = Logging.Initialize(options.LoggingLevel);

try
{
    logger.LogDebug("Building configuration...");
    
    var configuration = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .AddJsonFiles(options.ConfigurationPaths)
        .Build();

    var hostBuilder = ConsoleHostBuilder
        .CreateDefault()
        .ConfigureAppConfiguration(configBuilder => configBuilder
            .AddEnvironmentVariables()
            .AddJsonFiles(options.ConfigurationPaths))
        .ConfigureConsoleLogging(options.LoggingLevel)
        .ConfigureServices(services =>
        {
            services.AddPackageServices(builder => builder
                .AddNuGetProviders(configuration.GetSection("PackageServices:NuGet")));
        })
        .ConfigureProviders(providers =>
        {
            providers.AddInteractiveConsole();
        })
        .Configure(app =>
        {
            app.UseExitCommand("exit");

            app.UseEnvironmentVariableTokens();

            app.UseSpecialFolderTokens();

            app.UseRouting(router =>
            {
                router.MapHandlers();
                
                
            });
        });
    
    logger.LogInformation("Build CLI environment complete, starting interactive session");

    await hostBuilder.RunConsoleAsync();
}
catch (Exception exception)
{
    Logging.Default.LogError(exception, "Unhandled error occurred");
}