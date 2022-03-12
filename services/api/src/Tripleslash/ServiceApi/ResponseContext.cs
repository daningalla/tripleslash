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

using System.Diagnostics;
using System.Net;
using System.Text.Json.Serialization;

namespace Tripleslash.ServiceApi;

public class ResponseContext
{
    /// <summary>
    /// Represents a basic response.
    /// </summary>
    /// <param name="statusCode">Status code</param>
    /// <param name="message">Optional message</param>
    /// <param name="errorData">Error data</param>
    public ResponseContext(
        HttpStatusCode statusCode = HttpStatusCode.OK,
        string? message = null,
        IDictionary<string, object>? errorData = null)
    {
        TraceId = Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString();
        StatusCode = statusCode;
        Message = message;
        ErrorData = errorData;
    }

    /// <summary>
    /// Gets the result status code (defaults to 200)
    /// </summary>
    [JsonIgnore]
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets an optional message to return to the user.
    /// </summary>
    public string? Message { get; }

    /// <summary>
    /// Gets an optional dictionary of error related data.
    /// </summary>
    public IDictionary<string, object>? ErrorData { get; }

    /// <summary>
    /// Gets the request trace id.
    /// </summary>
    public string TraceId { get; }
}

public class ResponseContext<T> : ResponseContext where T : class
{
    /// <summary>
    /// Represents a basic response.
    /// </summary>
    /// <param name="result">Result object</param>
    /// <param name="statusCode">Status code</param>
    /// <param name="message">Optional message</param>
    /// <param name="errorData">Error data</param>
    public ResponseContext(
        T result,
        HttpStatusCode statusCode = HttpStatusCode.OK,
        string? message = null,
        IDictionary<string, object>? errorData = null)
    {
        Result = result;
    }

    /// <summary>
    /// Gets the result data.
    /// </summary>
    public T Result { get; }
}

