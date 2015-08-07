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

namespace DynamicRestClient.IO
{
    using System;
    using System.Diagnostics;
    using Serialization;

    /// <summary>
    /// A <see cref="IRequestBody"/> that defers the serialization of some body object to a <see cref="ISerializer"/>.
    /// </summary>
    [DebuggerDisplay("Serialized body via {Serializer}")]
    public sealed class SerializedBody : IRequestBody
    {
        /// <param name="body">The object to serialize.</param>
        public SerializedBody(object body)
            : this(body, new NewtonsoftSerializer())
        {
        }

        /// <param name="body">The object to serialize.</param>
        /// <param name="serializer">The <see cref="ISerializer"/> to use.</param>
        public SerializedBody(object body, ISerializer serializer)
            : this(body.GetType(), body, serializer)
        {
        }

        /// <param name="type">The <see cref="System.Type"/> of the <see cref="Body"/>.</param>
        /// <param name="body">The object to serialize.</param>
        /// <param name="serializer">The <see cref="ISerializer"/> to use.</param>
        public SerializedBody(Type type, object body, ISerializer serializer)
        {
            Check.NotNull(type, "A valid body type was expected.");
            Check.NotNull(body, "A valid body object was expected.");
            Check.NotNull(serializer, "A valid serializer was expected.");

            Type = type;
            Body = body;
            Serializer = serializer;
        }

        /// <summary>
        /// The <see cref="System.Type"/> of the body object.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// The body object itself.
        /// </summary>
        public object Body { get; }

        /// <summary>
        /// The underlying <see cref="ISerializer"/>.
        /// </summary>
        public ISerializer Serializer { get; }

        public string Content => Serializer.Serialize(Type, Body);

        public string ContentType => Serializer.ContentType;
    }
}
