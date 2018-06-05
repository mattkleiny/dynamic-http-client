using System;

namespace DynamicHttpClient.Attributes.Methods
{
  /// <summary>
  /// A <see cref="MethodAttribute"/> that denotes the given 
  /// method is equivalent to a <see cref="RestMethod.Put"/> request.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class PutAttribute : MethodAttribute
  {
    public PutAttribute(string path)
      : base(RestMethod.Put, path)
    {
    }
  }
}