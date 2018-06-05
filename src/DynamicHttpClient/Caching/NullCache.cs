using System;
using System.Threading.Tasks;

namespace DynamicHttpClient.Caching
{
  public sealed class NullCache : ICache
  {
    public Task<T> GetOrComputeAsync<T>(string key, CacheSettings settings, Func<Task<T>> computeDelegate, Func<T, bool> shouldCachePredicate)
    {
      Check.NotNull(computeDelegate, nameof(computeDelegate));

      return computeDelegate();
    }
  }
}