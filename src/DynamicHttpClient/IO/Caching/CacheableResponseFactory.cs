using System;
using System.Net;
using System.Text;
using DynamicHttpClient.IO.Compression;

namespace DynamicHttpClient.IO.Caching
{
  /// <summary>
  /// Static factories for cached <see cref="IResponse"/>s.
  /// </summary>
  internal static class CacheableResponseFactory
  {
    /// <summary>
    /// The supplier to use for the <see cref="ICompressor"/>.
    /// </summary>
    private static readonly Func<ICompressor> CompressorSupplier = () => new GzipCompressor();

    /// <summary>
    /// Builds a cacheable <see cref="IResponse"/> of the given type.
    /// </summary>
    public static IResponse BuildCachedResponse(CachedRepresentation representation, IResponse response)
    {
      Check.NotNull(response, nameof(response));

      switch (representation)
      {
        case CachedRepresentation.Normal:
          return response;

        case CachedRepresentation.Compressed:
          return new CompressedCacheableResponse(response, CompressorSupplier());

        default:
          throw new NotSupportedException("This representation is not yet supported: " + representation);
      }
    }

    /// <summary>
    /// Base class for any cacheable <see cref="IResponse"/>.
    /// </summary>
    internal abstract class CacheableResponse : IResponse
    {
      public virtual long ContentLength
      {
        get { throw new NotSupportedException("The content length is not stored in this cacheable form of IResponse."); }
      }

      public virtual byte[] RawBytes
      {
        get { throw new NotSupportedException("The raw bytes are not stored in this cacheable form of IResponse."); }
      }

      public virtual string Content
      {
        get { throw new NotSupportedException("The content is not stored in this cacheable form of IResponse."); }
      }

      public virtual string ContentType
      {
        get { throw new NotSupportedException("The content type is not stored in this cacheable form of IResponse."); }
      }

      public virtual Encoding ContentEncoding
      {
        get { throw new NotSupportedException("The content encoding is not stored in this cacheable form of IResponse."); }
      }

      public virtual string Url
      {
        get { throw new NotSupportedException("The url is not stored in this cacheable form of IResponse."); }
      }

      public virtual HttpStatusCode StatusCode
      {
        get { throw new NotSupportedException("The status code is not stored in this cacheable form of IResponse."); }
      }
    }

    /// <summary>
    /// A cacheable <see cref="IResponse"/> that stores the content of the response in a compressed form; no header, server or uri information.
    /// </summary>
    internal sealed class CompressedCacheableResponse : CacheableResponse
    {
      private readonly ICompressor compressor;
      private readonly Encoding    contentEncoding;

      public CompressedCacheableResponse(IResponse response, ICompressor compressor)
      {
        Check.NotNull(response,   nameof(response));
        Check.NotNull(compressor, nameof(compressor));

        this.compressor      = compressor;
        this.contentEncoding = response.ContentEncoding;

        StatusCode      = response.StatusCode;
        ContentType     = response.ContentType;
        ContentLength   = response.ContentLength;
        CompressedBytes = compressor.Compress(response.RawBytes);
      }

      public byte[] CompressedBytes { get; }

      public override byte[] RawBytes => this.compressor.Decompress(CompressedBytes);

      public override string Content => this.contentEncoding.GetString(RawBytes);

      public override string ContentType { get; }

      public override long ContentLength { get; }

      public override Encoding ContentEncoding => this.contentEncoding;

      public override HttpStatusCode StatusCode { get; }
    }
  }
}