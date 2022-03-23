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
using FluentValidation;
using MediatR;

namespace Tripleslash.ServiceApi.Infrastructure;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResponseContext
{
    private readonly IValidator<TRequest> _validator;

    /// <summary>
    /// Creates a new instance of this type.
    /// </summary>
    /// <param name="validator">Validator</param>
    public ValidationPipelineBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }
    
    /// <inheritdoc />
    public async Task<TResponse> Handle(
        TRequest request, 
        CancellationToken cancellationToken, 
        RequestHandlerDelegate<TResponse> next)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);

        if (result.IsValid)
        {
            return await next();
        }

        return (TResponse)new ResponseContext(
            HttpStatusCode.BadRequest,
            "One or more request parameters are invalid",
            new Dictionary<string, object>{ { "validationErrors", result.Errors } });
    }
}