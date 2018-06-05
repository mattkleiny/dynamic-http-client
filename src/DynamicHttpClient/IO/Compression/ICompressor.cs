namespace DynamicHttpClient.IO.Compression
{
  public interface ICompressor
  {
    byte[] Compress(byte[] rawBytes);
    byte[] Decompress(byte[] compressedBytes);
  }
}