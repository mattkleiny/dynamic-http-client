namespace DynamicHttpClient.IO.Authentication
{
  public interface IAuthenticationPolicy
  {
    void AttachAuthentication(IRequestBuilder builder);
    void AttachAuthentication(IRequest request);
  }
}