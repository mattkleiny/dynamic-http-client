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

namespace DynamicRestClient.Tests.Proxy
{
    using System;
    using Castle.Core.Logging;
    using Common.Logging;
    using DynamicRestClient.Proxy;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Xunit;

    public class CastleLoggerAdapterTests : TestFixture
    {
        [Fact]
        public void Log_Delegates_To_Logger_Target_With_Correct_Log_Level()
        {
            var log = Fixture.Create<ILog>();

            var adapter = new CastleLoggerAdapter(log)
            {
                Level = LoggerLevel.Debug
            };

            var exception = new Exception();

            adapter.Debug("Test", exception);
            log.Received().Debug("Test", exception);

            adapter.Info("Test", exception);
            log.Received().Info("Test", exception);

            adapter.Warn("Test", exception);
            log.Received().Warn("Test", exception);

            adapter.Error("Test", exception);
            log.Received().Error("Test", exception);

            adapter.Fatal("Test", exception);
            log.Received().Fatal("Test", exception);
        }
    }
}
