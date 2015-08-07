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
    using System.Threading.Tasks;

    /// <summary>
    /// A <see cref="IRequestExecutor"/> that does nothing and mocks both the <see cref="IRequest"/>s and <see cref="IResponse"/>s.
    /// </summary>
    public sealed class NullRequestExecutor : IRequestExecutor
    {
        public IRequestBuilder BuildRequest()
        {
            return new RequestBuilder<NullRequest>();
        }

        public IResponse ExecuteRequest(IRequest request)
        {
            Check.NotNull(request, "A valid request was expected.");

            return new NullResponse(request.Url);
        }

        public Task<IResponse> ExecuteRequestAsync(IRequest request)
        {
            return Task.FromResult(ExecuteRequest(request));
        }

        /// <summary>
        /// A null <see cref="IRequest"/>.
        /// </summary>
        private sealed class NullRequest : AbstractRequest
        {
        }

        /// <summary>
        /// A null <see cref="IResponse"/>.
        /// </summary>
        private sealed class NullResponse : IResponse
        {
            public NullResponse(string url)
            {
                Url = url;
            }

            public byte[] RawBytes => new byte[0];

            public string Content => string.Empty;

            public string ContentType => string.Empty;

            public long ContentLength => 0;

            public Encoding ContentEncoding => Encoding.UTF8;

            public string Url { get; }

            public HttpStatusCode StatusCode => HttpStatusCode.OK;
        }
    }
}
