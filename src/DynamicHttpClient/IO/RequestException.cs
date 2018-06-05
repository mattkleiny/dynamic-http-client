using System;
using System.Net;
using System.Runtime.Serialization;

namespace DynamicHttpClient.IO
{
  [Serializable]
  public class RequestException : Exception
  {
    public RequestException(string message, HttpStatusCode statusCode)
      : base(message)
    {
      StatusCode = statusCode;
    }

    public RequestException(string message, HttpStatusCode statusCode, Exception exception)
      : base(message, exception)
    {
      StatusCode = statusCode;
    }

    protected RequestException(SerializationInfo info, StreamingContext context)
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