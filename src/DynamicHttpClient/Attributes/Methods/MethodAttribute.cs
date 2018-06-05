using System;
using DynamicHttpClient.Metadata;

namespace DynamicHttpClient.Attributes.Methods
{
  /// <summary>
  /// Base class for any <see cref="Attribute"/> which defines a <see cref="method"/> and <see cref="path"/>.
  /// </summary>
  public abstract class MethodAttribute : Attribute, IMetadataAware
  {
    private readonly RestMethod method;
    private readonly string     path;

    protected MethodAttribute(RestMethod method, string path)
    {
      this.method = method;
      this.path   = path;
    }

    /// <summary>
    /// True whether a response is required and an exception should be generated if one is not received, otherwise false.
    /// </summary>
    public bool IsResponseRequired { get; set; }

    public void OnAttachMetadata(RequestMetadata metadata)
    {
      metadata.Method             = this.method;
      metadata.Path               = this.path;
      metadata.IsResponseRequired = IsResponseRequired;
    }
  }
}