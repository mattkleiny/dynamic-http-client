using System;
using System.Net.Http;

namespace DynamicHttpClient.Attributes.Methods
{
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class DeleteAttribute : MethodAttribute
  {
    public DeleteAttribute(string path)
      : base(HttpMethod.Delete, path)
    {
    }
  }
}