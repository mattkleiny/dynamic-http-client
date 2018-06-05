using System;
using System.Net.Http;

namespace DynamicHttpClient.Attributes
{
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class OptionsAttribute : MethodAttribute
  {
    public OptionsAttribute(string path)
      : base(HttpMethod.Options, path)
    {
    }
  }
}