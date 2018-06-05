using System;
using System.Collections.Generic;

namespace DynamicHttpClient.IO
{
  /// <summary>
  /// Represents a builder pattern for <see cref="IRequest"/>s.
  /// </summary>
  public interface IRequestBuilder
  {
    /// <summary>
    /// The HTTP path to request.
    /// </summary>
    string Path { get; set; }

    /// <summary>
    /// The <see cref="RestMethod"/> to use.
    /// </summary>
    RestMethod Method { get; set; }

    /// <summary>
    /// The <see cref="IRequestBody"/>, or null.
    /// </summary>
    IRequestBody Body { get; set; }

    /// <summary>
    /// The timeout for the request, or null to use the default timeout.
    /// </summary>
    TimeSpan? Timeout { get; set; }

    /// <summary>
    /// The HTTP headers to add to the request.
    /// </summary>
    IDictionary<string, string> Headers { get; }

    /// <summary>
    /// The URL segments to add to the request.
    /// </summary>
    IDictionary<string, string> Segments { get; }

    /// <summary>
    /// The resultant <see cref="IRequest"/>.
    /// </summary>
    IRequest Build();
  }
}