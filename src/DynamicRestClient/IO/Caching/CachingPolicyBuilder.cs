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

namespace DynamicRestClient.IO.Caching
{
    using System.Net;
    using DynamicRestClient.Caching;

    /// <summary>
    /// A builder for <see cref="ICachingPolicy"/>s.
    /// </summary>
    internal sealed class CachingPolicyBuilder
    {
        /// <summary>
        /// The desired <see cref="IExpirationPolicy"/>.
        /// </summary>
        public IExpirationPolicy ExpirationPolicy { get; set; }

        /// <summary>
        /// The desired <see cref="CachedRepresentation"/>.
        /// </summary>
        public CachedRepresentation CachedRepresentation { get; set; }

        /// <summary>
        /// Builds the resultant <see cref="ICachingPolicy"/>.
        /// </summary>
        public ICachingPolicy Build()
        {
            return new CachingPolicy(ExpirationPolicy, CachedRepresentation);
        }

        /// <summary>
        /// The <see cref="ICachingPolicy"/> implementation.
        /// </summary>
        private sealed class CachingPolicy : ICachingPolicy
        {
            private readonly CachedRepresentation cachedRepresentation;
            private readonly IExpirationPolicy expirationPolicy;

            /// <param name="expirationPolicy">The desired <see cref="IExpirationPolicy"/>.</param>
            /// <param name="cachedRepresentation">The desired <see cref="CachedRepresentation"/>.</param>
            public CachingPolicy(IExpirationPolicy expirationPolicy, CachedRepresentation cachedRepresentation)
            {
                this.expirationPolicy = expirationPolicy;
                this.cachedRepresentation = cachedRepresentation;
            }

            public string EvaluateCacheKey(IRequest request)
            {
                Check.NotNull(request, "A valid request was expected.");

                if (request is ICacheKeyProvider)
                {
                    return (request as ICacheKeyProvider).CacheKey;
                }

                return request.Url;
            }

            public bool ShouldCache(IResponse response)
            {
                Check.NotNull(response, "A valid response was expected.");

                // don't cache items if they aren't found
                return response.StatusCode != HttpStatusCode.NotFound;
            }

            public IResponse GetCacheableResponse(IResponse response)
            {
                Check.NotNull(response, "A valid response was expected.");

                return CacheableResponseFactory.BuildCachedResponse(this.cachedRepresentation, response);
            }

            public CacheSettings GetCacheSettings(IRequest request)
            {
                Check.NotNull(request, "A valid request was expected.");

                return this.expirationPolicy.BuildCacheSettings(request);
            }

            public override string ToString()
            {
                return $"{this.expirationPolicy} represented as {this.cachedRepresentation}";
            }
        }
    }
}
