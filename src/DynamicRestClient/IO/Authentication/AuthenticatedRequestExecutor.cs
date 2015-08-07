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

namespace DynamicRestClient.IO.Authentication
{
    using System.Threading.Tasks;

    /// <summary>
    /// A <see cref="IRequestExecutor"/> decorator that supports a <see cref="IAuthenticationPolicy"/>.
    /// </summary>
    public sealed class AuthenticatedRequestExecutor : IRequestExecutor
    {
        private readonly IRequestExecutor executor;
        private readonly IAuthenticationPolicy policy;

        /// <param name="executor">The inner <see cref="IRequestExecutor"/>.</param>
        /// <param name="policy">The <see cref="IAuthenticationPolicy"/> to use.</param>
        public AuthenticatedRequestExecutor(IRequestExecutor executor, IAuthenticationPolicy policy)
        {
            Check.NotNull(executor, "A valid executor was expected.");
            Check.NotNull(policy, "A valid authentication policy was expected.");
            Check.NotNull(executor, "A valid request executor was expected.");
            Check.NotNull(policy, "A valid authentication policy was expected.");

            this.executor = executor;
            this.policy = policy;
        }

        public IRequestBuilder BuildRequest()
        {
            var builder = this.executor.BuildRequest();

            this.policy.AttachAuthentication(builder);

            return builder;
        }

        public IResponse ExecuteRequest(IRequest request)
        {
            Check.NotNull(request, "A valid request was expected.");

            this.policy.AttachAuthentication(request);

            return this.executor.ExecuteRequest(request);
        }

        public Task<IResponse> ExecuteRequestAsync(IRequest request)
        {
            Check.NotNull(request, "A valid request was expected.");

            this.policy.AttachAuthentication(request);

            return this.executor.ExecuteRequestAsync(request);
        }
    }
}
