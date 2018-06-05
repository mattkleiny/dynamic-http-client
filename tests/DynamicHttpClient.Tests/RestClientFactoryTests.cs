using DynamicHttpClient.Metadata;
using Xunit;

namespace DynamicHttpClient.Tests
{
  public class RestClientFactoryTests
  {
    [Fact]
    public void Build_inspects_metadata_before_instantiation()
    {
      var factory = new DynamicHttpClientFactory();

      Assert.Throws<InvalidMetadataException>(() => factory.Build<RestClientFactoryTests>());
    }
  }
}