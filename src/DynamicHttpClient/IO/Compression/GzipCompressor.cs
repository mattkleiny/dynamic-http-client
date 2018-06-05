using System.IO;
using System.IO.Compression;

namespace DynamicHttpClient.IO.Compression
{
  public sealed class GzipCompressor : StreamingCompressor
  {
    public GzipCompressor()
    {
    }

    public GzipCompressor(int bufferSize)
      : base(bufferSize)
    {
    }

    protected override Stream CreateCompressStream(Stream stream)   => new GZipStream(stream, CompressionMode.Compress);
    protected override Stream CreateDecompressStream(Stream stream) => new GZipStream(stream, CompressionMode.Decompress);
  }
}