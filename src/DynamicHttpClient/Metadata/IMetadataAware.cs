using System;

namespace DynamicHttpClient.Metadata
{
  public interface IMetadataAware
  {
    void OnAttachMetadata(RequestMetadata metadata);
  }
}