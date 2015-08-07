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
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Caching;

    /// <summary>
    /// A <see cref="IRequestExecutor"/> that utilizes the Microsoft <see cref="HttpClient"/> implementation.
    /// </summary>
    public sealed class HttpClientRequestExecutor : IRequestExecutor
    {
        private readonly HttpClient client;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public HttpClientRequestExecutor()
            : this(string.Empty)
        {
        }

        /// <param name="baseUri">The absolute base URI for the executor.</param>
        public HttpClientRequestExecutor(string baseUri)
        {
            this.client = new HttpClient(new HttpClientHandler());

            if (!string.IsNullOrWhiteSpace(baseUri))
            {
                this.client.BaseAddress = new Uri(baseUri, UriKind.Absolute);
            }
        }

        /// <summary>
        /// The default <see cref="HttpRequestHeaders"/> to attach to a request.
        /// </summary>
        public HttpRequestHeaders DefaultRequestHeaders => this.client.DefaultRequestHeaders;

        /// <summary>
        /// The base address for the executor.
        /// </summary>
        public Uri BaseAddress => this.client.BaseAddress;

        /// <summary>
        /// The default timeout period for the executor.
        /// </summary>
        public TimeSpan Timeout
        {
            get { return this.client.Timeout; }
            set { this.client.Timeout = value; }
        }

        /// <summary>
        /// The maximum response content buffer size.
        /// </summary>
        public long MaxResponseContentBufferSize
        {
            get { return this.client.MaxResponseContentBufferSize; }
            set { this.client.MaxResponseContentBufferSize = value; }
        }

        public IRequestBuilder BuildRequest()
        {
            return new RequestBuilder<HttpClientRequest>();
        }

        public IResponse ExecuteRequest(IRequest request)
        {
            Check.That(request is HttpClientRequest, "A request created with this executor was expected.");

            return ExecuteRequestAsync(request).Result;
        }

        public Task<IResponse> ExecuteRequestAsync(IRequest original)
        {
            Check.That(original is HttpClientRequest, "A request created with this executor was expected.");

            var request = (HttpClientRequest) original;
            var message = request.BuildMessage();

            return this.client.SendAsync(message).ContinueWith<IResponse>(task =>
            {
                var result = task.Result;
                try
                {
                    result.EnsureSuccessStatusCode();
                    return new HttpClientResponse(result);
                }
                catch (HttpRequestException e)
                {
                    throw new RequestExecutorException("An error occurred whilst executing a request.", result.StatusCode, e);
                }
            });
        }

        /// <summary>
        /// A <see cref="IRequest"/> implementation for the Microsoft HTTP client implementation.
        /// </summary>
        [DebuggerDisplay("{Method} {Url}")]
        private sealed class HttpClientRequest : AbstractRequest, ICacheKeyProvider
        {
            public string CacheKey => RequestHelpers.SubstituteUrlParameters(Url, Segments, Uri.EscapeUriString);

            public HttpRequestMessage BuildMessage()
            {
                var message = new HttpRequestMessage(
                    Helpers.ConvertMethod(Method),
                    Helpers.SubstituteUrlSegments(Url, Segments));

                foreach (var header in Headers)
                {
                    message.Headers.Add(header.Key, header.Value);
                }

                if (Body != null)
                {
                    message.Content = new StringContent(Body.Content, Encoding.UTF8, Body.ContentType);
                }

                if (Timeout.HasValue)
                {
                    throw new NotSupportedException("HttpClientRequestExecutor currently doesn't support per-request timeouts.");
                }

                return message;
            }
        }

        /// <summary>
        /// A <see cref="IResponse"/> implementation for the Microsoft HTTP client implementation.
        /// </summary>
        [DebuggerDisplay("{Url} {StatusCode} {ContentLength}")]
        private sealed class HttpClientResponse : IResponse
        {
            private readonly Lazy<string> content;
            private readonly HttpResponseMessage message;

            public HttpClientResponse(HttpResponseMessage message)
            {
                this.message = message;
                this.content = new Lazy<string>(() => ContentEncoding.GetString(RawBytes));

                RawBytes = message.Content.ReadAsByteArrayAsync().Result;
            }

            public long ContentLength => RawBytes.Length;

            public byte[] RawBytes { get; }

            public string Content => this.content.Value;

            public string ContentType => this.message.Content.Headers.ContentType.MediaType;

            public Encoding ContentEncoding => Encoding.UTF8;

            public string Url => this.message.RequestMessage.RequestUri.ToString();

            public HttpStatusCode StatusCode => this.message.StatusCode;
        }

        /// <summary>
        /// Shared helpers for this implementation.
        /// </summary>
        private static class Helpers
        {
            public static HttpMethod ConvertMethod(RestMethod method)
            {
                switch (method)
                {
                    case RestMethod.Get:
                        return HttpMethod.Get;
                    case RestMethod.Delete:
                        return HttpMethod.Delete;
                    case RestMethod.Head:
                        return HttpMethod.Head;
                    case RestMethod.Options:
                        return HttpMethod.Options;
                    case RestMethod.Post:
                        return HttpMethod.Post;
                    case RestMethod.Put:
                        return HttpMethod.Put;
                }

                throw new NotSupportedException("An unsupported HTTP method was requested: " + method);
            }

            public static string SubstituteUrlSegments(string path, IEnumerable<KeyValuePair<string, string>> segments)
            {
                foreach (var segment in segments)
                {
                    path = path.Replace("{" + segment.Key + "}", segment.Value);
                }

                return path;
            }
        }
    }
}
