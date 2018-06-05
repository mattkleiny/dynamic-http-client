using System;

namespace DynamicHttpClient.Attributes.Methods
{
  /// <summary>
  /// A <see cref="MethodAttribute"/> that denotes the given 
  /// method is equivalent to a <see cref="RestMethod.Options"/> request.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class OptionsAttribute : MethodAttribute
  {
    public OptionsAttribute(string path)
      : base(RestMethod.Options, path)
    {
    }
  }
}