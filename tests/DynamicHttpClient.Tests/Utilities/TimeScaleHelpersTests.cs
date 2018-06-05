using System;
using Xunit;

namespace DynamicHttpClient.Tests.Utilities
{
  public class TimeScaleHelpersTests
  {
    [Fact]
    public void BuildTimeSpan_Honours_Interval_And_Scale()
    {
      Assert.Equal(TimeSpan.FromMilliseconds(10), TimeScaleHelpers.BuildTimeSpan(10, TimeScale.Milliseconds));
      Assert.Equal(TimeSpan.FromSeconds(10),      TimeScaleHelpers.BuildTimeSpan(10, TimeScale.Seconds));
      Assert.Equal(TimeSpan.FromHours(10),        TimeScaleHelpers.BuildTimeSpan(10, TimeScale.Hours));
      Assert.Equal(TimeSpan.FromMinutes(10),      TimeScaleHelpers.BuildTimeSpan(10, TimeScale.Minutes));
      Assert.Equal(TimeSpan.FromDays(10),         TimeScaleHelpers.BuildTimeSpan(10, TimeScale.Days));
    }

    [Fact]
    public void BuildTimeSpan_Complains_About_Unknown_Scale()
    {
      Assert.Throws<ArgumentException>(() => TimeScaleHelpers.BuildTimeSpan(10, (TimeScale) 15));
    }
  }
}