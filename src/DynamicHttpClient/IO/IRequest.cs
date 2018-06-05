using System.Net.Http;

namespace DynamicHttpClient.IO
{
  public interface IRequest
  {
    string       Url     { get; }
    HttpMethod   Method  { get; }
    IRequestBody Body    { get; }
  }
}