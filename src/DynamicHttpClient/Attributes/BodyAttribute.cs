using System;

namespace DynamicHttpClient.Attributes
{
  [AttributeUsage(AttributeTargets.Parameter)]
  public sealed class BodyAttribute : ParameterAttribute
  {
  }
}