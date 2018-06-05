using System;
using DynamicHttpClient.IO.Serialization;

namespace DynamicHttpClient.IO
{
  /// <summary>
  /// A <see cref="IRequestBody"/> that defers the serialization of some body object to a <see cref="ISerializer"/>.
  /// </summary>
  public sealed class SerializedBody : IRequestBody
  {
    /// <param name="body">The object to serialize.</param>
    public SerializedBody(object body)
      : this(body, new NewtonsoftSerializer())
    {
    }

    /// <param name="body">The object to serialize.</param>
    /// <param name="serializer">The <see cref="ISerializer"/> to use.</param>
    public SerializedBody(object body, ISerializer serializer)
      : this(body.GetType(), body, serializer)
    {
    }

    /// <param name="type">The <see cref="System.Type"/> of the <see cref="Body"/>.</param>
    /// <param name="body">The object to serialize.</param>
    /// <param name="serializer">The <see cref="ISerializer"/> to use.</param>
    public SerializedBody(Type type, object body, ISerializer serializer)
    {
      Check.NotNull(type,       nameof(type));
      Check.NotNull(body,       nameof(body));
      Check.NotNull(serializer, nameof(serializer));

      Type       = type;
      Body       = body;
      Serializer = serializer;
    }

    /// <summary>
    /// The <see cref="System.Type"/> of the body object.
    /// </summary>
    public Type Type { get; }

    /// <summary>
    /// The body object itself.
    /// </summary>
    public object Body { get; }

    /// <summary>
    /// The underlying <see cref="ISerializer"/>.
    /// </summary>
    public ISerializer Serializer { get; }

    public string Content => Serializer.Serialize(Type, Body);

    public string ContentType => Serializer.ContentType;
  }
}