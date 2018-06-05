﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DynamicHttpClient.Attributes;
using DynamicHttpClient.Attributes.Methods;

namespace DynamicHttpClient.Metadata
{
  /// <summary>
  /// Static factories related to metadata.
  /// </summary>
  internal static class MetadataFactory
  {
    /// <summary>
    /// Inspects the given <see cref="Type"/> and looks for errors in the metadata.
    /// </summary>
    public static void InspectType(Type type)
    {
      Check.NotNull(type, nameof(type));

      if (!type.IsInterface)
      {
        throw new InvalidMetadataException("The given type " + type + " must be an interface.");
      }

      if (type.GetCustomAttribute<SerializerAttribute>() == null)
      {
        throw new InvalidMetadataException("The given type " + type + " must specify a ISerializer via the [Serializer] attribute.");
      }

      if (type.GetCustomAttribute<DeserializerAttribute>() == null)
      {
        throw new InvalidMetadataException("The given type " + type + " must specify a IDeserializer via the [Deserializer] attribute.");
      }

      foreach (var method in type.GetMethods())
      {
        InspectMethod(method);
      }
    }

    /// <summary>
    /// Inspects the given <see cref="MemberInfo"/> to ensure it matches the required contract.
    /// </summary>
    public static void InspectMethod(MethodInfo method)
    {
      Check.NotNull(method, nameof(method));

      var methodAttribute = method.GetCustomAttribute<MethodAttribute>();

      if (methodAttribute == null)
      {
        throw new InvalidMetadataException("A MethodAttribute must be specified for " + method.Name);
      }

      if (method.GetParameters().SelectMany(p => p.GetCustomAttributes<BodyAttribute>()).Count() > 1)
      {
        throw new InvalidMetadataException("Only a single [BodyAttribute] per method may be specified for " + method.Name);
      }
    }

    /// <summary>
    /// Builds <see cref="RequestMetadata"/> from the given <see cref="MethodInfo"/>.
    /// </summary>
    /// <exception cref="InvalidMetadataException">If the method metadata is invalid.</exception>
    public static RequestMetadata CreateMetadata(MethodInfo method)
    {
      Check.NotNull(method, nameof(method));

      InspectMethod(method);

      var metadata = new RequestMetadata
      {
        ResultType = GetReturnType(method)
      };

      foreach (var attribute in FindMetadataAwareAttributes(method))
      {
        attribute.OnAttachMetadata(metadata);
      }

      PopulateParameterInfo(method, metadata);

      if (typeof(Task).IsAssignableFrom(method.ReturnType))
      {
        metadata.IsAsynchronous = true;
      }

      return metadata;
    }

    /// <summary>
    /// Determines the return type for the given method.
    /// </summary>
    internal static Type GetReturnType(MethodInfo method)
    {
      Check.NotNull(method, nameof(method));

      if (typeof(Task).IsAssignableFrom(method.ReturnType))
      {
        if (method.ReturnType.GenericTypeArguments.Any())
        {
          return method.ReturnType.GetGenericArguments()[0];
        }
      }

      return method.ReturnType;
    }

    /// <summary>
    /// Populates the parameters on the <see cref="RequestMetadata"/> object.
    /// </summary>
    private static void PopulateParameterInfo(MethodInfo method, RequestMetadata metadata)
    {
      var parameters       = method.GetParameters();
      var bodyWasSpecified = false;

      // walk the parameters, keep track of the index because it's passed into the metadata objects
      for (var index = 0; index < parameters.Length; index++)
      {
        var parameter = parameters[index];

        // check for body parameters
        var body = parameter.GetCustomAttribute<BodyAttribute>();

        if (body != null)
        {
          // only a single body parameter is supported
          if (bodyWasSpecified)
          {
            throw new InvalidMetadataException("Only a single body parameter is supported for: " + method.Name);
          }

          // body parameters are only valid for POST/PUT
          if (metadata.Method != RestMethod.Post &&
              metadata.Method != RestMethod.Put)
          {
            throw new InvalidMetadataException("A body parameter was specified for a " + metadata.Method + " request: " + method.Name);
          }

          metadata.Body = new RequestBodyMetadata
          {
            Index = index,
            Type  = parameter.ParameterType
          };

          bodyWasSpecified = true;
        }
        else
        {
          // if no body attribute was specified, then by convention this a 'url segment' property
          var segment = new UrlSegmentMetadata
          {
            Index = index,
            Name  = parameter.Name
          };

          // permit overriding the convention with a custom attribute
          var attribute = parameter.GetCustomAttribute<SegmentAttribute>();

          if (attribute != null)
          {
            if (!string.IsNullOrWhiteSpace(segment.Name))
            {
              segment.Name = attribute.Name;
            }
          }

          metadata.UrlSegments.Add(segment);
        }
      }
    }

    /// <summary>
    /// Finds the <see cref="IMetadataAware"/> for the given method.
    /// </summary>
    private static IEnumerable<IMetadataAware> FindMetadataAwareAttributes(MethodBase method)
    {
      Check.NotNull(method, nameof(method));

      foreach (var attribute in method.GetParameters().SelectMany(p => p.GetCustomAttributes()).OfType<IMetadataAware>())
      {
        yield return attribute;
      }

      foreach (var attribute in method.GetCustomAttributes().OfType<IMetadataAware>())
      {
        yield return attribute;
      }

      foreach (var attribute in method.DeclaringType.GetCustomAttributes().OfType<IMetadataAware>())
      {
        yield return attribute;
      }
    }
  }
}