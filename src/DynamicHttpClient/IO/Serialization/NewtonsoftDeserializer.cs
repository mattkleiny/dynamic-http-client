using System;
using System.IO;
using Newtonsoft.Json;

namespace DynamicHttpClient.IO.Serialization
{
  public sealed class NewtonsoftDeserializer : IDeserializer
  {
    private readonly JsonSerializer serializer = JsonSerializer.CreateDefault(SerializationSettings.Default);

    public object Deserialize(Type type, TextReader reader)
    {
      Check.NotNull(type,   nameof(type));
      Check.NotNull(reader, nameof(reader));

      return serializer.Deserialize(reader, type);
    }
  }
}