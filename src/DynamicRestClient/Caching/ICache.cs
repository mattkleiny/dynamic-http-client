﻿// The MIT License (MIT)
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
    /// Represents a cache generically.
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Attempts to retrieve an item with the given <see cref="key"/> from the cache.
        /// <para/>
        /// If the item does not exist the <see cref="computeDelegate"/> is used to retrieve it.  
        /// <para/>
        /// The <see cref="shouldCachePredicate"/> is invoked after the <see cref="computeDelegate"/> to determine if the result is valid for caching.
        /// </summary>
        /// <remarks>This method should be thread-safe.</remarks>
        T GetOrCompute<T>(string key, Func<T> computeDelegate, Func<T, bool> shouldCachePredicate);
    }
}
