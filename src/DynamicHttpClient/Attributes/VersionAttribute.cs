using System;
using DynamicHttpClient.Metadata;

namespace DynamicHttpClient.Attributes
{
  /// <summary>Attaches a <see cref="Version"/> to an HTTP client.</summary>
  public sealed class VersionAttribute : Attribute, IMetadataAware
  {
    private readonly Version version;

    public VersionAttribute(int major, int minor, int revision)
    {
      version = new Version(major, minor, revision);
    }

    public void OnAttachMetadata(RequestMetadata metadata)
    {
      metadata.Headers.Add("Version", version.ToString());
    }
  }
}