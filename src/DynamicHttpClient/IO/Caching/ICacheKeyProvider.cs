namespace DynamicHttpClient.IO.Caching
{
  /// <summary>
  /// A custom provider for cache key information.
  /// </summary>
  public interface ICacheKeyProvider
  {
    /// <summary>
    /// The relevant cache key.
    /// </summary>
    string CacheKey { get; }
  }
}