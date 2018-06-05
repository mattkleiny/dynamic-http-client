using DynamicHttpClient.Caching;

namespace DynamicHttpClient.IO.Caching
{
  public interface ICachingPolicy
  {
    string EvaluateCacheKey(IRequest request);
    bool ShouldCache(IResponse response);
    CacheSettings GetCacheSettings(IRequest request);
    IResponse GetCacheableResponse(IResponse response);
  }
}