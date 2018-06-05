using System.Net;
using DynamicHttpClient.Caching;

namespace DynamicHttpClient.IO.Caching
{
  internal sealed class CachingPolicyBuilder
  {
    public IExpirationPolicy    ExpirationPolicy     { get; set; }
    public CachedRepresentation CachedRepresentation { get; set; }

    public ICachingPolicy Build() => new CachingPolicy(ExpirationPolicy, CachedRepresentation);

    private sealed class CachingPolicy : ICachingPolicy
    {
      private readonly CachedRepresentation cachedRepresentation;
      private readonly IExpirationPolicy    expirationPolicy;

      public CachingPolicy(IExpirationPolicy expirationPolicy, CachedRepresentation cachedRepresentation)
      {
        Check.NotNull(expirationPolicy, nameof(expirationPolicy));

        this.expirationPolicy     = expirationPolicy;
        this.cachedRepresentation = cachedRepresentation;
      }

      public string EvaluateCacheKey(IRequest request)
      {
        Check.NotNull(request, nameof(request));

        if (request is ICacheKeyProvider provider)
        {
          return provider.CacheKey;
        }

        return request.Url;
      }

      public bool ShouldCache(IResponse response)
      {
        Check.NotNull(response, nameof(response));

        return response.StatusCode != HttpStatusCode.NotFound;
      }

      public IResponse GetCacheableResponse(IResponse response)
      {
        Check.NotNull(response, nameof(response));

        return CacheableResponseFactory.BuildCachedResponse(cachedRepresentation, response);
      }

      public CacheSettings GetCacheSettings(IRequest request)
      {
        Check.NotNull(request, nameof(request));

        return expirationPolicy.BuildCacheSettings(request);
      }

      public override string ToString() => $"{expirationPolicy} represented as {cachedRepresentation}";
    }
  }
}