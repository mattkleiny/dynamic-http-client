using System;
using DynamicHttpClient.IO.Caching;
using DynamicHttpClient.Utilities;

namespace DynamicHttpClient.Caching
{
  /// <summary>
  /// A <see cref="CachingAttribute"/> that describes a sliding expiration policy.
  /// </summary>
  public sealed class SlidingCacheAttribute : CachingAttribute
  {
    private readonly TimeSpan interval;

    public SlidingCacheAttribute(int interval, TimeScale scale)
    {
      this.interval = TimeScaleHelpers.BuildTimeSpan(interval, scale);
    }

    protected override IExpirationPolicy ExpirationPolicy => ExpirationPolicyFactory.BuildSlidingExpirationPolicy(this.interval);
  }
}