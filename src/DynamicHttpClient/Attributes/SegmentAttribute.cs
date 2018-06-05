using System;

namespace DynamicHttpClient.Attributes
{
  /// <summary>
  /// Associates a method parameter with a URI segment parameter.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  public sealed class SegmentAttribute : ParameterAttribute
  {
    public SegmentAttribute(string name)
    {
      Check.NotNullOrEmpty(name, nameof(name));

      Name = name;
    }

    public string Name { get; }
  }
}