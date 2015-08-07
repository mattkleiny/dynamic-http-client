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

namespace DynamicRestClient.IO.Compression
{
    using System.IO;

    /// <summary>
    /// A <see cref="ICompressor"/> implementation that compresses data using .NET <see cref="Stream"/>s.
    /// </summary>
    public abstract class StreamingCompressor : ICompressor
    {
        private readonly int bufferSize;

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected StreamingCompressor()
            : this(4096)
        {
        }

        /// <param name="bufferSize">The size of the buffer to use when compressing/decompressing.</param>
        protected StreamingCompressor(int bufferSize)
        {
            this.bufferSize = bufferSize;
        }

        public byte[] Compress(byte[] bytes)
        {
            Check.NotNull(bytes, "A valid byte array was expected.");

            using (var output = new MemoryStream())
            {
                using (var input = new MemoryStream(bytes, false))
                using (var compress = CreateCompressStream(output))
                using (var buffer = new BufferedStream(compress, this.bufferSize))
                {
                    input.CopyTo(buffer);
                }

                return output.ToArray();
            }
        }

        public byte[] Decompress(byte[] bytes)
        {
            Check.NotNull(bytes, "A valid byte array was expected.");

            using (var output = new MemoryStream())
            {
                using (var input = new MemoryStream(bytes, false))
                using (var decompress = CreateDecompressStream(input))
                using (var buffer = new BufferedStream(decompress, this.bufferSize))
                {
                    buffer.CopyTo(output);
                }

                return output.ToArray();
            }
        }

        /// <summary>
        /// Creates a compression <see cref="Stream"/> decorating the given <see cref="Stream"/>.
        /// </summary>
        protected abstract Stream CreateCompressStream(Stream stream);

        /// <summary>
        /// Creates a decompression <see cref="Stream"/> decorating the given <see cref="Stream"/>.
        /// </summary>
        protected abstract Stream CreateDecompressStream(Stream stream);
    }
}