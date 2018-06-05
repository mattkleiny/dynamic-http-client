using System.Collections.Generic;
using System.Linq;

namespace DynamicHttpClient.IO.Authentication
{
  public sealed class CompositeAuthenticationPolicy : IAuthenticationPolicy
  {
    private readonly IEnumerable<IAuthenticationPolicy> policies;

    public CompositeAuthenticationPolicy(params IAuthenticationPolicy[] policies)
      : this(policies.ToList())
    {
    }

    public CompositeAuthenticationPolicy(IEnumerable<IAuthenticationPolicy> policies)
    {
      Check.NotNull(policies, nameof(policies));

      this.policies = policies;
    }

    public void AttachAuthentication(IRequestBuilder builder)
    {
      Check.NotNull(builder, nameof(builder));

      foreach (var policy in policies)
      {
        policy.AttachAuthentication(builder);
      }
    }

    public void AttachAuthentication(IRequest request)
    {
      Check.NotNull(request, nameof(request));

      foreach (var policy in policies)
      {
        policy.AttachAuthentication(request);
      }
    }
  }
}