namespace DynamicHttpClient.IO.Caching
{
  public interface ICacheKeyProvider
  {
    string CacheKey { get; }
  }
}