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
    using IO.Caching;
    using Metadata;

    /// <summary>
    /// Denotes the caching policy and expiration interval for a particular request.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class CachingAttribute : Attribute, IMetadataAware
    {
        protected CachingAttribute()
        {
            Representation = CachedRepresentation.Normal; // by default, use a normal representation
        }

        /// <summary>
        /// The desired <see cref="CachedRepresentation"/>.
        /// </summary>
        public CachedRepresentation Representation { get; set; }

        /// <summary>
        /// The <see cref="IExpirationPolicy"/> for the associated method.
        /// </summary>
        protected abstract IExpirationPolicy ExpirationPolicy { get; }

        public void OnAttachMetadata(RequestMetadata metadata)
        {
            metadata.CachingPolicy = new CachingPolicyBuilder
            {
                CachedRepresentation = Representation,
                ExpirationPolicy = ExpirationPolicy
            }.Build();
        }
    }
}
