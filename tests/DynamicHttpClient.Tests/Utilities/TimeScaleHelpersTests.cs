using System;
using DynamicHttpClient.Utilities;
using Xunit;

namespace DynamicHttpClient.Tests.Utilities
{
  public class TimeScaleHelpersTests
  {
    [Fact]
    public void BuildTimeSpan_honours_interval_and_scale()
    {
      Assert.Equal(TimeSpan.FromMilliseconds(10), TimeScaleHelpers.BuildTimeSpan(10, TimeScale.Milliseconds));
      Assert.Equal(TimeSpan.FromSeconds(10),      TimeScaleHelpers.BuildTimeSpan(10, TimeScale.Seconds));
      Assert.Equal(TimeSpan.FromHours(10),        TimeScaleHelpers.BuildTimeSpan(10, TimeScale.Hours));
      Assert.Equal(TimeSpan.FromMinutes(10),      TimeScaleHelpers.BuildTimeSpan(10, TimeScale.Minutes));
      Assert.Equal(TimeSpan.FromDays(10),         TimeScaleHelpers.BuildTimeSpan(10, TimeScale.Days));
    }
  }
}