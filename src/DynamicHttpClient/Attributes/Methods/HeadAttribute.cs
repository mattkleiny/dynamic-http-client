using System;
using System.Net.Http;

namespace DynamicHttpClient.Attributes.Methods
{
  /// <summary>Marks the given method as a HTTP HEAD call.</summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class HeadAttribute : MethodAttribute
  {
    public HeadAttribute(string path)
      : base(HttpMethod.Head, path)
    {
    }
  }
}