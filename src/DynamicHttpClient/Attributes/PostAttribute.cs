using System;
using System.Net.Http;

namespace DynamicHttpClient.Attributes
{
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class PostAttribute : MethodAttribute
  {
    public PostAttribute(string path)
      : base(HttpMethod.Post, path)
    {
    }
  }
}