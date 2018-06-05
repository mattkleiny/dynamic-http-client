using System.Threading.Tasks;

namespace DynamicHttpClient.IO.Authentication
{
  public sealed class AuthenticatedRequestExecutor : IRequestExecutor
  {
    private readonly IRequestExecutor      executor;
    private readonly IAuthenticationPolicy policy;

    public AuthenticatedRequestExecutor(IRequestExecutor executor, IAuthenticationPolicy policy)
    {
      Check.NotNull(executor, nameof(executor));
      Check.NotNull(policy,   nameof(policy));

      this.executor = executor;
      this.policy   = policy;
    }

    public IRequestBuilder PrepareRequest()
    {
      var builder = executor.PrepareRequest();

      policy.AttachAuthentication(builder);

      return builder;
    }

    public Task<IResponse> ExecuteRequestAsync(IRequest request)
    {
      Check.NotNull(request, nameof(request));

      policy.AttachAuthentication(request);

      return executor.ExecuteRequestAsync(request);
    }

    public void Dispose()
    {
      executor.Dispose();
    }
  }
}