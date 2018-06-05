using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicHttpClient.IO
{
  /// <summary>
  /// Static helpers related to <see cref="IRequest"/>s.
  /// </summary>
  public static class RequestHelpers
  {
    /// <summary>
    /// Substitutes the given URL segment parameters in the given string using the given delegate to encode parameters.
    /// </summary>
    public static string SubstituteUrlParameters(string url, IEnumerable<KeyValuePair<string, string>> segments)
    {
      return SubstituteUrlParameters(url, segments, _ => _);
    }

    /// <summary>
    /// Substitutes the given URL segment parameters in the given string using the given delegate to encode parameters.
    /// </summary>
    public static string SubstituteUrlParameters(string url, IEnumerable<KeyValuePair<string, string>> segments, Func<string, string> urlEncodeDelegate)
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