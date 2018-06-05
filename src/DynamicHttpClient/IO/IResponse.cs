using System.Net;
using System.Text;

namespace DynamicHttpClient.IO
{
  /// <summary>
  /// Abstractly represents a response in the RestClient I/O pipeline.
  /// </summary>
  public interface IResponse
  {
    /// <summary>
    /// The raw bytes of the response.
    /// </summary>
    byte[] RawBytes { get; }

    /// <summary>
    /// The decoded <see cref="RawBytes"/> from the response.
    /// </summary>
    string Content { get; }

    /// <summary>
    /// The content type.
    /// </summary>
    string ContentType { get; }

    /// <summary>
    /// The size of the response, in bytes.
    /// </summary>
    long ContentLength { get; }

    /// <summary>
    /// The <see cref="Encoding"/> of the content.
    /// </summary>
    Encoding ContentEncoding { get; }

    /// <summary>
    /// The URL the response was received from.
    /// </summary>
    string Url { get; }

    /// <summary>
    /// The <see cref="HttpStatusCode"/> of the response.
    /// </summary>
    HttpStatusCode StatusCode { get; }
  }
}