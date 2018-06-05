using System;
using DynamicHttpClient.IO.Serialization;
using DynamicHttpClient.Metadata;

namespace DynamicHttpClient.Attributes
{
  /// <summary>
  /// An <see cref="Attribute"/> which associates a <see cref="ISerializer"/> with a method or interface.
  /// </summary>
  [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
  public sealed class SerializerAttribute : Attribute, IMetadataAware
  {
    private readonly Type serializerType;

    /// <param name="serializerType">The associated <see cref="ISerializer"/> type.</param>
    public SerializerAttribute(Type serializerType)
    {
      Check.That(typeof(ISerializer).IsAssignableFrom(serializerType), "A valid ISerializer type was expected.");

      this.serializerType = serializerType;
    }

    public void OnAttachMetadata(RequestMetadata metadata)
    {
      metadata.Serializer = (ISerializer) Activator.CreateInstance(this.serializerType);
    }
  }
}