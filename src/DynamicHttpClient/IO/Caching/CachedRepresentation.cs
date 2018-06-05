namespace DynamicHttpClient.IO.Caching
{
  /// <summary>
  /// The desired representation type for the resultant cached entries.
  /// </summary>
  public enum CachedRepresentation
  {
    /// <summary>
    /// An unchanged <see cref="IResponse"/> representation.
    /// </summary>
    Normal,

    /// <summary>
    /// A compressed <see cref="IResponse"/> with only content data.
    /// </summary>
    Compressed
  }
}