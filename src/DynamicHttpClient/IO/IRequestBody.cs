namespace DynamicHttpClient.IO
{
  /// <summary>
  /// Abstractly represents the body of an HTTP request.
  /// </summary>
  public interface IRequestBody
  {
    /// <summary>
    /// The textual content of the body.
    /// </summary>
    string Content { get; }

    /// <summary>
    /// The media/mime type of the body.
    /// </summary>
    string ContentType { get; }
  }
}