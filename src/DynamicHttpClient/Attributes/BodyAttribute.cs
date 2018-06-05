using System;

namespace DynamicHttpClient.Attributes
{
  /// <summary>Denotes the associated paramater is the HTTP body.</summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  public sealed class BodyAttribute : ParameterAttribute
  {
  }
}