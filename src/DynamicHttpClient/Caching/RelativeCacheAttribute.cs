using System;
using DynamicHttpClient.IO.Caching;
using DynamicHttpClient.Utilities;

namespace DynamicHttpClient.Caching
{
  public sealed class RelativeCacheAttribute : CachingAttribute
  {
    private readonly TimeSpan interval;

    public RelativeCacheAttribute(int interval, TimeScale scale)
    {
      this.interval = TimeScaleHelpers.BuildTimeSpan(interval, scale);
    }

    protected override IExpirationPolicy ExpirationPolicy => ExpirationPolicyFactory.BuildRelativeExpirationPolicy(interval);
  }
}