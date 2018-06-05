using System;
using System.Net;
using System.Runtime.Serialization;

namespace DynamicHttpClient.IO
{
  /// <summary>
  /// Encapsulates an exception from a <see cref="IRequestExecutor"/>.
  /// </summary>
  [Serializable]
  public class RequestExecutorException : Exception
  {
    public RequestExecutorException(string message, HttpStatusCode statusCode)
      : base(message)
    {
      StatusCode = statusCode;
    }

    public RequestExecutorException(string message, HttpStatusCode statusCode, Exception exception)
      : base(message, exception)
    {
      StatusCode = statusCode;
    }

    protected RequestExecutorException(SerializationInfo info, StreamingContext context)
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