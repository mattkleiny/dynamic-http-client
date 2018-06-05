using System;
using Castle.Core.Logging;
using Xunit;

namespace DynamicHttpClient.Tests.Proxy
{
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