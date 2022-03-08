//  Copyright 2022 Tripleslash contributors
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using System.Reflection;

namespace Tripleslash.ServiceApi.Infrastructure;

public static class RouteExtensions
{
    public static void MapRouteDefinitions(this WebApplication app)
    {
        var definitionTypes = typeof(RouteExtensions)
            .Assembly
            .GetTypes()
            .Where(type => !type.IsAbstract && typeof(RouteDefinition).IsAssignableFrom(type));

        foreach (var type in definitionTypes)
        {
            // Performs registration
            Activator.CreateInstance(type, app);
        }
    }    
}