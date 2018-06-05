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

        case CachedRepresentation.Gzipped:
          return new CompressedCacheableResponse(response, new GzipCompressor());

        case CachedRepresentation.Deflate:
          return new CompressedCacheableResponse(response, new DeflateCompressor());

        default:
          throw new NotSupportedException($"This representation is not yet supported: {representation}");
      }
    }

    internal abstract class CacheableResponse : IResponse
    {
      public virtual long           ContentLength   => throw new NotSupportedException("The content length is not stored in this cacheable form of IResponse.");
      public virtual byte[]         RawBytes        => throw new NotSupportedException("The raw bytes are not stored in this cacheable form of IResponse.");
      public virtual string         Content         => throw new NotSupportedException("The content is not stored in this cacheable form of IResponse.");
      public virtual string         ContentType     => throw new NotSupportedException("The content type is not stored in this cacheable form of IResponse.");
      public virtual Encoding       ContentEncoding => throw new NotSupportedException("The content encoding is not stored in this cacheable form of IResponse.");
      public virtual string         Url             => throw new NotSupportedException("The url is not stored in this cacheable form of IResponse.");
      public virtual HttpStatusCode StatusCode      => throw new NotSupportedException("The status code is not stored in this cacheable form of IResponse.");
    }

    internal sealed class CompressedCacheableResponse : CacheableResponse
    {
      private readonly ICompressor compressor;
      private readonly Encoding    contentEncoding;

      public CompressedCacheableResponse(IResponse response, ICompressor compressor)
      {
        Check.NotNull(response,   nameof(response));
        Check.NotNull(compressor, nameof(compressor));

        this.compressor = compressor;

        contentEncoding = response.ContentEncoding;

        StatusCode      = response.StatusCode;
        ContentType     = response.ContentType;
        ContentLength   = response.ContentLength;
        CompressedBytes = compressor.Compress(response.RawBytes);
      }

      public          byte[]         CompressedBytes { get; }
      public override byte[]         RawBytes        => compressor.Decompress(CompressedBytes);
      public override string         Content         => contentEncoding.GetString(RawBytes);
      public override string         ContentType     { get; }
      public override long           ContentLength   { get; }
      public override Encoding       ContentEncoding => contentEncoding;
      public override HttpStatusCode StatusCode      { get; }
    }
  }
}