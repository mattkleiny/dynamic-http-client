using System.IO;

namespace DynamicHttpClient.IO.Compression
{
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
      Check.NotNull(bytes, nameof(bytes));

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
      Check.NotNull(bytes, nameof(bytes));

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