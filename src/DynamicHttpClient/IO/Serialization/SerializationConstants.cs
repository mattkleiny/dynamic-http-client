namespace DynamicHttpClient.IO.Serialization
{
  /// <summary>
  /// Common constants related to serialization.
  /// </summary>
  internal static class SerializationConstants
  {
    /// <summary>
    /// Default <see cref="JsonSerializerSettings"/> for Newtonsoft.
    /// </summary>
    public static readonly JsonSerializerSettings DefaultSerializerSettings = new JsonSerializerSettings
    {
      TraceWriter = new DiagnosticsTraceWriter(),
#if DEBUG
      Formatting = Formatting.Indented,
#else
            Formatting = Formatting.None,
#endif
      NullValueHandling      = NullValueHandling.Ignore,
      DefaultValueHandling   = DefaultValueHandling.Ignore,
      MissingMemberHandling  = MissingMemberHandling.Ignore,
      ObjectCreationHandling = ObjectCreationHandling.Replace,
      ReferenceLoopHandling  = ReferenceLoopHandling.Error
    };
  }
}