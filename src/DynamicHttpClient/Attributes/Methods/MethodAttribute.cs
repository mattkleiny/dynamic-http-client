using System;
using System.Net.Http;
using DynamicHttpClient.Metadata;

namespace DynamicHttpClient.Attributes.Methods
{
  /// <summary>Denotes the associated method is an HTTP call.</summary>
  public abstract class MethodAttribute : Attribute, IMetadataAware
  {
    private readonly HttpMethod method;
    private readonly string     path;

    protected MethodAttribute(HttpMethod method, string path)
    {
      this.method = method;
      this.path   = path;
    }

    public bool IsResponseRequired { get; set; }

    public void OnAttachMetadata(RequestMetadata metadata)
    {
      metadata.Method             = method;
      metadata.Path               = path;
      metadata.IsResponseRequired = IsResponseRequired;
    }
  }
}