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
    using System.Net;
    using System.Text;

    /// <summary>
    /// Abstractly represents a response in the RestClient I/O pipeline.
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// The raw bytes of the response.
        /// </summary>
        byte[] RawBytes { get; }

        /// <summary>
        /// The decoded <see cref="RawBytes"/> from the response.
        /// </summary>
        string Content { get; }

        /// <summary>
        /// The content type.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// The size of the response, in bytes.
        /// </summary>
        long ContentLength { get; }

        /// <summary>
        /// The <see cref="Encoding"/> of the content.
        /// </summary>
        Encoding ContentEncoding { get; }

        /// <summary>
        /// The URL the response was received from.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// The <see cref="HttpStatusCode"/> of the response.
        /// </summary>
        HttpStatusCode StatusCode { get; }
    }
}
