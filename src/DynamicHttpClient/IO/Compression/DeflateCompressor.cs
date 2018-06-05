using System.IO;
using System.IO.Compression;

namespace DynamicHttpClient.IO.Compression
{
  /// <summary>
  /// A <see cref="StreamingCompressor"/> specialization for Deflate.
  /// </summary>
  public sealed class DeflateCompressor : StreamingCompressor
  {
    public DeflateCompressor()
    {
    }

    public DeflateCompressor(int bufferSize)
      : base(bufferSize)
    {
    }

    protected override Stream CreateCompressStream(Stream stream)
    {
      return new DeflateStream(stream, CompressionMode.Compress);
    }

    protected override Stream CreateDecompressStream(Stream stream)
    {
      return new DeflateStream(stream, CompressionMode.Decompress);
    }
  }
}