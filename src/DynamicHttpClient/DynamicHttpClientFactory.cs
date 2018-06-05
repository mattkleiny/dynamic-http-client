using DynamicHttpClient.Caching;
using DynamicHttpClient.IO;
using DynamicHttpClient.Metadata;

namespace DynamicHttpClient
{
  public static class DynamicHttpClientFactory
  {
    public static TClient Build<TClient>(IRequestExecutor executor)
      where TClient : class
    {
      return Build<TClient>(executor, new NullCache());
    }

    public static TClient Build<TClient>(IRequestExecutor executor, ICache cache)
      where TClient : class
    {
      Check.NotNull(executor, nameof(executor));
      Check.NotNull(cache,    nameof(cache));

      MetadataFactory.InspectType(typeof(TClient));

      return DynamicHttpClientProxy.Create<TClient>(executor, cache);
    }
  }
}