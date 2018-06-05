using System;

namespace DynamicHttpClient.Attributes
{
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