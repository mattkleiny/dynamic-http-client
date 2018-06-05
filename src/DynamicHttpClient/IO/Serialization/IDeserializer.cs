using System;
using System.IO;

namespace DynamicHttpClient.IO.Serialization
{
  /// <summary>
  /// Represents a utility that deserializes objects from text.
  /// </summary>
  public interface IDeserializer
  {
    /// <summary>
    /// Deserializes an object from the given <see cref="TextReader"/> of the given <see cref="Type"/>.
    /// </summary>
    object Deserialize(Type type, TextReader reader);
  }
}