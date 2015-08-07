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

namespace DynamicRestClient.Caching
{
    using System;

    /// <summary>
    /// Settings for inserting an entry into a <see cref="ICache"/>.
    /// </summary>
    public sealed class CacheSettings
    {
        /// <summary>
        /// Reasonable default <see cref="CacheSettings"/>.
        /// </summary>
        public static readonly CacheSettings Default = new CacheSettings();

        private DateTimeOffset? expirationTime;
        private TimeSpan? slidingExpiration;

        /// <summary>
        /// The absolute expiration time for the associated cache entry.
        /// </summary>
        /// <remarks>This is mutually exclusive with <see cref="SlidingExpiration"/></remarks>
        public DateTimeOffset ExpirationTime
        {
            get { return this.expirationTime ?? DateTimeOffset.MaxValue; }
            set
            {
                Check.That(!this.slidingExpiration.HasValue, "An absolute expiration may not be set if the sliding expiration has been set.");

                this.expirationTime = value;
            }
        }

        /// <summary>
        /// The sliding expiration time for the associated cache entry.
        /// </summary>
        /// <remarks>This is mutually exclusive with <see cref="ExpirationTime"/></remarks>
        public TimeSpan SlidingExpiration
        {
            get { return this.slidingExpiration ?? TimeSpan.Zero; }
            set
            {
                Check.That(!this.expirationTime.HasValue, "A sliding expiration may not be set if the absolute expiration has been set.");

                this.slidingExpiration = value;
            }
        }
    }
}
