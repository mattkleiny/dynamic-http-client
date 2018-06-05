using System;
using DynamicHttpClient.Metadata;

namespace DynamicHttpClient.Attributes
{
  /// <summary>
  /// A <see cref="Attribute"/> which adds a fixed header value to requests.
  /// </summary>
  [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
  public sealed class HeaderAttribute : Attribute, IMetadataAware
  {
    private readonly string key;
    private readonly string value;

    public HeaderAttribute(string key, string value)
    {
      Check.NotNullOrEmpty(key,   nameof(key));
      Check.NotNullOrEmpty(value, nameof(value));

      this.key   = key;
      this.value = value;
    }

    public void OnAttachMetadata(RequestMetadata metadata)
    {
      metadata.Headers.Add(this.key, this.value);
    }
  }
}