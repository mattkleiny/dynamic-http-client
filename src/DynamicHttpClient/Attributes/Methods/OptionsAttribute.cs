using System;
using System.Net.Http;

namespace DynamicHttpClient.Attributes.Methods
{
  /// <summary>Marks the given method as a HTTP OPTIONS call.</summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class OptionsAttribute : MethodAttribute
  {
    public OptionsAttribute(string path)
      : base(HttpMethod.Options, path)
    {
    }
  }
}