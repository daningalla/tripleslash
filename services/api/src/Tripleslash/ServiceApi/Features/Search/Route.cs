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

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tripleslash.ServiceApi.Infrastructure;

namespace Tripleslash.ServiceApi.Features.Search;

public class Route : RouteDefinition
{
    private const int DefaultPage = 0;
    private const int DefaultSize = 25;
    
    /// <inheritdoc />
    public Route(IEndpointRouteBuilder app)
    {
        app.MapGet("/search", async (
                [FromServices] IMediator mediator,
                [FromQuery(Name = "eco")] string ecosystem,
                [FromQuery(Name = "term")] string term,
                [FromQuery(Name = "pg")] int? page,
                [FromQuery(Name = "sz")] int? size) =>
            {
                var request = new Request
                {
                    Ecosystem = ecosystem,
                    Page = page.GetValueOrDefault(DefaultPage),
                    Size = page.GetValueOrDefault(DefaultSize),
                    Term = term
                };

                return await mediator.Send(request);
            })
            .WithName("search")
            .Produces<Response>();
    }
}