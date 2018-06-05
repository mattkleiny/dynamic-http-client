using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using DynamicHttpClient.IO.Caching;
using DynamicHttpClient.IO.Serialization;

namespace DynamicHttpClient.Metadata
{
  [DebuggerDisplay("{Method}: {Path}")]
  public sealed class RequestMetadata
  {
    public IDictionary<string, string>     Headers     { get; } = new Dictionary<string, string>(0, StringComparer.OrdinalIgnoreCase);
    public ICollection<UrlSegmentMetadata> UrlSegments { get; } = new List<UrlSegmentMetadata>(0);

    public HttpMethod          Method             { get; set; }
    public string              Path               { get; set; }
    public Type                ResultType         { get; set; }
    public ISerializer         Serializer         { get; set; }
    public IDeserializer       Deserializer       { get; set; }
    public bool                IsResponseRequired { get; set; }
    public bool                IsAsynchronous     { get; set; }
    public RequestBodyMetadata Body               { get; set; }
    public ICachingPolicy      CachingPolicy      { get; set; }
  }
}