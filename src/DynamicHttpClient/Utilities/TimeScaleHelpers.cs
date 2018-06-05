using System;

namespace DynamicHttpClient.Utilities
{
  internal static class TimeScaleHelpers
  {
    public static TimeSpan BuildTimeSpan(int interval, TimeScale scale)
    {
      Check.That(interval > 0, "A positive interval was expected.");

      switch (scale)
      {
        case TimeScale.Milliseconds:
          return TimeSpan.FromMilliseconds(interval);

        case TimeScale.Seconds:
          return TimeSpan.FromSeconds(interval);

        case TimeScale.Minutes:
          return TimeSpan.FromMinutes(interval);

        case TimeScale.Hours:
          return TimeSpan.FromHours(interval);

        case TimeScale.Days:
          return TimeSpan.FromDays(interval);

        default:
          throw new ArgumentException($"An unrecognized scale was requested: {scale}", nameof(scale));
      }
    }
  }
}