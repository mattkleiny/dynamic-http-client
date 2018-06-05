using DynamicHttpClient.Caching;

namespace DynamicHttpClient.IO.Caching
{
  public interface IExpirationPolicy
  {
    CacheSettings BuildCacheSettings(IRequest request);
  }
}