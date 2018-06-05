using System.Threading.Tasks;
using DynamicHttpClient.Attributes;
using DynamicHttpClient.Attributes.Methods;
using DynamicHttpClient.Caching;
using DynamicHttpClient.IO.Serialization;
using DynamicHttpClient.Metadata;
using DynamicHttpClient.Utilities;
using Xunit;

namespace DynamicHttpClient.Tests.Metadata
{
  public sealed class MetadataFactoryTests
  {
    [Fact]
    public void Serializer_and_deserializer_are_extracted_ok()
    {
      var method   = typeof(ITestInterface).GetMethod(nameof(ITestInterface.WithPathAndMethod));
      var metadata = MetadataFactory.CreateMetadata(method);

      Assert.NotNull(metadata.Serializer);
      Assert.NotNull(metadata.Deserializer);
    }

    [Fact]
    public void Body_metadata_is_extracted_ok()
    {
      var method   = typeof(ITestInterface).GetMethod(nameof(ITestInterface.WithBody));
      var metadata = MetadataFactory.CreateMetadata(method);

      Assert.NotNull(metadata.Body);
    }

    [Fact]
    public void Url_segments_are_extracted_ok()
    {
      var method   = typeof(ITestInterface).GetMethod(nameof(ITestInterface.WithParameters));
      var metadata = MetadataFactory.CreateMetadata(method);

      Assert.NotEmpty(metadata.UrlSegments);
    }

    [Fact]
    public void Caching_policy_is_extracted_ok()
    {
      var method   = typeof(ITestInterface).GetMethod(nameof(ITestInterface.WithCachingPolicy));
      var metadata = MetadataFactory.CreateMetadata(method);

      Assert.NotNull(metadata.CachingPolicy);
    }

    [Fact]
    public void Method_metadata_complains_about_method_attribute_missing()
    {
      Assert.Throws<InvalidMetadataException>(() =>
      {
        var method = typeof(ITestInterface).GetMethod(nameof(ITestInterface.MissingMethodAttribute));
        return MetadataFactory.CreateMetadata(method);
      });
    }

    [Fact]
    public void GetReturnType_supports_generic_task_objects()
    {
      var method     = typeof(ITestInterface).GetMethod(nameof(ITestInterface.WithTaskResult));
      var returnType = MetadataFactory.GetReturnType(method);

      Assert.True(typeof(string).IsAssignableFrom(returnType));
    }

    [Fact]
    public void Get_return_type_supports_plain_objects()
    {
      var method     = typeof(ITestInterface).GetMethod(nameof(ITestInterface.WithPlainResult));
      var returnType = MetadataFactory.GetReturnType(method);

      Assert.True(typeof(string).IsAssignableFrom(returnType));
    }

    [Fact]
    public void Inspect_type_complains_about_lack_of_serializer_and_deserializer()
    {
      Assert.Throws<InvalidMetadataException>(() => MetadataFactory.InspectType(typeof(ITestClientWithoutSerializer)));
    }

    [Fact]
    public void InspectType_complains_about_invalid_method_metadata()
    {
      Assert.Throws<InvalidMetadataException>(() => MetadataFactory.InspectType(typeof(ITestInterface)));
    }

    [Fact]
    public void Inspect_method_complains_about_lack_of_method_metadata()
    {
      Assert.Throws<InvalidMetadataException>(() => MetadataFactory.InspectMethod(typeof(ITestInterface).GetMethod(nameof(ITestInterface.MissingMethodAttribute))));
    }

    private interface ITestClientWithoutSerializer
    {
    }

    [Serializer(typeof(NewtonsoftSerializer))]
    [Deserializer(typeof(NewtonsoftDeserializer))]
    private interface ITestInterface
    {
      [Post("test/path")]
      void WithPathAndMethod();

      [Post("test/path")]
      void WithBody([Body] string messageBody);

      [Post("test/path")]
      void WithParameters(string param1, [Segment("param3")] string param2);

      void MissingMethodAttribute();

      Task<string> WithTaskResult();

      string WithPlainResult();

      [Get("/test")]
      [RelativeCache(5, TimeScale.Seconds)]
      string WithCachingPolicy();
    }
  }
}