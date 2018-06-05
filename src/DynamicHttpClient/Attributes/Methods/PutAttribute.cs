using System;
using System.Net.Http;

namespace DynamicHttpClient.Attributes.Methods
{
  /// <summary>Marks the given method as a HTTP PUT call.</summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class PutAttribute : MethodAttribute
  {
    public PutAttribute(string path)
      : base(HttpMethod.Put, path)
    {
    }
  }
}