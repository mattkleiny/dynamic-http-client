using System;

namespace DynamicHttpClient.Metadata
{
  /// <summary>
  /// Occurs if there is an error in the metadata used to construct a proxy.
  /// </summary>
  [Serializable]
  public class InvalidMetadataException : Exception
  {
    public InvalidMetadataException(string message)
      : base(message)
    {
    }
  }
}