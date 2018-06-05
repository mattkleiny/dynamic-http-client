using Newtonsoft.Json;

namespace DynamicHttpClient.IO.Serialization
{
  internal static class SerializationSettings
  {
    public static readonly JsonSerializerSettings Default = new JsonSerializerSettings
    {
      Formatting             = Formatting.Indented,
      NullValueHandling      = NullValueHandling.Ignore,
      DefaultValueHandling   = DefaultValueHandling.Ignore,
      MissingMemberHandling  = MissingMemberHandling.Ignore,
      ObjectCreationHandling = ObjectCreationHandling.Replace,
      ReferenceLoopHandling  = ReferenceLoopHandling.Error
    };
  }
}