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

namespace DynamicRestClient.Attributes
{
    using System;
    using IO.Serialization;
    using Metadata;

    /// <summary>
    /// An <see cref="Attribute"/> which associates a <see cref="IDeserializer"/> with a method or interface.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
    public sealed class DeserializerAttribute : Attribute, IMetadataAware
    {
        private readonly Type deserializerType;

        /// <param name="deserializerType">The associated <see cref="IDeserializer"/> type.</param>
        public DeserializerAttribute(Type deserializerType)
        {
            Check.That(typeof (IDeserializer).IsAssignableFrom(deserializerType), "A valid IDeserializer type was expected.");

            this.deserializerType = deserializerType;
        }

        public void OnAttachMetadata(RequestMetadata metadata)
        {
            metadata.Deserializer = (IDeserializer) Activator.CreateInstance(this.deserializerType);
        }
    }
}