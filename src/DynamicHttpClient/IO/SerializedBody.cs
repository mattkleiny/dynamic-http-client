using System;
using DynamicHttpClient.IO.Serialization;

namespace DynamicHttpClient.IO
{
  public sealed class SerializedBody : IRequestBody
  {
    public SerializedBody(object body)
      : this(body, new NewtonsoftSerializer())
    {
    }

    public SerializedBody(object body, ISerializer serializer)
      : this(body.GetType(), body, serializer)
    {
    }

    public SerializedBody(Type type, object body, ISerializer serializer)
    {
      Check.NotNull(type,       nameof(type));
      Check.NotNull(body,       nameof(body));
      Check.NotNull(serializer, nameof(serializer));

      Type       = type;
      Body       = body;
      Serializer = serializer;
    }

    public Type        Type       { get; }
    public object      Body       { get; }
    public ISerializer Serializer { get; }

    public string Content     => Serializer.Serialize(Type, Body);
    public string ContentType => Serializer.ContentType;
  }
}