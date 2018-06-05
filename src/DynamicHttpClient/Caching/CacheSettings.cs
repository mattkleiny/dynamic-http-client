using System;

namespace DynamicHttpClient.Caching
{
  /// <summary>
  /// Settings for inserting an entry into a <see cref="ICache"/>.
  /// </summary>
  public sealed class CacheSettings
  {
    /// <summary>
    /// Reasonable default <see cref="CacheSettings"/>.
    /// </summary>
    public static readonly CacheSettings Default = new CacheSettings();

    private DateTimeOffset? expirationTime;
    private TimeSpan?       slidingExpiration;

    /// <summary>
    /// The absolute expiration time for the associated cache entry.
    /// </summary>
    /// <remarks>This is mutually exclusive with <see cref="SlidingExpiration"/></remarks>
    public DateTimeOffset ExpirationTime
    {
      get { return this.expirationTime ?? DateTimeOffset.MaxValue; }
      set
      {
        Check.That(!this.slidingExpiration.HasValue, "An absolute expiration may not be set if the sliding expiration has been set.");

        this.expirationTime = value;
      }
    }

    /// <summary>
    /// The sliding expiration time for the associated cache entry.
    /// </summary>
    /// <remarks>This is mutually exclusive with <see cref="ExpirationTime"/></remarks>
    public TimeSpan SlidingExpiration
    {
      get { return this.slidingExpiration ?? TimeSpan.Zero; }
      set
      {
        Check.That(!this.expirationTime.HasValue, "A sliding expiration may not be set if the absolute expiration has been set.");

        this.slidingExpiration = value;
      }
    }
  }
}