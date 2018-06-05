using System.IO;

namespace DynamicHttpClient.IO.Compression
{
  public abstract class StreamingCompressor : ICompressor
  {
    private readonly int bufferSize;

    protected StreamingCompressor()
      : this(4096)
    {
    }

    protected StreamingCompressor(int bufferSize)
    {
      Check.That(bufferSize > 0, "bufferSize > 0");

      this.bufferSize = bufferSize;
    }

    public byte[] Compress(byte[] bytes)
    {
      Check.NotNull(bytes, nameof(bytes));

      using (var output = new MemoryStream())
      {
        using (var input = new MemoryStream(bytes, false))
        using (var compress = CreateCompressStream(output))
        using (var buffer = new BufferedStream(compress, bufferSize))
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
        using (var buffer = new BufferedStream(decompress, bufferSize))
        {
          buffer.CopyTo(output);
        }

        return output.ToArray();
      }
    }

    protected abstract Stream CreateCompressStream(Stream stream);
    protected abstract Stream CreateDecompressStream(Stream stream);
  }
}