using System;
using System.Text;

namespace DynamicHttpClient.IO.Authentication
{
  /// <summary>
  /// A <see cref="IAuthenticationPolicy"/> that uses HTTP Basic authentication.
  /// </summary>
  public sealed class BasicAuthenticationPolicy : IAuthenticationPolicy
  {
    private readonly string encodedCredentials;

    /// <param name="username">The username to use for authenticating.</param>
    /// <param name="password">The password to use for authenticating.</param>
    public BasicAuthenticationPolicy(string username, string password)
    {
      Check.NotNullOrEmpty(username, nameof(username));
      Check.NotNullOrEmpty(password, nameof(password));

      this.encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
    }

    public void AttachAuthentication(IRequestBuilder builder)
    {
      builder.Headers.Add("Authorization", "Basic " + this.encodedCredentials);
    }

    public void AttachAuthentication(IRequest request)
    {
    }
  }
}