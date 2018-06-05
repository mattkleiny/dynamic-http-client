using System.Threading.Tasks;
using DynamicHttpClient.Caching;
using DynamicHttpClient.IO;
using DynamicHttpClient.Metadata;
using DynamicHttpClient.Proxy;

namespace DynamicHttpClient
{
  /// <summary>
  /// Builder pattern for a REST client implementation.
  /// </summary>
  public sealed class RestClientFactory
  {
    /// <summary>
    /// Default constructor.
    /// </summary>
    public RestClientFactory()
    {
      Executor  = new NullRequestExecutor();
      Cache     = new NullCache();
      Scheduler = TaskScheduler.Default;
    }

    /// <summary>
    /// The <see cref="IRequestExecutor"/> to use.
    /// </summary>
    public IRequestExecutor Executor { get; set; }

    /// <summary>
    /// The <see cref="ICache"/> to use.
    /// </summary>
    public ICache Cache { get; set; }

    /// <summary>
    /// The <see cref="TaskScheduler"/> to use for asynchronous requests.
    /// </summary>
    public TaskScheduler Scheduler { get; set; }

    /// <summary>
    /// Builds the resultant REST client.
    /// </summary>
    public TClient Build<TClient>()
    {
      Check.NotNull(Executor,  nameof(Executor));
      Check.NotNull(Cache,     nameof(Cache));
      Check.NotNull(Scheduler, nameof(Scheduler));

      MetadataFactory.InspectType(typeof(TClient));

      var interceptor = new RestClientInterceptor(Executor, Cache, Scheduler);

      return DynamicProxyFactory.BuildProxy<TClient>(interceptor);
    }
  }
}