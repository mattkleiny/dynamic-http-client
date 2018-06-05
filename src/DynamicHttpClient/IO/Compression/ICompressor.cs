namespace DynamicHttpClient.IO.Compression
{
  /// <summary>
  /// Represents a utility that compresses bytes.
  /// </summary>
  public interface ICompressor
  {
    /// <summary>
    /// Compresses bytes.
    /// </summary>
    byte[] Compress(byte[] rawBytes);

    /// <summary>
    /// Decompresses bytes.
    /// </summary>
    byte[] Decompress(byte[] compressedBytes);
  }
}