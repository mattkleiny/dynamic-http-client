using System;
using System.Collections.Generic;
using System.Net.Http;

namespace DynamicHttpClient.IO
{
  public class RequestBuilder<TRequest> : IRequestBuilder
    where TRequest : AbstractRequest, new()
  {
    public string                      Path     { get; set; }
    public HttpMethod                  Method   { get; set; }
    public IRequestBody                Body     { get; set; }
    public IDictionary<string, string> Headers  { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
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

      return request;
    }
  }
}