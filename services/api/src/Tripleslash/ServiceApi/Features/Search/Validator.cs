﻿//  Copyright 2022 Tripleslash contributors
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

using FluentValidation;
using Microsoft.Extensions.Options;
using Tripleslash.ServiceApi.Options;

namespace Tripleslash.ServiceApi.Features.Search;

/// <summary>
/// Validates the request
/// </summary>
public class Validator : AbstractValidator<Request>
{
    public Validator(IOptions<SearchOptions> optionsProvider)
    {
        var options = optionsProvider.Value;
        
        RuleFor(req => req.Ecosystem).IsInEnum();
        RuleFor(req => req.Page).GreaterThanOrEqualTo(0);
        RuleFor(req => req.Size).GreaterThanOrEqualTo(0).LessThanOrEqualTo(options.MaxResultSize);
        RuleFor(req => req.Term).NotNull().NotEmpty().MinimumLength(options.MinTermLength);
    }
}