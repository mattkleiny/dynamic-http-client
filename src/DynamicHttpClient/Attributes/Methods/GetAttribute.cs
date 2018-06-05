using System;
using System.Net.Http;

namespace DynamicHttpClient.Attributes.Methods
{
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class GetAttribute : MethodAttribute
  {
    public GetAttribute(string path)
      : base(HttpMethod.Get, path)
    {
    }
  }
}