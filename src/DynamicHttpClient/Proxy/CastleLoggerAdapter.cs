using System;

namespace DynamicHttpClient.Proxy
{
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