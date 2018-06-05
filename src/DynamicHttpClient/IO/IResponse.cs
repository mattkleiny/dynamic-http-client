using System.Net;
using System.Text;

namespace DynamicHttpClient.IO
{
  public interface IResponse
  {
    byte[]         RawBytes        { get; }
    string         Content         { get; }
    string         ContentType     { get; }
    long           ContentLength   { get; }
    Encoding       ContentEncoding { get; }
    string         Url             { get; }
    HttpStatusCode StatusCode      { get; }
  }
}