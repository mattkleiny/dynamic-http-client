using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DynamicHttpClient.IO
{
  public sealed class NullRequestExecutor : IRequestExecutor
  {
    public IRequestBuilder PrepareRequest()
    {
      return new RequestBuilder<NullRequest>();
    }

    public Task<IResponse> ExecuteRequestAsync(IRequest request)
    {
      return Task.FromResult<IResponse>(new NullResponse(request.Url));
    }

    private sealed class NullRequest : AbstractRequest
    {
    }

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