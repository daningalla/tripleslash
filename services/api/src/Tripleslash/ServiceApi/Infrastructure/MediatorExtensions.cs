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

using System.Net;
using MediatR;

namespace Tripleslash.ServiceApi.Infrastructure;

public static class MediatorExtensions
{
    public static async Task<object> GetResponseAsync<TRequest>(
        this IMediator mediator,
        TRequest request)
        where TRequest : class
    {
        var result = await mediator.Send(request, CancellationToken.None);

        if (result is not ResponseContext responseContext)
            throw new InvalidOperationException($"Result is not of type {nameof(responseContext)}");

        return responseContext.StatusCode switch
        {
            HttpStatusCode.OK => Results.Ok(responseContext),
            HttpStatusCode.BadRequest => Results.BadRequest(responseContext),
            _ => throw new NotSupportedException()
        };
    }
}