using System;
using DynamicHttpClient.IO.Serialization;
using DynamicHttpClient.Metadata;

namespace DynamicHttpClient.Attributes
{
  /// <summary>
  /// An <see cref="Attribute"/> which associates a <see cref="IDeserializer"/> with a method or interface.
  /// </summary>
  [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
  public sealed class DeserializerAttribute : Attribute, IMetadataAware
  {
    private readonly Type deserializerType;

    /// <param name="deserializerType">The associated <see cref="IDeserializer"/> type.</param>
    public DeserializerAttribute(Type deserializerType)
    {
      Check.That(typeof(IDeserializer).IsAssignableFrom(deserializerType), "A valid IDeserializer type was expected.");

      this.deserializerType = deserializerType;
    }

    public void OnAttachMetadata(RequestMetadata metadata)
    {
      metadata.Deserializer = (IDeserializer) Activator.CreateInstance(this.deserializerType);
    }
  }
}