using System;

namespace DynamicHttpClient.Metadata
{
  [Serializable]
  public class InvalidMetadataException : Exception
  {
    public InvalidMetadataException(string message)
      : base(message)
    {
    }
  }
}