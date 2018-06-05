using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace DynamicHttpClient.IO.Serialization
{
  public sealed class NewtonsoftSerializer : ISerializer
  {
    private readonly JsonSerializer serializer = JsonSerializer.CreateDefault(SerializationSettings.Default);

    public string ContentType => "application/json";

    public string Serialize(Type type, object content)
    {
      Check.NotNull(content, nameof(content));

      var builder = new StringBuilder();

      using (var writer = new StringWriter(builder))
      {
        serializer.Serialize(writer, content, type);
      }

      return builder.ToString();
    }
  }
}