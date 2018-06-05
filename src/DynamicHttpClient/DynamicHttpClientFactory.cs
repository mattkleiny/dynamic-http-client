using DynamicHttpClient.Caching;
using DynamicHttpClient.IO;
using DynamicHttpClient.Metadata;

namespace DynamicHttpClient
{
  public sealed class DynamicHttpClientFactory
  {
    public IRequestExecutor Executor { get; set; } = new NullRequestExecutor();
    public ICache           Cache    { get; set; } = new NullCache();

    public TClient Build<TClient>()
      where TClient : class
    {
      Check.NotNull(Executor, nameof(Executor));
      Check.NotNull(Cache,    nameof(Cache));

      MetadataFactory.InspectType(typeof(TClient));

      return DynamicHttpClientProxy.Create<TClient>(Executor, Cache);
    }
  }
}