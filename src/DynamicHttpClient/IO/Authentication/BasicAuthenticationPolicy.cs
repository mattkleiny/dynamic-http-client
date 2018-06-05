using System;
using System.Text;

namespace DynamicHttpClient.IO.Authentication
{
  public sealed class BasicAuthenticationPolicy : IAuthenticationPolicy
  {
    private readonly string encodedCredentials;

    public BasicAuthenticationPolicy(string username, string password)
    {
      Check.NotNullOrEmpty(username, nameof(username));
      Check.NotNullOrEmpty(password, nameof(password));

      encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
    }

    public void AttachAuthentication(IRequestBuilder builder)
    {
      builder.Headers.Add("Authorization", "Basic " + encodedCredentials);
    }

    public void AttachAuthentication(IRequest request)
    {
    }
  }
}