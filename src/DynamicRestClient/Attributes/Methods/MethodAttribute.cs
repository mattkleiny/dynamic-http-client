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

namespace DynamicRestClient.Attributes.Methods
{
    using System;
    using Metadata;

    /// <summary>
    /// Base class for any <see cref="Attribute"/> which defines a <see cref="method"/> and <see cref="path"/>.
    /// </summary>
    public abstract class MethodAttribute : Attribute, IMetadataAware
    {
        private readonly RestMethod method;
        private readonly string path;

        protected MethodAttribute(RestMethod method, string path)
        {
            this.method = method;
            this.path = path;
        }

        /// <summary>
        /// True whether a response is required and an exception should be generated if one is not received, otherwise false.
        /// </summary>
        public bool IsResponseRequired { get; set; }

        public void OnAttachMetadata(RequestMetadata metadata)
        {
            metadata.Method = this.method;
            metadata.Path = this.path;
            metadata.IsResponseRequired = IsResponseRequired;
        }
    }
}
