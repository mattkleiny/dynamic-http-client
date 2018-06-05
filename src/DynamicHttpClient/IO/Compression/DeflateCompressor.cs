using System.IO;
using System.IO.Compression;

namespace DynamicHttpClient.IO.Compression
{
  public sealed class DeflateCompressor : StreamingCompressor
  {
    public DeflateCompressor()
    {
    }

    public DeflateCompressor(int bufferSize)
      : base(bufferSize)
    {
    }

    protected override Stream CreateCompressStream(Stream stream)   => new DeflateStream(stream, CompressionMode.Compress);
    protected override Stream CreateDecompressStream(Stream stream) => new DeflateStream(stream, CompressionMode.Decompress);
  }
}