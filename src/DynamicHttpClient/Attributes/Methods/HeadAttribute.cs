using System;
using System.Net.Http;

namespace DynamicHttpClient.Attributes.Methods
{
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class HeadAttribute : MethodAttribute
  {
    public HeadAttribute(string path)
      : base(HttpMethod.Head, path)
    {
    }
  }
}