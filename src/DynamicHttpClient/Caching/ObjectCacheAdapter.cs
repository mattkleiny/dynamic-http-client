using System;
using System.Threading;
using DynamicHttpClient.Utilities;

namespace DynamicHttpClient.Caching
{
  /// <summary>
  /// A <see cref="ICache"/> implementation that adapts the <see cref="ObjectCache"/> from System.Runtime.Caching.
  /// </summary>
  public sealed class ObjectCacheAdapter : ICache
  {
    private readonly ObjectCache cache;

    /// <summary>
    /// A <see cref="ReaderWriterLockSlim"/> for serializing write access to <see cref="GetOrCompute{T}"/>.
    /// </summary>
    private readonly ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();

    /// <param name="cache">The <see cref="cache"/> to adapt.</param>
    public ObjectCacheAdapter(ObjectCache cache)
    {
      Check.NotNull(cache, nameof(cache));

      this.cache = cache;
    }

    public T GetOrCompute<T>(string key, CacheSettings settings, Func<T> computeDelegate, Func<T, bool> shouldCachePredicate)
    {
      Check.NotNullOrEmpty(key, nameof(key));
      Check.NotNull(computeDelegate,      nameof(computeDelegate));
      Check.NotNull(shouldCachePredicate, nameof(shouldCachePredicate));

      // try quick read without serializing access
      using (this.cacheLock.ScopedReadLock())
      {
        var result = (T) this.cache.Get(key);
        if (result != null)
        {
          return result;
        }
      }

      using (this.cacheLock.ScopedUpgradeableReadLock())
      {
        // second attempt, in case another thread has already acquired the resource
        var result = (T) this.cache.Get(key);
        if (result != null)
        {
          return result;
        }

        // serialize access, acquire and insert resource
        using (this.cacheLock.ScopedWriteLock())
        {
          var value = computeDelegate();

          if (shouldCachePredicate(value))
          {
            this.cache.Add(key, value, ConvertToItemPolicy(settings));
          }

          return value;
        }
      }
    }

    /// <summary>
    /// Converts a <see cref="CacheSettings"/> to a <see cref="CacheItemPolicy"/>.
    /// </summary>
    private static CacheItemPolicy ConvertToItemPolicy(CacheSettings settings)
    {
      return new CacheItemPolicy
      {
        AbsoluteExpiration = settings.ExpirationTime,
        SlidingExpiration  = settings.SlidingExpiration
      };
    }
  }
}