using System;

namespace DynamicHttpClient.IO.Serialization
{
  /// <summary>
  /// Represents a utility that serializes objects from text.
  /// </summary>
  public interface ISerializer
  {
    /// <summary>
    /// The content type/mime type of the resultant serialized format.
    /// </summary>
    string ContentType { get; }

    /// <summary>
    /// Serializes an object of the given type to text.
    /// </summary>
    string Serialize(Type type, object content);
  }
}