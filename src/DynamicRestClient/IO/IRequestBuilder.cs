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
    /// Represents a builder pattern for <see cref="IRequest"/>s.
    /// </summary>
    public interface IRequestBuilder
    {
        /// <summary>
        /// The HTTP path to request.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// The <see cref="RestMethod"/> to use.
        /// </summary>
        RestMethod Method { get; set; }

        /// <summary>
        /// The <see cref="IRequestBody"/>, or null.
        /// </summary>
        IRequestBody Body { get; set; }

        /// <summary>
        /// The timeout for the request, or null to use the default timeout.
        /// </summary>
        TimeSpan? Timeout { get; set; }

        /// <summary>
        /// The HTTP headers to add to the request.
        /// </summary>
        IDictionary<string, string> Headers { get; }

        /// <summary>
        /// The URL segments to add to the request.
        /// </summary>
        IDictionary<string, string> Segments { get; }

        /// <summary>
        /// The resultant <see cref="IRequest"/>.
        /// </summary>
        IRequest Build();
    }
}
