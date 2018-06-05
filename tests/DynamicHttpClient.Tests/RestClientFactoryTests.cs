using DynamicHttpClient.Metadata;
using Xunit;

namespace DynamicHttpClient.Tests
{
  public class RestClientFactoryTests
  {
    [Fact]
    public void Build_Inspects_Metadata_Before_Instantiation()
    {
      var factory = new RestClientFactory();

      Assert.Throws<InvalidMetadataException>(() => factory.Build<RestClientFactoryTests>());
    }
  }
}