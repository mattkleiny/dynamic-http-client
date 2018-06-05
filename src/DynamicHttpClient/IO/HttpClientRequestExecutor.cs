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

    public HttpClientRequestExecutor(HttpClient client)
    {
      Check.NotNull(client, nameof(client));

      this.client = client;
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

    public async Task<IResponse> ExecuteRequestAsync(IRequest original)
    {
      Check.That(original is HttpClientRequest, "A request created with this executor was expected.");

      var request = (HttpClientRequest) original;
      var message = request.BuildMessage();

      var response = await client.SendAsync(message);

      if (!response.IsSuccessStatusCode)
      {
        throw new HttpRequestException("An error occurred whilst executing a request.", response.StatusCode);
      }

      var rawBytes = await response.Content.ReadAsByteArrayAsync();

      return new HttpClientResponse(response, rawBytes);
    }
    
    public void Dispose()
    {
      client.Dispose();
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

        return message;
      }
    }

    [DebuggerDisplay("{Url} {StatusCode} {ContentLength}")]
    private sealed class HttpClientResponse : IResponse
    {
      private readonly HttpResponseMessage message;
      private readonly Lazy<string>        content;

      public HttpClientResponse(HttpResponseMessage message, byte[] rawBytes)
      {
        this.message = message;
        content      = new Lazy<string>(() => ContentEncoding.GetString(RawBytes));

        RawBytes = rawBytes;
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