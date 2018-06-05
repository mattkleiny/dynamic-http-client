using System;
using DynamicHttpClient.IO.Serialization;
using DynamicHttpClient.Metadata;

namespace DynamicHttpClient.Attributes
{
  [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
  public sealed class DeserializerAttribute : Attribute, IMetadataAware
  {
    private readonly Type deserializerType;

    public DeserializerAttribute(Type deserializerType)
    {
      Check.That(typeof(IDeserializer).IsAssignableFrom(deserializerType), "A valid IDeserializer type was expected.");

      this.deserializerType = deserializerType;
    }

    public void OnAttachMetadata(RequestMetadata metadata)
    {
      metadata.Deserializer = (IDeserializer) Activator.CreateInstance(deserializerType);
    }
  }
}