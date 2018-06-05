using System;

namespace DynamicHttpClient.Attributes
{
  /// <summary>
  /// Denotes the given parameter should be serialized into the message body.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  public sealed class BodyAttribute : ParameterAttribute
  {
  }
}