using System;
using System.Net.Http;

namespace DynamicHttpClient.Attributes.Methods
{
  /// <summary>Marks the given method as a HTTP POST call.</summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class PostAttribute : MethodAttribute
  {
    public PostAttribute(string path)
      : base(HttpMethod.Post, path)
    {
    }
  }
}