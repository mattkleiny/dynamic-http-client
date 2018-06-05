using System;
using System.Net.Http;

namespace DynamicHttpClient.Attributes.Methods
{
  /// <summary>Marks the given method as a HTTP GET call.</summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class GetAttribute : MethodAttribute
  {
    public GetAttribute(string path)
      : base(HttpMethod.Get, path)
    {
    }
  }
}