using System.Collections.Generic;
using DynamicHttpClient.IO;
using Xunit;

namespace DynamicHttpClient.Tests.IO
{
  public class RequestHelpersTests
  {
    [Fact]
    public void SubstituteUrlParameters_replaces_well_known_segments_in_string()
    {
      const string url = "https://test.com/{key1}/inner/{key2}";

      var substituted = RequestHelpers.SubstituteUrlParameters(url, new Dictionary<string, string>
      {
        ["key1"] = "value1",
        ["key2"] = "value2"
      });

      Assert.Equal("https://test.com/value1/inner/value2", substituted);
    }
  }
}