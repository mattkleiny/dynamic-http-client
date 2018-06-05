using System;
using DynamicHttpClient.IO.Serialization;
using DynamicHttpClient.Metadata;

namespace DynamicHttpClient.Attributes
{
  /// <summary>Attaches a <see cref="ISerializer"/> to an HTTP client.</summary>
  [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
  public sealed class SerializerAttribute : Attribute, IMetadataAware
  {
    private readonly Type serializerType;

    public SerializerAttribute(Type serializerType)
    {
      Check.That(typeof(ISerializer).IsAssignableFrom(serializerType), "A valid ISerializer type was expected.");

      this.serializerType = serializerType;
    }

    public void OnAttachMetadata(RequestMetadata metadata)
    {
      metadata.Serializer = (ISerializer) Activator.CreateInstance(serializerType);
    }
  }
}