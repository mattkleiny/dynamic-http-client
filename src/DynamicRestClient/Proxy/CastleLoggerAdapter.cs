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
    using Castle.Core.Logging;
    using Common.Logging;

    /// <summary>
    /// An adapter from Castle logging to Common.Logging.
    /// </summary>
    internal sealed class CastleLoggerAdapter : LevelFilteredLogger
    {
        private readonly ILog log;

        public CastleLoggerAdapter(ILog log)
        {
            Check.NotNull(log, nameof(log));

            this.log = log;
        }

        public override ILogger CreateChildLogger(string loggerName)
        {
            return new CastleLoggerAdapter(LogManager.GetLogger(loggerName));
        }

        protected override void Log(LoggerLevel loggerLevel, string loggerName, string message, Exception exception)
        {
            switch (loggerLevel)
            {
                case LoggerLevel.Debug:
                    this.log.Debug(message, exception);
                    break;

                case LoggerLevel.Info:
                    this.log.Info(message, exception);
                    break;

                case LoggerLevel.Warn:
                    this.log.Warn(message, exception);
                    break;

                case LoggerLevel.Error:
                    this.log.Error(message, exception);
                    break;

                case LoggerLevel.Fatal:
                    this.log.Fatal(message, exception);
                    break;
            }
        }
    }
}
