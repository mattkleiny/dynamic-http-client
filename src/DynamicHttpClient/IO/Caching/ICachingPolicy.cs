using DynamicHttpClient.Caching;

namespace DynamicHttpClient.IO.Caching
{
  /// <summary>
  /// A policy for caching a request.
  /// </summary>
  public interface ICachingPolicy
  {
    /// <summary>
    /// Evaluates a cache key for the given <see cref="IRequest"/>.
    /// </summary>
    string EvaluateCacheKey(IRequest request);

    /// <summary>
    /// Determines if the given <see cref="IResponse"/> is a valid candidate for caching.
    /// </summary>
    bool ShouldCache(IResponse response);

    /// <summary>
    /// Retrieves <see cref="CacheSettings"/> for the given <see cref="IRequest"/>.
    /// </summary>
    CacheSettings GetCacheSettings(IRequest request);

    /// <summary>
    /// Transforms the given <see cref="IResponse"/> into something cacheable.
    /// </summary>
    IResponse GetCacheableResponse(IResponse response);
  }
}