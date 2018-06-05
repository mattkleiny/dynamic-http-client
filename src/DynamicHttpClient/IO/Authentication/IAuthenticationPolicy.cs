namespace DynamicHttpClient.IO.Authentication
{
  /// <summary>
  /// An authentication policy for the <see cref="AuthenticatedRequestExecutor"/>.
  /// </summary>
  public interface IAuthenticationPolicy
  {
    /// <summary>
    /// Attaches authentication to the given <see cref="IRequestBuilder"/>.
    /// </summary>
    void AttachAuthentication(IRequestBuilder builder);

    /// <summary>
    /// Attaches authentication to the given <see cref="IRequest"/>.
    /// </summary>
    void AttachAuthentication(IRequest request);
  }
}