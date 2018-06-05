using System.Collections.Generic;
using Xunit;

namespace DynamicHttpClient.Tests.IO
{
  public class RequestHelpersTests
  {
    [Fact]
    public void SubstituteUrlParameters_Replaces_Well_Known_Segments_In_String()
    {
      const string url = "https://test.com/{key1}/inner/{key2}";

      var substituted = RequestHelpers.SubstituteUrlParameters(url, new[]
      {
        new KeyValuePair<string, string>("key1", "value1"),
        new KeyValuePair<string, string>("key2", "value2")
      });

      Assert.Equal("https://test.com/value1/inner/value2", substituted);
    }
  }
}