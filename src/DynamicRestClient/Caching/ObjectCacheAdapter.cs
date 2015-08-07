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
    using System.Runtime.Caching;
    using System.Threading;
    using Utilities;

    /// <summary>
    /// A <see cref="ICache"/> implementation that adapts the <see cref="ObjectCache"/> from System.Runtime.Caching.
    /// </summary>
    public sealed class ObjectCacheAdapter : ICache
    {
        private readonly ObjectCache cache;

        /// <summary>
        /// A <see cref="ReaderWriterLockSlim"/> for serializing write access to <see cref="GetOrCompute{T}"/>.
        /// </summary>
        private readonly ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();

        /// <param name="cache">The <see cref="cache"/> to adapt.</param>
        public ObjectCacheAdapter(ObjectCache cache)
        {
            Check.NotNull(cache, nameof(cache));

            this.cache = cache;
        }

        public T GetOrCompute<T>(string key, CacheSettings settings, Func<T> computeDelegate, Func<T, bool> shouldCachePredicate)
        {
            Check.NotNullOrEmpty(key, nameof(key));
            Check.NotNull(computeDelegate, nameof(computeDelegate));
            Check.NotNull(shouldCachePredicate, nameof(shouldCachePredicate));

            // try quick read without serializing access
            using (this.cacheLock.ScopedReadLock())
            {
                var result = (T) this.cache.Get(key);
                if (result != null)
                {
                    return result;
                }
            }

            using (this.cacheLock.ScopedUpgradeableReadLock())
            {
                // second attempt, in case another thread has already acquired the resource
                var result = (T) this.cache.Get(key);
                if (result != null)
                {
                    return result;
                }

                // serialize access, acquire and insert resource
                using (this.cacheLock.ScopedWriteLock())
                {
                    var value = computeDelegate();

                    if (shouldCachePredicate(value))
                    {
                        this.cache.Add(key, value, ConvertToItemPolicy(settings));
                    }

                    return value;
                }
            }
        }

        /// <summary>
        /// Converts a <see cref="CacheSettings"/> to a <see cref="CacheItemPolicy"/>.
        /// </summary>
        private static CacheItemPolicy ConvertToItemPolicy(CacheSettings settings)
        {
            return new CacheItemPolicy
            {
                AbsoluteExpiration = settings.ExpirationTime,
                SlidingExpiration = settings.SlidingExpiration
            };
        }
    }
}
