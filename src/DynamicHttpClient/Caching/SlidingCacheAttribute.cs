using System;
using DynamicHttpClient.IO.Caching;
using DynamicHttpClient.Utilities;

namespace DynamicHttpClient.Caching
{
  /// <summary>Denotes the associated HTTP invocation should be cached using a sliding <see cref="IExpirationPolicy"/>.</summary>
  public sealed class SlidingCacheAttribute : CachingAttribute
  {
    private readonly TimeSpan interval;

    public SlidingCacheAttribute(int interval, TimeScale scale)
    {
      this.interval = TimeScaleHelpers.BuildTimeSpan(interval, scale);
    }

    protected override IExpirationPolicy ExpirationPolicy => ExpirationPolicyFactory.BuildSlidingExpirationPolicy(interval);
  }
}