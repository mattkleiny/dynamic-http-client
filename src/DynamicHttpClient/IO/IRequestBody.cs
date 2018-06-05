namespace DynamicHttpClient.IO
{
  public interface IRequestBody
  {
    string Content     { get; }
    string ContentType { get; }
  }
}