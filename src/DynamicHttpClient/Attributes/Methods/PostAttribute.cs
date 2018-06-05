using System;

namespace DynamicHttpClient.Attributes.Methods
{
  /// <summary>
  /// A <see cref="MethodAttribute"/> that denotes the given 
  /// method is equivalent to a <see cref="RestMethod.Post"/> request.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class PostAttribute : MethodAttribute
  {
    public PostAttribute(string path)
      : base(RestMethod.Post, path)
    {
    }
  }
}