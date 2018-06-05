using System;
using System.Collections.Generic;
using System.Net.Http;

namespace DynamicHttpClient.IO
{
  public interface IRequestBuilder
  {
    string                      Path     { get; set; }
    HttpMethod                  Method   { get; set; }
    IRequestBody                Body     { get; set; }
    TimeSpan?                   Timeout  { get; set; }
    IDictionary<string, string> Headers  { get; }
    IDictionary<string, string> Segments { get; }

    IRequest Build();
  }
}