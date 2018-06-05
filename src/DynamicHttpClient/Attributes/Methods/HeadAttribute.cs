using System;

namespace DynamicHttpClient.Attributes.Methods
{
  /// <summary>
  /// A <see cref="MethodAttribute"/> that denotes the given 
  /// method is equivalent to a <see cref="RestMethod.Head"/> request.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class HeadAttribute : MethodAttribute
  {
    public HeadAttribute(string path)
      : base(RestMethod.Head, path)
    {
    }
  }
}