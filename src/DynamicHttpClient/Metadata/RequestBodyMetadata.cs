using System;
using System.Diagnostics;

namespace DynamicHttpClient.Metadata
{
  [DebuggerDisplay("{Index}: {Type}")]
  public sealed class RequestBodyMetadata
  {
    public int  Index { get; set; }
    public Type Type  { get; set; }
  }
}