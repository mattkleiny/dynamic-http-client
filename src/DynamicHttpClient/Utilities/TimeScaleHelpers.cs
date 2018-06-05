using System;

namespace DynamicHttpClient.Utilities
{
  /// <summary>
  /// Static utilities for computing <see cref="TimeSpan"/>s from <see cref="TimeScale"/>s.
  /// </summary>
  internal static class TimeScaleHelpers
  {
    /// <summary>
    /// Given an <see cref="interval"/> and a <see cref="scale"/>, 
    /// builds a <see cref="TimeSpan"/> representing the measure.
    /// </summary>
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
          throw new ArgumentException("An unrecognized scale was requested: " + scale);
      }
    }
  }
}