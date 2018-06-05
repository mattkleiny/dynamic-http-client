using System;
using DynamicHttpClient.Metadata;
using DynamicHttpClient.Utilities;

namespace DynamicHttpClient.Attributes
{
  /// <summary>
  /// A <see cref="Attribute"/> which associates a timeout with a method in a RestClient implementation.
  /// </summary>
  public sealed class TimeoutAttribute : Attribute, IMetadataAware
  {
    private readonly TimeSpan timeout;

    public TimeoutAttribute(int interval, TimeScale scale)
    {
      this.timeout = TimeScaleHelpers.BuildTimeSpan(interval, scale);
    }

    public void OnAttachMetadata(RequestMetadata metadata)
    {
      metadata.Timeout = this.timeout;
    }
  }
}