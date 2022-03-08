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

namespace Tripleslash.ServiceApi.Features.Search;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly LinkGenerator _linkGenerator;

    public Handler(LinkGenerator linkGenerator)
    {
        _linkGenerator = linkGenerator;
    }
    
    /// <inheritdoc />
    public Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var response = new Response(_linkGenerator.GetPathByName("search", values: null)!);

        return Task.FromResult(response);
    }
}