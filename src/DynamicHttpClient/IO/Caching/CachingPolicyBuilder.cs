using System.Net;
using DynamicHttpClient.Caching;

namespace DynamicHttpClient.IO.Caching
{
  /// <summary>
  /// A builder for <see cref="ICachingPolicy"/>s.
  /// </summary>
  internal sealed class CachingPolicyBuilder
  {
    /// <summary>
    /// The desired <see cref="IExpirationPolicy"/>.
    /// </summary>
    public IExpirationPolicy ExpirationPolicy { get; set; }

    /// <summary>
    /// The desired <see cref="CachedRepresentation"/>.
    /// </summary>
    public CachedRepresentation CachedRepresentation { get; set; }

    /// <summary>
    /// Builds the resultant <see cref="ICachingPolicy"/>.
    /// </summary>
    public ICachingPolicy Build()
    {
      return new CachingPolicy(ExpirationPolicy, CachedRepresentation);
    }

    /// <summary>
    /// The <see cref="ICachingPolicy"/> implementation.
    /// </summary>
    private sealed class CachingPolicy : ICachingPolicy
    {
      private readonly CachedRepresentation cachedRepresentation;
      private readonly IExpirationPolicy    expirationPolicy;

      /// <param name="expirationPolicy">The desired <see cref="IExpirationPolicy"/>.</param>
      /// <param name="cachedRepresentation">The desired <see cref="CachedRepresentation"/>.</param>
      public CachingPolicy(IExpirationPolicy expirationPolicy, CachedRepresentation cachedRepresentation)
      {
        this.expirationPolicy     = expirationPolicy;
        this.cachedRepresentation = cachedRepresentation;
      }

      public string EvaluateCacheKey(IRequest request)
      {
        Check.NotNull(request, nameof(request));

        if (request is ICacheKeyProvider)
        {
          return (request as ICacheKeyProvider).CacheKey;
        }

        return request.Url;
      }

      public bool ShouldCache(IResponse response)
      {
        Check.NotNull(response, nameof(response));

        // don't cache items if they aren't found
        return response.StatusCode != HttpStatusCode.NotFound;
      }

      public IResponse GetCacheableResponse(IResponse response)
      {
        Check.NotNull(response, nameof(response));

        return CacheableResponseFactory.BuildCachedResponse(this.cachedRepresentation, response);
      }

      public CacheSettings GetCacheSettings(IRequest request)
      {
        Check.NotNull(request, nameof(request));

        return this.expirationPolicy.BuildCacheSettings(request);
      }

      public override string ToString()
      {
        return $"{this.expirationPolicy} represented as {this.cachedRepresentation}";
      }
    }
  }
}