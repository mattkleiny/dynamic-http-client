using System.Diagnostics;

namespace DynamicHttpClient.Metadata
{
  /// <summary>
  /// Encapsulates the metadata for a URL segment.
  /// </summary>
  [DebuggerDisplay("{Index}: {Name}")]
  public sealed class UrlSegmentMetadata
  {
    /// <summary>
    /// The index of the segment in the source method.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// The name of the segment.
    /// </summary>
    public string Name { get; set; }
  }
}