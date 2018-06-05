using System;
using DynamicHttpClient.Metadata;

namespace DynamicHttpClient.Attributes
{
  /// <summary>
  /// A <see cref="Attribute"/> which denotes the version of a client.
  /// </summary>
  public sealed class VersionAttribute : Attribute, IMetadataAware
  {
    private readonly Version version;

    public VersionAttribute(int major, int minor, int revision)
    {
      this.version = new Version(major, minor, revision);
    }

    public void OnAttachMetadata(RequestMetadata metadata)
    {
      metadata.Headers.Add("Version", this.version.ToString());
    }
  }
}