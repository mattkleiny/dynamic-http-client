namespace DynamicHttpClient.Proxy
{
  /// <summary>
  /// A supplier for Castle dynamic proxies.
  /// </summary>
  internal static class DynamicProxyFactory
  {
    /// <summary>
    /// The <see cref="ProxyGenerator"/> to use. We want to build in-memory assemblies, and we'll cache the results manually.
    /// </summary>
    private static readonly ProxyGenerator Generator = new ProxyGenerator(new DefaultProxyBuilder())
    {
      Logger = new CastleLoggerAdapter(LogManager.GetLogger(typeof(DynamicProxyFactory)))
      {
        Level = LoggerLevel.Warn
      }
    };

    /// <summary>
    /// Builds a proxy of <see cref="TType"/> with the given <see cref="IInterceptor"/>.
    /// </summary>
    public static TType BuildProxy<TType>(IInterceptor interceptor)
    {
      Check.NotNull(interceptor, nameof(interceptor));

      return (TType) Generator.CreateInterfaceProxyWithoutTarget(typeof(TType), interceptor);
    }
  }
}