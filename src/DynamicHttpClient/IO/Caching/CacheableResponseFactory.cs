using System;
using System.Net;
using System.Text;
using DynamicHttpClient.IO.Compression;

namespace DynamicHttpClient.IO.Caching
{
  internal static class CacheableResponseFactory
  {
    public static IResponse BuildCachedResponse(CachedRepresentation representation, IResponse response)
    {
      Check.NotNull(response, nameof(response));

      switch (representation)
      {
        case CachedRepresentation.Full:
          return response;

        case CachedRepresentation.Gzip:
          return new CompressedCacheableResponse(response, new GzipCompressor());

        case CachedRepresentation.Deflate:
          return new CompressedCacheableResponse(response, new DeflateCompressor());

        default:
          throw new NotSupportedException($"This representation is not yet supported: {representation}");
      }
    }

    private abstract class CacheableResponse : IResponse
    {
      public virtual long           ContentLength   => throw new NotSupportedException($"The {nameof(ContentLength)} is not stored in this cached form of IResponse.");
      public virtual byte[]         RawBytes        => throw new NotSupportedException($"The {nameof(RawBytes)} are not stored in this cached form of IResponse.");
      public virtual string         Content         => throw new NotSupportedException($"The {nameof(Content)} is not stored in this cached form of IResponse.");
      public virtual string         ContentType     => throw new NotSupportedException($"The {nameof(ContentType)} is not stored in this cached form of IResponse.");
      public virtual Encoding       ContentEncoding => throw new NotSupportedException($"The {nameof(ContentEncoding)} is not stored in this cached form of IResponse.");
      public virtual string         Url             => throw new NotSupportedException($"The {nameof(Url)} is not stored in this cached form of IResponse.");
      public virtual HttpStatusCode StatusCode      => throw new NotSupportedException($"The {nameof(StatusCode)} is not stored in this cached form of IResponse.");
    }

    private sealed class CompressedCacheableResponse : CacheableResponse
    {
      private readonly ICompressor compressor;

      public CompressedCacheableResponse(IResponse response, ICompressor compressor)
      {
        Check.NotNull(response,   nameof(response));
        Check.NotNull(compressor, nameof(compressor));

        this.compressor = compressor;

        ContentType     = response.ContentType;
        ContentLength   = response.ContentLength;
        ContentEncoding = response.ContentEncoding;
        Url             = response.Url;
        StatusCode      = response.StatusCode;

        CompressedBytes = compressor.Compress(response.RawBytes);
      }

      public          byte[]         CompressedBytes { get; }
      public override byte[]         RawBytes        => compressor.Decompress(CompressedBytes);
      public override string         Content         => ContentEncoding.GetString(RawBytes);
      public override string         ContentType     { get; }
      public override long           ContentLength   { get; }
      public override Encoding       ContentEncoding { get; }
      public override string         Url             { get; }
      public override HttpStatusCode StatusCode      { get; }
    }
  }
}