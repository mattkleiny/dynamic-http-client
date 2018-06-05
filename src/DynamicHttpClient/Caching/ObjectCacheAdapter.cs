using System;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;
using DynamicHttpClient.Utilities;

namespace DynamicHttpClient.Caching
{
  public sealed class ObjectCacheAdapter : ICache
  {
    private readonly ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
    private readonly ObjectCache          cache;

    public ObjectCacheAdapter(ObjectCache cache)
    {
      Check.NotNull(cache, nameof(cache));

      this.cache = cache;
    }

    public async Task<T> GetOrComputeAsync<T>(string key, CacheSettings settings, Func<Task<T>> computeDelegate, Func<T, bool> shouldCachePredicate)
    {
      Check.NotNullOrEmpty(key, nameof(key));
      Check.NotNull(computeDelegate,      nameof(computeDelegate));
      Check.NotNull(shouldCachePredicate, nameof(shouldCachePredicate));

      // try quick read without serializing access
      using (cacheLock.ScopedReadLock())
      {
        var result = (T) cache.Get(key);
        if (result != null)
        {
          return result;
        }
      }

      using (cacheLock.ScopedUpgradeableReadLock())
      {
        // second attempt, in case another thread has already acquired the resource
        var result = (T) cache.Get(key);
        if (result != null)
        {
          return result;
        }

        // serialize access, acquire and insert resource
        using (cacheLock.ScopedWriteLock())
        {
          var value = await computeDelegate();

          if (shouldCachePredicate(value))
          {
            cache.Add(key, value, new CacheItemPolicy
            {
              AbsoluteExpiration = settings.ExpirationTime,
              SlidingExpiration  = settings.SlidingExpiration
            });
          }

          return value;
        }
      }
    }
  }
}