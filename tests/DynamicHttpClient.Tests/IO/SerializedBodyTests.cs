using AutoFixture;
using DynamicHttpClient.IO;
using DynamicHttpClient.IO.Serialization;
using NSubstitute;
using Xunit;

namespace DynamicHttpClient.Tests.IO
{
  public class SerializedBodyTests : TestCase
  {
    [Fact]
    public void Content_delegates_to_serialization_mechanism()
    {
      var serializer = Fixture.Create<ISerializer>();
      var target     = new object();
      var body       = new SerializedBody(target, serializer);

      Assert.NotNull(body.Content); // invoke the Content property to hit the serializer

      serializer.Received().Serialize(typeof(object), target);
    }
  }
}