using System;
using System.IO;

namespace DynamicHttpClient.IO.Serialization
{
  public interface IDeserializer
  {
    object Deserialize(Type type, TextReader reader);
  }
}