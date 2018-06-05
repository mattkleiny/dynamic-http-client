using System;

namespace DynamicHttpClient.Attributes.Methods
{
  /// <summary>
  /// A <see cref="MethodAttribute"/> that denotes the given 
  /// method is equivalent to a <see cref="RestMethod.Get"/> request.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class GetAttribute : MethodAttribute
  {
    public GetAttribute(string path)
      : base(RestMethod.Get, path)
    {
    }
  }
}