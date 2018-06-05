using System;
using System.IO;
using System.Text;

namespace DynamicHttpClient.IO.Serialization
{
  /// <summary>
  /// A <see cref="ISerializer"/> that uses Newtonsoft's JSON serializer.
  /// </summary>
  public sealed class NewtonsoftSerializer : ISerializer
  {
    private readonly JsonSerializer serializer = JsonSerializer.CreateDefault(SerializationConstants.DefaultSerializerSettings);

    public string ContentType => "application/json";

    public string Serialize(Type type, object content)
    {
      Check.NotNull(content, nameof(content));

      var builder = new StringBuilder();

      using (var writer = new StringWriter(builder))
      {
        this.serializer.Serialize(writer, content, type);
      }

      return builder.ToString();
    }
  }
}