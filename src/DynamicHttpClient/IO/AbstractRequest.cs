using System;
using System.Collections.Generic;
using System.Net.Http;

namespace DynamicHttpClient.IO
{
  public abstract class AbstractRequest : IRequest
  {
    public IDictionary<string, string> Headers  { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    public IDictionary<string, string> Segments { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    public string                      Url      { get; set; }
    public HttpMethod                  Method   { get; set; }
    public IRequestBody                Body     { get; set; }
  }
}