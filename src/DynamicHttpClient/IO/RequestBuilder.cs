using System;
using System.Collections.Generic;

namespace DynamicHttpClient.IO
{
  /// <summary>
  /// A builder for <see cref="AbstractRequest"/> implementations.
  /// </summary>
  public class RequestBuilder<TRequest> : IRequestBuilder
    where TRequest : AbstractRequest, new()
  {
    public string Path { get; set; }

    public RestMethod Method { get; set; }

    public IRequestBody Body { get; set; }

    public TimeSpan? Timeout { get; set; }

    public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    public IDictionary<string, string> Segments { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    public virtual IRequest Build()
    {
      var request = new TRequest
      {
        Url    = Path,
        Method = Method,
        Body   = Body
      };

      foreach (var header in Headers)
      {
        request.Headers.Add(header);
      }

      foreach (var segment in Segments)
      {
        request.Segments.Add(segment);
      }

      request.Timeout = Timeout;

      return request;
    }
  }
}