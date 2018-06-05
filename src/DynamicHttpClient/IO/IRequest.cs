using System;

namespace DynamicHttpClient.IO
{
  /// <summary>
  /// Abstractly represents a request in the RestClient I/O pipeline.
  /// </summary>
  public interface IRequest
  {
    /// <summary>
    /// The URL the request is to be made against.
    /// </summary>
    string Url { get; }

    /// <summary>
    /// The <see cref="RestMethod"/> to use.
    /// </summary>
    RestMethod Method { get; }

    /// <summary>
    /// The <see cref="IRequestBody"/>, or null if no body is specified.
    /// </summary>
    IRequestBody Body { get; }

    /// <summary>
    /// The timeout for the request.
    /// </summary>
    TimeSpan? Timeout { get; }
  }
}