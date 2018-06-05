using System;
using DynamicHttpClient.Metadata;

namespace DynamicHttpClient.Attributes
{
  /// <summary>Attaches a static HTTP header to an HTTP request.</summary>
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
      metadata.Headers.Add(key, value);
    }
  }
}