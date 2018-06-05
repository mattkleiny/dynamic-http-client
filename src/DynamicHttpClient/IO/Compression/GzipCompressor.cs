using System.IO;
using System.IO.Compression;

namespace DynamicHttpClient.IO.Compression
{
  /// <summary>
  /// A gzip <see cref="StreamingCompressor"/> specialization for GZip.
  /// </summary>
  public sealed class GzipCompressor : StreamingCompressor
  {
    public GzipCompressor()
    {
    }

    public GzipCompressor(int bufferSize)
      : base(bufferSize)
    {
    }

    protected override Stream CreateCompressStream(Stream stream)
    {
      return new GZipStream(stream, CompressionMode.Compress);
    }

    protected override Stream CreateDecompressStream(Stream stream)
    {
      return new GZipStream(stream, CompressionMode.Decompress);
    }
  }
}