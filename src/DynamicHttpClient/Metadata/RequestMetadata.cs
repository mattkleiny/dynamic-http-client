using System;
using System.Collections.Generic;
using System.Diagnostics;
using DynamicHttpClient.IO.Caching;
using DynamicHttpClient.IO.Serialization;

namespace DynamicHttpClient.Metadata
{
  /// <summary>
  /// Encapsulates the metadata for a request.
  /// </summary>
  [DebuggerDisplay("{Method}: {Path}")]
  public sealed class RequestMetadata
  {
    // potentially expensive to have many of these collections lying around the metadata 
    // cache, especially if they have no entries. initialize with 0 capacity and we'll incur 
    // the cost of resizing in the factory.

    /// <summary>
    /// Extra headers to attach to the request.
    /// </summary>
    public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>(0, StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// URL segments to attach to the request.
    /// </summary>
    public ICollection<UrlSegmentMetadata> UrlSegments { get; } = new List<UrlSegmentMetadata>(0);

    /// <summary>
    /// The <see cref="RestMethod"/> to use.
    /// </summary>
    public RestMethod Method { get; set; }

    /// <summary>
    /// The HTTP path to request.
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// The expected result <see cref="Type"/>.
    /// </summary>
    public Type ResultType { get; set; }

    /// <summary>
    /// The associated <see cref="ISerializer"/>.
    /// </summary>
    public ISerializer Serializer { get; set; }

    /// <summary>
    /// The associated <see cref="IDeserializer"/>.
    /// </summary>
    public IDeserializer Deserializer { get; set; }

    /// <summary>
    /// The timeout period for the request, or null if no timeout is required.
    /// </summary>
    public TimeSpan? Timeout { get; set; }

    /// <summary>
    /// True if a response is required for the request.
    /// </summary>
    public bool IsResponseRequired { get; set; }

    /// <summary>
    /// True if the request may run asynchonously, otherwise false.
    /// </summary>
    public bool IsAsynchronous { get; set; }

    /// <summary>
    /// The metadata about the body of the request, or null if there is no body.
    /// </summary>
    public RequestBodyMetadata Body { get; set; }

    /// <summary>
    /// The <see cref="ICachingPolicy"/> for the request, or null if there is no policy.
    /// </summary>
    public ICachingPolicy CachingPolicy { get; set; }
  }
}