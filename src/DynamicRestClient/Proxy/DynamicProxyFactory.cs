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
    using Castle.Core.Interceptor;
    using Castle.Core.Logging;
    using Castle.DynamicProxy;
    using Common.Logging;

    /// <summary>
    /// A supplier for Castle dynamic proxies.
    /// </summary>
    internal static class DynamicProxyFactory
    {
        /// <summary>
        /// The <see cref="ProxyGenerator"/> to use. We want to build in-memory assemblies, and we'll cache the results manually.
        /// </summary>
        private static readonly ProxyGenerator Generator = new ProxyGenerator(new DefaultProxyBuilder())
        {
            Logger = new CastleLoggerAdapter(LogManager.GetLogger(typeof (DynamicProxyFactory)))
            {
                Level = LoggerLevel.Warn
            }
        };

        /// <summary>
        /// Builds a proxy of <see cref="TType"/> with the given <see cref="IInterceptor"/>.
        /// </summary>
        public static TType BuildProxy<TType>(IInterceptor interceptor)
        {
            Check.NotNull(interceptor, nameof(interceptor));

            return (TType) Generator.CreateInterfaceProxyWithoutTarget(typeof (TType), interceptor);
        }
    }
}
