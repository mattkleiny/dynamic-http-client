using System;
using System.Threading.Tasks;

namespace DynamicHttpClient.Caching
{
  /// <summary>A no-op <see cref="ICache"/> implementation.</summary>
  public sealed class NullCache : ICache
  {
    public Task<T> GetOrComputeAsync<T>(string key, CacheSettings settings, Func<Task<T>> computeDelegate, Func<T, bool> shouldCachePredicate)
    {
      Check.NotNull(computeDelegate, nameof(computeDelegate));

      return computeDelegate(); // always compute 
    }
  }
}