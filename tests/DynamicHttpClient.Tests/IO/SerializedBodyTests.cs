using Xunit;

namespace DynamicHttpClient.Tests.IO
{
  public class SerializedBodyTests : TestFixture
  {
    [Fact]
    public void Content_Delegates_To_Serialization_Mechanism()
    {
      var serializer = Fixture.Create<ISerializer>();
      var target     = new object();
      var body       = new SerializedBody(target, serializer);

      Assert.NotNull(body.Content); // invoke the Content property to hit the serializer

      serializer.Received().Serialize(typeof(object), target);
    }
  }
}