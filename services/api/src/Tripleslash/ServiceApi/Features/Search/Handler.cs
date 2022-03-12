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

using AutoMapper;
using MediatR;
using Tripleslash.PackageServices;

namespace Tripleslash.ServiceApi.Features.Search;

public class Handler : IRequestHandler<Request, ResponseContext>
{
    private readonly IPackageServiceAggregator _packageAggregator;
    private readonly IMapper _mapper;

    public Handler(IPackageServiceAggregator packageAggregator, IMapper mapper)
    {
        _packageAggregator = packageAggregator;
        _mapper = mapper;
    }
    
    /// <inheritdoc />
    public async Task<ResponseContext> Handle(Request request, CancellationToken cancellationToken)
    {
        var result = await _packageAggregator.SearchAsync(
            request.Ecosystem,
            request.Term,
            request.Page,
            request.Size,
            request.PreRelease,
            cancellationToken);

        var response = _mapper.Map<Response>(result);

        return new ResponseContext<Response>(response);
    }
}