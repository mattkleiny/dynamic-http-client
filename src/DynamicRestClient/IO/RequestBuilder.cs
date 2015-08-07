// The MIT License (MIT)
// 
// Copyright (C) 2015, Matthew Kleinschafer.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace DynamicRestClient.IO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A builder for <see cref="AbstractRequest"/> implementations.
    /// </summary>
    public class RequestBuilder<TRequest> : IRequestBuilder
        where TRequest : AbstractRequest, new()
    {
        public string Path { get; set; }

        public RestMethod Method { get; set; }

        public IRequestBody Body { get; set; }

        public TimeSpan? Timeout { get; set; }

        public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public IDictionary<string, string> Segments { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public virtual IRequest Build()
        {
            var request = new TRequest
            {
                Url = Path,
                Method = Method,
                Body = Body
            };

            foreach (var header in Headers)
            {
                request.Headers.Add(header);
            }

            foreach (var segment in Segments)
            {
                request.Segments.Add(segment);
            }

            request.Timeout = Timeout;

            return request;
        }
    }
}
