using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DynamicHttpClient.IO.Serialization
{
  internal static class SerializationSettings
  {
    public static readonly JsonSerializerSettings Default = new JsonSerializerSettings
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