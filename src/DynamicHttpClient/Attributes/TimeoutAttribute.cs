using System;
using DynamicHttpClient.Metadata;
using DynamicHttpClient.Utilities;

namespace DynamicHttpClient.Attributes
{
  public sealed class TimeoutAttribute : Attribute, IMetadataAware
  {
    private readonly TimeSpan timeout;

    public TimeoutAttribute(int interval, TimeScale scale)
    {
      timeout = TimeScaleHelpers.BuildTimeSpan(interval, scale);
    }

    public void OnAttachMetadata(RequestMetadata metadata)
    {
      metadata.Timeout = timeout;
    }
  }
}