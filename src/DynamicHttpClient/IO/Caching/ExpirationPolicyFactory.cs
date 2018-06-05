using System;
using DynamicHttpClient.Caching;

namespace DynamicHttpClient.IO.Caching
{
  /// <summary>
  /// Static factories for <see cref="IExpirationPolicy"/>s.
  /// </summary>
  internal static class ExpirationPolicyFactory
  {
    /// <summary>
    /// Builds a <see cref="IExpirationPolicy"/> that defines a relative expiration interval.
    /// </summary>
    /// <param name="interval">The relative interval.</param>
    public static IExpirationPolicy BuildRelativeExpirationPolicy(TimeSpan interval)
    {
      return new RelativeExpirationPolicy(interval);
    }

    /// <summary>
    /// Builds a <see cref="IExpirationPolicy"/> that defines a sliding expiration interval.
    /// </summary>
    /// <param name="interval">The sliding interval.</param>
    public static IExpirationPolicy BuildSlidingExpirationPolicy(TimeSpan interval)
    {
      return new SlidingExpirationPolicy(interval);
    }

    /// <summary>
    /// A <see cref="IExpirationPolicy"/> that defines a relative expiration interval.
    /// </summary>
    private sealed class RelativeExpirationPolicy : IExpirationPolicy
    {
      private readonly TimeSpan interval;

      public RelativeExpirationPolicy(TimeSpan interval)
      {
        this.interval = interval;
      }

      public CacheSettings BuildCacheSettings(IRequest request)
      {
        return new CacheSettings
        {
          ExpirationTime = DateTime.Now + this.interval
        };
      }

      public override string ToString()
      {
        return $"Relative expiration every {this.interval}";
      }
    }

    /// <summary>
    /// A <see cref="IExpirationPolicy"/> that defines a sliding expiration policy.
    /// </summary>
    private sealed class SlidingExpirationPolicy : IExpirationPolicy
    {
      private readonly TimeSpan interval;

      public SlidingExpirationPolicy(TimeSpan interval)
      {
        this.interval = interval;
      }

      public CacheSettings BuildCacheSettings(IRequest request)
      {
        return new CacheSettings
        {
          SlidingExpiration = this.interval
        };
      }

      public override string ToString()
      {
        return $"Sliding expiration every {this.interval}";
      }
    }
  }
}