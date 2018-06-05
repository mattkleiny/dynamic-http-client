using System;
using System.Net;
using System.Runtime.Serialization;

namespace DynamicHttpClient.IO
{
  [Serializable]
  public sealed class HttpRequestException : Exception
  {
    public HttpRequestException(string message, HttpStatusCode statusCode)
      : base(message)
    {
      StatusCode = statusCode;
    }

    protected HttpRequestException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      StatusCode = (HttpStatusCode) info.GetValue("StatusCode", typeof(HttpStatusCode));
    }

    public HttpStatusCode StatusCode { get; }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);

      info.AddValue("StatusCode", StatusCode, typeof(HttpStatusCode));
    }
  }
}