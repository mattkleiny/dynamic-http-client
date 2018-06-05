using System;
using System.Collections.Generic;

namespace DynamicHttpClient.IO
{
  /// <summary>
  /// Base class for any <see cref="IRequest"/> implementation.
  /// </summary>
  public abstract class AbstractRequest : IRequest
  {
    public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    public IDictionary<string, string> Segments { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    public string Url { get; set; }

    public RestMethod Method { get; set; }

    public IRequestBody Body { get; set; }

    public TimeSpan? Timeout { get; set; }
  }
}