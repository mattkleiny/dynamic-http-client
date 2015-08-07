// The MIT License (MIT)
// 
// Copyright (C) 2015, Matthew Kleinschafer.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace DynamicRestClient.Proxy
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Caching;
    using Castle.Core.Interceptor;
    using IO;
    using Metadata;

    /// <summary>
    /// The Castle DynamicProxy <see cref="IInterceptor"/> for the REST client implementation.
    /// </summary>
    internal sealed class RestClientInterceptor : IInterceptor
    {
        private readonly IRequestExecutor executor;

        /// <summary>
        /// A 1:1 cache for <see cref="RequestMetadata"/> against their associated <see cref="MethodInfo"/>.
        /// </summary>
        private readonly ConcurrentDictionary<MethodInfo, RequestMetadata> metadataCache = new ConcurrentDictionary<MethodInfo, RequestMetadata>();

        private readonly ICache responseCache;
        private readonly TaskScheduler scheduler;

        /// <param name="executor">The <see cref="IRequestExecutor"/> to use.</param>
        public RestClientInterceptor(IRequestExecutor executor)
            : this(executor, new NullCache(), TaskScheduler.Default)
        {
        }

        /// <param name="executor">The <see cref="IRequestExecutor"/> to use.</param>
        /// <param name="responseCache">The <see cref="ICache"/> to use for responses.</param>
        public RestClientInterceptor(IRequestExecutor executor, ICache responseCache)
            : this(executor, responseCache, TaskScheduler.Default)
        {
        }

        /// <param name="executor">The <see cref="IRequestExecutor"/> to use.</param>
        /// <param name="responseCache">The <see cref="ICache"/> to use for responses.</param>
        /// <param name="scheduler">The <see cref="TaskScheduler"/> to use.</param>
        public RestClientInterceptor(IRequestExecutor executor, ICache responseCache, TaskScheduler scheduler)
        {
            Check.NotNull(executor, nameof(executor));
            Check.NotNull(responseCache, nameof(responseCache));
            Check.NotNull(scheduler, nameof(scheduler));

            this.executor = executor;
            this.responseCache = responseCache;
            this.scheduler = scheduler;
        }

        /// <remarks>
        /// This interceptor is shared amongst many threads; don't store state in the instance.
        /// </remarks>
        public void Intercept(IInvocation invocation)
        {
            // extract metadata from cache or reflection
            var metadata = this.metadataCache.GetOrAdd(invocation.Method, MetadataFactory.CreateMetadata);

            if (metadata.IsAsynchronous)
            {
                // start the task executing
                var task = ExecuteTask(_ => InterceptInner(invocation, metadata));

                if (invocation.Method.ReturnType.GenericTypeArguments.Any())
                {
                    // Task<T> is not co-variant; we need to do some trickery to 
                    // get the right <T> in the resultant Task object.
                    invocation.ReturnValue = CastTask(task, metadata.ResultType);
                }
                else
                {
                    invocation.ReturnValue = task;
                }
            }
            else
            {
                invocation.ReturnValue = InterceptInner(invocation, metadata);
            }
        }

        /// <summary>
        /// The actual implementation of <see cref="Intercept"/>.
        /// </summary>
        private object InterceptInner(IInvocation invocation, RequestMetadata metadata)
        {
            // build request based on metadata
            var request = BuildRequestFromMetadata(invocation, metadata);
            var response = ExecuteRequest(request, metadata);

            // extract response object
            return ExtractResult(response, metadata);
        }

        /// <summary>
        /// Executes a new <see cref="Task"/> on the inner <see cref="scheduler"/> with reasonable default parameters.
        /// </summary>
        private Task<T> ExecuteTask<T>(Func<object, T> task)
        {
            return Task.Factory.StartNew(task, null, CancellationToken.None, TaskCreationOptions.None, this.scheduler);
        }

        /// <summary>
        /// Builds a <see cref="IRequest"/> from the given <see cref="RequestMetadata"/>
        /// </summary>
        private IRequest BuildRequestFromMetadata(IInvocation invocation, RequestMetadata metadata)
        {
            var builder = this.executor.BuildRequest();

            // specify path and method on the request
            builder.Path = metadata.Path;
            builder.Method = metadata.Method;

            // specify timeout on the request
            if (metadata.Timeout.HasValue)
            {
                builder.Timeout = metadata.Timeout.Value;
            }

            // attach headers to the request
            foreach (var header in metadata.Headers)
            {
                builder.Headers.Add(header);
            }

            // attach url segments to the request
            foreach (var segment in metadata.UrlSegments)
            {
                var value = invocation.GetArgumentValue(segment.Index);

                builder.Segments.Add(segment.Name, value.ToString());
            }

            // attach body to the request
            if (metadata.Body != null)
            {
                var value = invocation.GetArgumentValue(metadata.Body.Index);

                builder.Body = new SerializedBody(metadata.Body.Type, value, metadata.Serializer);
            }

            return builder.Build();
        }

        /// <summary>
        /// Executes a <see cref="IRequest"/>, optionally caching the response if requested.
        /// </summary>
        private IResponse ExecuteRequest(IRequest request, RequestMetadata metadata)
        {
            var policy = metadata.CachingPolicy;

            if (policy != null)
            {
                // employ the response cache
                return this.responseCache.GetOrCompute(
                    policy.EvaluateCacheKey(request),
                    policy.GetCacheSettings(request),
                    () => policy.GetCacheableResponse(this.executor.ExecuteRequest(request)),
                    policy.ShouldCache);
            }

            return this.executor.ExecuteRequest(request);
        }

        /// <summary>
        /// Extracts the resultant object from a <see cref="IResponse"/>.
        /// </summary>
        private static object ExtractResult(IResponse response, RequestMetadata metadata)
        {
            if (response.StatusCode != HttpStatusCode.NotFound && response.StatusCode != HttpStatusCode.NoContent)
            {
                if (typeof (void).IsAssignableFrom(metadata.ResultType))
                {
                    return null;
                }

                if (typeof (IResponse).IsAssignableFrom(metadata.ResultType))
                {
                    return response;
                }

                if (typeof (Stream).IsAssignableFrom(metadata.ResultType))
                {
                    return new MemoryStream(response.RawBytes);
                }

                // deserialize with associated deserializer
                var deserializer = metadata.Deserializer;
                var reader = new StringReader(response.Content);

                return deserializer.Deserialize(metadata.ResultType, reader);
            }

            return null;
        }

        /// <summary>
        /// Given a <see cref="Task{T}"/> of, casts the result as <see cref="resultType"/>.
        /// </summary>
        /// <remarks>This may be a hot-path depending on dynamic dispatch overhead.</remarks>
        private static Task CastTask(Task<object> task, Type resultType)
        {
            var completionSourceType = typeof (TaskCompletionSource<>).MakeGenericType(resultType);
            var completionSource = (dynamic) Activator.CreateInstance(completionSourceType);

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
    }
}
