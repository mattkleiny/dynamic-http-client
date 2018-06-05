using System;

namespace DynamicHttpClient.Caching
{
  /// <summary>
  /// A no-op <see cref="ICache"/> implementation.
  /// </summary>
  public sealed class NullCache : ICache
  {
    public T GetOrCompute<T>(string key, CacheSettings settings, Func<T> computeDelegate, Func<T, bool> shouldCachePredicate)
    {
      Check.NotNullOrEmpty(key, nameof(key));
      Check.NotNull(computeDelegate,      nameof(computeDelegate));
      Check.NotNull(shouldCachePredicate, nameof(shouldCachePredicate));

      return computeDelegate(); // always compute
    }
  }
}