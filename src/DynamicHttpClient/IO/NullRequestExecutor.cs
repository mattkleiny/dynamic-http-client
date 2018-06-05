using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DynamicHttpClient.IO
{
  /// <summary>
  /// A <see cref="IRequestExecutor"/> that does nothing and mocks both the <see cref="IRequest"/>s and <see cref="IResponse"/>s.
  /// </summary>
  public sealed class NullRequestExecutor : IRequestExecutor
  {
    public IRequestBuilder BuildRequest()
    {
      return new RequestBuilder<NullRequest>();
    }

    public IResponse ExecuteRequest(IRequest request)
    {
      Check.NotNull(request, nameof(request));

      return new NullResponse(request.Url);
    }

    public Task<IResponse> ExecuteRequestAsync(IRequest request)
    {
      return Task.FromResult(ExecuteRequest(request));
    }

    /// <summary>
    /// A null <see cref="IRequest"/>.
    /// </summary>
    private sealed class NullRequest : AbstractRequest
    {
    }

    /// <summary>
    /// A null <see cref="IResponse"/>.
    /// </summary>
    private sealed class NullResponse : IResponse
    {
      public NullResponse(string url)
      {
        Url = url;
      }

      public byte[] RawBytes => new byte[0];

      public string Content => string.Empty;

      public string ContentType => string.Empty;

      public long ContentLength => 0;

      public Encoding ContentEncoding => Encoding.UTF8;

      public string Url { get; }

      public HttpStatusCode StatusCode => HttpStatusCode.OK;
    }
  }
}