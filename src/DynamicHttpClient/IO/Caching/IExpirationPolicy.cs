using DynamicHttpClient.Caching;

namespace DynamicHttpClient.IO.Caching
{
  /// <summary>
  /// Defines a policy for building expiration settings for a <see cref="IRequest"/>.
  /// </summary>
  public interface IExpirationPolicy
  {
    /// <summary>
    /// Builds the <see cref="CacheSettings"/> for the given <see cref="IRequest"/>.
    /// </summary>
    CacheSettings BuildCacheSettings(IRequest request);
  }
}