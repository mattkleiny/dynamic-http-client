using System;

namespace DynamicHttpClient.IO.Serialization
{
  public interface ISerializer
  {
    string ContentType { get; }

    string Serialize(Type type, object content);
  }
}