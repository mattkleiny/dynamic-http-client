﻿using System;
using DynamicHttpClient.IO.Caching;
using DynamicHttpClient.Metadata;

namespace DynamicHttpClient.Caching
{
  /// <summary>Denotes the associated HTTP invocation should be cached, and how it should be cached.</summary>
  [AttributeUsage(AttributeTargets.Method)]
  public abstract class CachingAttribute : Attribute, IMetadataAware
  {
    public             CachedRepresentation Representation   { get; set; } = CachedRepresentation.Full;
    protected abstract IExpirationPolicy    ExpirationPolicy { get; }

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