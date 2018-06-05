using System.Diagnostics;

namespace DynamicHttpClient.Metadata
{
  [DebuggerDisplay("{Index}: {Name}")]
  public sealed class UrlSegmentMetadata
  {
    public int Index { get; set; }
    public string Name { get; set; }
  }
}