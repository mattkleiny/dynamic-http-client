using System;
using DynamicHttpClient.IO.Caching;
using DynamicHttpClient.Metadata;

namespace DynamicHttpClient.Caching
{
  /// <summary>
  /// Denotes the caching policy and expiration interval for a particular request.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public abstract class CachingAttribute : Attribute, IMetadataAware
  {
    protected CachingAttribute()
    {
      Representation = CachedRepresentation.Normal; // by default, use a normal representation
    }

    /// <summary>
    /// The desired <see cref="CachedRepresentation"/>.
    /// </summary>
    public CachedRepresentation Representation { get; set; }

    /// <summary>
    /// The <see cref="IExpirationPolicy"/> for the associated method.
    /// </summary>
    protected abstract IExpirationPolicy ExpirationPolicy { get; }

    public void OnAttachMetadata(RequestMetadata metadata)
    {
      metadata.CachingPolicy = new CachingPolicyBuilder
      {
        CachedRepresentation = Representation,
        ExpirationPolicy     = ExpirationPolicy
      }.Build();
    }
  }
}