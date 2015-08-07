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
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A <see cref="IAuthenticationPolicy"/> that composes other <see cref="IAuthenticationPolicy"/>s.
    /// </summary>
    public sealed class CompositeAuthenticationPolicy : IAuthenticationPolicy
    {
        private readonly IEnumerable<IAuthenticationPolicy> policies;

        /// <param name="policies">The <see cref="IAuthenticationPolicy"/> to compose.</param>
        public CompositeAuthenticationPolicy(params IAuthenticationPolicy[] policies)
            : this(policies.ToList())
        {
        }

        /// <param name="policies">The <see cref="IAuthenticationPolicy"/> to compose.</param>
        public CompositeAuthenticationPolicy(IEnumerable<IAuthenticationPolicy> policies)
        {
            Check.NotNull(policies, nameof(policies));

            this.policies = policies;
        }

        public void AttachAuthentication(IRequestBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            foreach (var policy in this.policies)
            {
                policy.AttachAuthentication(builder);
            }
        }

        public void AttachAuthentication(IRequest request)
        {
            Check.NotNull(request, nameof(request));

            foreach (var policy in this.policies)
            {
                policy.AttachAuthentication(request);
            }
        }
    }
}
