using System.Collections.Generic;
using System.Linq;

namespace DynamicHttpClient.IO.Authentication
{
  /// <summary>
  /// A <see cref="IAuthenticationPolicy"/> that composes other <see cref="IAuthenticationPolicy"/>s.
  /// </summary>
  public sealed class CompositeAuthenticationPolicy : IAuthenticationPolicy
  {
    private readonly IEnumerable<IAuthenticationPolicy> policies;

    /// <param name="policies">The <see cref="IAuthenticationPolicy"/> to compose.</param>
    public CompositeAuthenticationPolicy(params IAuthenticationPolicy[] policies)
      : this(policies.ToList())
    {
    }

    /// <param name="policies">The <see cref="IAuthenticationPolicy"/> to compose.</param>
    public CompositeAuthenticationPolicy(IEnumerable<IAuthenticationPolicy> policies)
    {
      Check.NotNull(policies, nameof(policies));

      this.policies = policies;
    }

    public void AttachAuthentication(IRequestBuilder builder)
    {
      Check.NotNull(builder, nameof(builder));

      foreach (var policy in this.policies)
      {
        policy.AttachAuthentication(builder);
      }
    }

    public void AttachAuthentication(IRequest request)
    {
      Check.NotNull(request, nameof(request));

      foreach (var policy in this.policies)
      {
        policy.AttachAuthentication(request);
      }
    }
  }
}