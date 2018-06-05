using System.Threading.Tasks;

namespace DynamicHttpClient.IO.Authentication
{
  /// <summary>
  /// A <see cref="IRequestExecutor"/> decorator that supports a <see cref="IAuthenticationPolicy"/>.
  /// </summary>
  public sealed class AuthenticatedRequestExecutor : IRequestExecutor
  {
    private readonly IRequestExecutor      executor;
    private readonly IAuthenticationPolicy policy;

    /// <param name="executor">The inner <see cref="IRequestExecutor"/>.</param>
    /// <param name="policy">The <see cref="IAuthenticationPolicy"/> to use.</param>
    public AuthenticatedRequestExecutor(IRequestExecutor executor, IAuthenticationPolicy policy)
    {
      Check.NotNull(executor, nameof(executor));
      Check.NotNull(policy,   nameof(policy));

      this.executor = executor;
      this.policy   = policy;
    }

    public IRequestBuilder BuildRequest()
    {
      var builder = this.executor.BuildRequest();

      this.policy.AttachAuthentication(builder);

      return builder;
    }

    public IResponse ExecuteRequest(IRequest request)
    {
      Check.NotNull(request, nameof(request));

      this.policy.AttachAuthentication(request);

      return this.executor.ExecuteRequest(request);
    }

    public Task<IResponse> ExecuteRequestAsync(IRequest request)
    {
      Check.NotNull(request, nameof(request));

      this.policy.AttachAuthentication(request);

      return this.executor.ExecuteRequestAsync(request);
    }
  }
}