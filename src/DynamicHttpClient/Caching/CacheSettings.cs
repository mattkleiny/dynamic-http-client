using System;

namespace DynamicHttpClient.Caching
{
  public sealed class CacheSettings
  {
    public static readonly CacheSettings Default = new CacheSettings();

    private DateTimeOffset? expirationTime;
    private TimeSpan?       slidingExpiration;

    /// <summary>The absolute expiration time for the associated cache entry.</summary>
    /// <remarks>This is mutually exclusive with <see cref="SlidingExpiration"/></remarks>
    public DateTimeOffset ExpirationTime
    {
      get => expirationTime ?? DateTimeOffset.MaxValue;
      set
      {
        Check.That(!slidingExpiration.HasValue, "An absolute expiration may not be set if the sliding expiration has been set.");

        expirationTime = value;
      }
    }

    /// <summary>The sliding expiration time for the associated cache entry.</summary>
    /// <remarks>This is mutually exclusive with <see cref="ExpirationTime"/></remarks>
    public TimeSpan SlidingExpiration
    {
      get => slidingExpiration ?? TimeSpan.Zero;
      set
      {
        Check.That(!expirationTime.HasValue, "A sliding expiration may not be set if the absolute expiration has been set.");

        slidingExpiration = value;
      }
    }
  }
}