using System;
using System.Net.Http;

namespace DynamicHttpClient.Attributes.Methods
{
  /// <summary>Marks the given method as a HTTP DELETE call.</summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class DeleteAttribute : MethodAttribute
  {
    public DeleteAttribute(string path)
      : base(HttpMethod.Delete, path)
    {
    }
  }
}