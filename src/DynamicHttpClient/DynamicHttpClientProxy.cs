using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using DynamicHttpClient.Caching;
using DynamicHttpClient.IO;
using DynamicHttpClient.Metadata;

namespace DynamicHttpClient
{
  public class DynamicHttpClientProxy : DispatchProxy
  {
    private readonly ConcurrentDictionary<MethodInfo, RequestMetadata> metadataCache = new ConcurrentDictionary<MethodInfo, RequestMetadata>();

    private IRequestExecutor executor;
    private ICache           responseCache;

    public static TClient Create<TClient>(IRequestExecutor executor, ICache cache)
    {
      Check.NotNull(executor, nameof(executor));
      Check.NotNull(cache,    nameof(cache));

      object client = Create<TClient, DynamicHttpClientProxy>();
      var    proxy  = (DynamicHttpClientProxy) client;

      proxy.executor      = executor;
      proxy.responseCache = cache;

      return (TClient) client;
    }

    protected override object Invoke(MethodInfo method, object[] args)
    {
      dynamic CastTask(Task<object> task, Type resultType)
      {
        var completionSourceType = typeof(TaskCompletionSource<>).MakeGenericType(resultType);
        var completionSource     = (dynamic) Activator.CreateInstance(completionSourceType);

        task.ContinueWith(antecendent =>
        {
          try
          {
            dynamic result = antecendent.Result;

            completionSource.SetResult(result);
          }
          catch (Exception e)
          {
            completionSource.SetException(e);
          }
        });

        return completionSource.Task;
      }

      // extract metadata from cache or reflection
      var metadata = metadataCache.GetOrAdd(method, MetadataFactory.CreateMetadata);

      if (metadata.IsAsynchronous)
      {
        return CastTask(InvokeInnerAsync(metadata, args), metadata.ResultType);
      }

      return InvokeInnerAsync(metadata, args).Result;
    }

    private async Task<object> InvokeInnerAsync(RequestMetadata metadata, IReadOnlyList<object> args)
    {
      var request  = BuildRequestFromMetadata(metadata, args);
      var response = await ExecuteRequestAsync(request, metadata);

      return ExtractResult(response, metadata);
    }

    private IRequest BuildRequestFromMetadata(RequestMetadata metadata, IReadOnlyList<object> args)
    {
      var builder = executor.PrepareRequest();

      builder.Path   = metadata.Path;
      builder.Method = metadata.Method;

      if (metadata.Timeout.HasValue)
      {
        builder.Timeout = metadata.Timeout.Value;
      }

      foreach (var header in metadata.Headers)
      {
        builder.Headers.Add(header);
      }

      foreach (var segment in metadata.UrlSegments)
      {
        var value = args[segment.Index];

        builder.Segments.Add(segment.Name, value.ToString());
      }

      if (metadata.Body != null)
      {
        var value = args[metadata.Body.Index];

        builder.Body = new SerializedBody(metadata.Body.Type, value, metadata.Serializer);
      }

      return builder.Build();
    }

    private async Task<IResponse> ExecuteRequestAsync(IRequest request, RequestMetadata metadata)
    {
      var policy = metadata.CachingPolicy;

      if (policy != null)
      {
        return await responseCache.GetOrComputeAsync(
          policy.EvaluateCacheKey(request),
          policy.GetCacheSettings(request),
          async () => policy.GetCacheableResponse(await executor.ExecuteRequestAsync(request)),
          policy.ShouldCache
        );
      }

      return await executor.ExecuteRequestAsync(request);
    }

    private static object ExtractResult(IResponse response, RequestMetadata metadata)
    {
      if (response.StatusCode != HttpStatusCode.NotFound && response.StatusCode != HttpStatusCode.NoContent)
      {
        if (typeof(void).IsAssignableFrom(metadata.ResultType))
        {
          return null;
        }

        if (typeof(IResponse).IsAssignableFrom(metadata.ResultType))
        {
          return response;
        }

        if (typeof(Stream).IsAssignableFrom(metadata.ResultType))
        {
          return new MemoryStream(response.RawBytes);
        }

        var deserializer = metadata.Deserializer;
        var reader       = new StringReader(response.Content);

        return deserializer.Deserialize(metadata.ResultType, reader);
      }

      return null;
    }
  }
}