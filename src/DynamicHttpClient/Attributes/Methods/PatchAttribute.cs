using System;

namespace DynamicHttpClient.Attributes.Methods
{
  /// <summary>
  /// A <see cref="MethodAttribute"/> that denotes the given 
  /// method is equivalent to a <see cref="RestMethod.Patch"/> request.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class PatchAttribute : MethodAttribute
  {
    public PatchAttribute(string path)
      : base(RestMethod.Patch, path)
    {
    }
  }
}