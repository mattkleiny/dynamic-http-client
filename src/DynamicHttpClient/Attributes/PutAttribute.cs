using System;
using System.Net.Http;

namespace DynamicHttpClient.Attributes
{
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class PutAttribute : MethodAttribute
  {
    public PutAttribute(string path)
      : base(HttpMethod.Put, path)
    {
    }
  }
}