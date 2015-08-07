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

namespace DynamicRestClient
{
    using System.Threading.Tasks;
    using Caching;
    using IO;
    using Proxy;

    /// <summary>
    /// Builder pattern for a REST client implementation.
    /// </summary>
    public sealed class RestClientFactory
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RestClientFactory()
        {
            Executor = new NullRequestExecutor();
            Cache = new NullCache();
            Scheduler = TaskScheduler.Default;
        }

        /// <summary>
        /// The <see cref="IRequestExecutor"/> to use.
        /// </summary>
        public IRequestExecutor Executor { get; set; }

        /// <summary>
        /// The <see cref="ICache"/> to use.
        /// </summary>
        public ICache Cache { get; set; }

        /// <summary>
        /// The <see cref="TaskScheduler"/> to use for asynchronous requests.
        /// </summary>
        public TaskScheduler Scheduler { get; set; }

        /// <summary>
        /// Builds the resultant REST client.
        /// </summary>
        public TClient Build<TClient>()
        {
            Check.NotNull(Executor, nameof(Executor));
            Check.NotNull(Cache, nameof(Cache));
            Check.NotNull(Scheduler, nameof(Scheduler));

            return DynamicProxyFactory.BuildProxy<TClient>(new RestClientInterceptor(Executor, Cache, Scheduler));
        }
    }
}
