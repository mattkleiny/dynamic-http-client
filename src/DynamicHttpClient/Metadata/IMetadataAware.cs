using System;

namespace DynamicHttpClient.Metadata
{
  /// <summary>
  /// Represents an <see cref="Attribute"/> which contributes to <see cref="RequestMetadata"/>.
  /// </summary>
  public interface IMetadataAware
  {
    /// <summary>
    /// Attaches metadata to the given <see cref="RequestMetadata"/> object.
    /// </summary>
    void OnAttachMetadata(RequestMetadata metadata);
  }
}