using System;

namespace DynamicHttpClient.Attributes.Methods
{
  /// <summary>
  /// A <see cref="MethodAttribute"/> that denotes the given 
  /// method is equivalent to a <see cref="RestMethod.Delete"/> request.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class DeleteAttribute : MethodAttribute
  {
    public DeleteAttribute(string path)
      : base(RestMethod.Delete, path)
    {
    }
  }
}