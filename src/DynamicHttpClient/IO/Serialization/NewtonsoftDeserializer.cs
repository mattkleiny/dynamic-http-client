using System;
using System.IO;

namespace DynamicHttpClient.IO.Serialization
{
  /// <summary>
  /// A <see cref="IDeserializer"/> that uses Newtonsoft's JSON deserializer.
  /// </summary>
  public sealed class NewtonsoftDeserializer : IDeserializer
  {
    private readonly JsonSerializer serializer = JsonSerializer.CreateDefault(SerializationConstants.DefaultSerializerSettings);

    public object Deserialize(Type type, TextReader reader)
    {
      Check.NotNull(type,   nameof(type));
      Check.NotNull(reader, nameof(reader));

      return this.serializer.Deserialize(reader, type);
    }
  }
}