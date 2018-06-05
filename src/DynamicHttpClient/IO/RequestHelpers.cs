using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicHttpClient.IO
{
  public static class RequestHelpers
  {
    public static string SubstituteUrlParameters(string url, IDictionary<string, string> segments)
    {
      return SubstituteUrlParameters(url, segments, _ => _);
    }

    public static string SubstituteUrlParameters(string url, IDictionary<string, string> segments, Func<string, string> urlEncodeDelegate)
    {
      Check.NotNullOrEmpty(url, nameof(url));
      Check.NotNull(segments,          nameof(segments));
      Check.NotNull(urlEncodeDelegate, nameof(urlEncodeDelegate));

      var builder = new StringBuilder(url);

      foreach (var segment in segments)
      {
        builder.Replace("{" + segment.Key + "}", urlEncodeDelegate(segment.Value));
      }

      return builder.ToString();
    }
  }
}