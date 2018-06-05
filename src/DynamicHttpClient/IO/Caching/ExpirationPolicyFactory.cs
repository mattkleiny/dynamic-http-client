using System;
using DynamicHttpClient.Caching;

namespace DynamicHttpClient.IO.Caching
{
  internal static class ExpirationPolicyFactory
  {
    public static IExpirationPolicy BuildRelativeExpirationPolicy(TimeSpan interval) => new RelativeExpirationPolicy(interval);
    public static IExpirationPolicy BuildSlidingExpirationPolicy(TimeSpan interval)  => new SlidingExpirationPolicy(interval);

    private sealed class RelativeExpirationPolicy : IExpirationPolicy
    {
      private readonly TimeSpan interval;

      public RelativeExpirationPolicy(TimeSpan interval) => this.interval = interval;

      public CacheSettings BuildCacheSettings(IRequest request) => new CacheSettings
      {
        ExpirationTime = DateTime.Now + interval
      };

      public override string ToString() => $"Relative expiration every {interval}";
    }

    private sealed class SlidingExpirationPolicy : IExpirationPolicy
    {
      private readonly TimeSpan interval;

      public SlidingExpirationPolicy(TimeSpan interval) => this.interval = interval;

      public CacheSettings BuildCacheSettings(IRequest request) => new CacheSettings
      {
        SlidingExpiration = interval
      };

      public override string ToString() => $"Sliding expiration every {interval}";
    }
  }
}