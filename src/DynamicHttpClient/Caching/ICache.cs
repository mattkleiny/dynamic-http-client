using System;
using System.Threading.Tasks;

namespace DynamicHttpClient.Caching
{
  /// <summary>Represents a cache generically.</summary>
  public interface ICache
  {
    /// <summary>
    /// Attempts to retrieve an item with the given <see cref="key"/> from the cache.
    /// If the item does not exist the <see cref="computeDelegate"/> is used to retrieve it.  
    /// <para/>
    /// The <see cref="shouldCachePredicate"/> is invoked after the <see cref="computeDelegate"/> to determine if the result is valid for caching.
    /// </summary>
    /// <remarks>This implementation should be thread-safe.</remarks>
    Task<T> GetOrComputeAsync<T>(string key, CacheSettings settings, Func<Task<T>> computeDelegate, Func<T, bool> shouldCachePredicate);
  }
}