using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DynamicHttpClient.IO.Caching;

namespace DynamicHttpClient.IO
{
  public sealed class HttpClientRequestExecutor : IRequestExecutor
  {
    private readonly HttpClient client;

    public HttpClientRequestExecutor()
      : this(string.Empty)
    {
    }

    public HttpClientRequestExecutor(string baseUri)
    {
      client = new HttpClient(new HttpClientHandler());

      if (!string.IsNullOrWhiteSpace(baseUri))
      {
        client.BaseAddress = new Uri(baseUri, UriKind.Absolute);
      }
    }

    public HttpRequestHeaders DefaultRequestHeaders => client.DefaultRequestHeaders;
    public Uri                BaseAddress           => client.BaseAddress;

    public TimeSpan Timeout
    {
      get => client.Timeout;
      set => client.Timeout = value;
    }

    public long MaxResponseContentBufferSize
    {
      get => client.MaxResponseContentBufferSize;
      set => client.MaxResponseContentBufferSize = value;
    }

    public IRequestBuilder PrepareRequest()
    {
      return new RequestBuilder<HttpClientRequest>();
    }

    public IResponse ExecuteRequest(IRequest request)
    {
      Check.That(request is HttpClientRequest, "A request created with this executor was expected.");

      return ExecuteRequestAsync(request).Result;
    }

    public Task<IResponse> ExecuteRequestAsync(IRequest original)
    {
      Check.That(original is HttpClientRequest, "A request created with this executor was expected.");

      var request = (HttpClientRequest) original;
      var message = request.BuildMessage();

      return client.SendAsync(message).ContinueWith<IResponse>(task =>
      {
        var result = task.Result;
        try
        {
          result.EnsureSuccessStatusCode();
          return new HttpClientResponse(result);
        }
        catch (HttpRequestException e)
        {
          throw new RequestExecutorException("An error occurred whilst executing a request.", result.StatusCode, e);
        }
      });
    }

    [DebuggerDisplay("{Method} {Url}")]
    private sealed class HttpClientRequest : AbstractRequest, ICacheKeyProvider
    {
      public string CacheKey => RequestHelpers.SubstituteUrlParameters(Url, Segments, Uri.EscapeUriString);

      public HttpRequestMessage BuildMessage()
      {
        var message = new HttpRequestMessage(Method, RequestHelpers.SubstituteUrlParameters(Url, Segments));

        foreach (var header in Headers)
        {
          message.Headers.Add(header.Key, header.Value);
        }

        if (Body != null)
        {
          message.Content = new StringContent(Body.Content, Encoding.UTF8, Body.ContentType);
        }

        if (Timeout.HasValue)
        {
          throw new NotSupportedException("HttpClientRequestExecutor currently doesn't support per-request timeouts.");
        }

        return message;
      }
    }

    [DebuggerDisplay("{Url} {StatusCode} {ContentLength}")]
    private sealed class HttpClientResponse : IResponse
    {
      private readonly Lazy<string>        content;
      private readonly HttpResponseMessage message;

      public HttpClientResponse(HttpResponseMessage message)
      {
        this.message = message;
        content      = new Lazy<string>(() => ContentEncoding.GetString(RawBytes));

        RawBytes = message.Content.ReadAsByteArrayAsync().Result;
      }

      public byte[] RawBytes { get; }

      public long           ContentLength   => RawBytes.Length;
      public string         Content         => content.Value;
      public string         ContentType     => message.Content.Headers.ContentType.MediaType;
      public Encoding       ContentEncoding => Encoding.UTF8;
      public string         Url             => message.RequestMessage.RequestUri.ToString();
      public HttpStatusCode StatusCode      => message.StatusCode;
    }
  }
}