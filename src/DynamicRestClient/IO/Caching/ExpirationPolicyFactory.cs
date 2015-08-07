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
    using System;
    using DynamicRestClient.Caching;

    /// <summary>
    /// Static factories for <see cref="IExpirationPolicy"/>s.
    /// </summary>
    internal static class ExpirationPolicyFactory
    {
        /// <summary>
        /// Builds a <see cref="IExpirationPolicy"/> that defines a relative expiration interval.
        /// </summary>
        /// <param name="interval">The relative interval.</param>
        public static IExpirationPolicy BuildRelativeExpirationPolicy(TimeSpan interval)
        {
            return new RelativeExpirationPolicy(interval);
        }

        /// <summary>
        /// Builds a <see cref="IExpirationPolicy"/> that defines a sliding expiration interval.
        /// </summary>
        /// <param name="interval">The sliding interval.</param>
        public static IExpirationPolicy BuildSlidingExpirationPolicy(TimeSpan interval)
        {
            return new SlidingExpirationPolicy(interval);
        }

        /// <summary>
        /// A <see cref="IExpirationPolicy"/> that defines a relative expiration interval.
        /// </summary>
        private sealed class RelativeExpirationPolicy : IExpirationPolicy
        {
            private readonly TimeSpan interval;

            public RelativeExpirationPolicy(TimeSpan interval)
            {
                this.interval = interval;
            }

            public CacheSettings BuildCacheSettings(IRequest request)
            {
                return new CacheSettings
                {
                    ExpirationTime = DateTime.Now + this.interval
                };
            }

            public override string ToString()
            {
                return $"Relative expiration every {this.interval}";
            }
        }

        /// <summary>
        /// A <see cref="IExpirationPolicy"/> that defines a sliding expiration policy.
        /// </summary>
        private sealed class SlidingExpirationPolicy : IExpirationPolicy
        {
            private readonly TimeSpan interval;

            public SlidingExpirationPolicy(TimeSpan interval)
            {
                this.interval = interval;
            }

            public CacheSettings BuildCacheSettings(IRequest request)
            {
                return new CacheSettings
                {
                    SlidingExpiration = this.interval
                };
            }

            public override string ToString()
            {
                return $"Sliding expiration every {this.interval}";
            }
        }
    }
}
