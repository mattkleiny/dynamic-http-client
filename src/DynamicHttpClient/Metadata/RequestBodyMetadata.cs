using System;
using System.Diagnostics;

namespace DynamicHttpClient.Metadata
{
  /// <summary>
  /// Encapsulates the metadata for a request body.
  /// </summary>
  [DebuggerDisplay("{Index}: {Type}")]
  public sealed class RequestBodyMetadata
  {
    /// <summary>
    /// The index of the body in the source method.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// The type of the body.
    /// </summary>
    public Type Type { get; set; }
  }
}