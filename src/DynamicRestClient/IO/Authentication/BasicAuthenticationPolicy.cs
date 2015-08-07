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
    using System;
    using System.Text;

    /// <summary>
    /// A <see cref="IAuthenticationPolicy"/> that uses HTTP Basic authentication.
    /// </summary>
    public sealed class BasicAuthenticationPolicy : IAuthenticationPolicy
    {
        private readonly string encodedCredentials;

        /// <param name="username">The username to use for authenticating.</param>
        /// <param name="password">The password to use for authenticating.</param>
        public BasicAuthenticationPolicy(string username, string password)
        {
            Check.NotNullOrEmpty(username, "A valid username was expected.");
            Check.NotNullOrEmpty(password, "A valid password was expected.");

            this.encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
        }

        public void AttachAuthentication(IRequestBuilder builder)
        {
            builder.Headers.Add("Authorization", "Basic " + this.encodedCredentials);
        }

        public void AttachAuthentication(IRequest request)
        {
        }
    }
}
