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

namespace DynamicRestClient.IO.Serialization
{
    using System;
    using System.IO;
    using Newtonsoft.Json;

    /// <summary>
    /// A <see cref="IDeserializer"/> that uses Newtonsoft's JSON deserializer.
    /// </summary>
    public sealed class NewtonsoftDeserializer : IDeserializer
    {
        private readonly JsonSerializer serializer = JsonSerializer.CreateDefault(SerializationConstants.DefaultSerializerSettings);

        public object Deserialize(Type type, TextReader reader)
        {
            Check.NotNull(type, "A valid type was expected.");
            Check.NotNull(reader, "A valid text reader was expected.");

            return this.serializer.Deserialize(reader, type);
        }
    }
}