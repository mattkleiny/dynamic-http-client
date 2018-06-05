using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using DynamicHttpClient.Attributes;
using DynamicHttpClient.Attributes.Methods;
using DynamicHttpClient.Caching;
using DynamicHttpClient.IO;
using DynamicHttpClient.IO.Caching;
using DynamicHttpClient.IO.Serialization;
using DynamicHttpClient.Utilities;
using Xunit;

namespace DynamicHttpClient.Tests
{
  public class IntegrationTests
  {
    private static readonly DynamicHttpClientFactory Factory = new DynamicHttpClientFactory
    {
      Cache    = new ObjectCacheAdapter(new MemoryCache("REST Cache")),
      Executor = new HttpClientRequestExecutor("http://jsonplaceholder.typicode.com/")
    };

    private readonly ITestClient client = Factory.Build<ITestClient>();

    [Fact]
    public void GetPosts_succeeds()
    {
      var posts = client.GetPosts();

      Assert.NotNull(posts);
      Assert.True(posts.Any());
    }

    [Fact]
    public void GetPost_succeeds()
    {
      var post = client.GetPost(1);

      Assert.NotNull(post);
    }

    [Fact]
    public void CreatePost_succeeds()
    {
      var post = client.CreatePost(new Post
      {
        Title = "Test",
        Body  = "Test test test"
      });

      Assert.NotNull(post);
    }

    [Fact]
    public void DeletePost_succeeds()
    {
      client.DeletePost(66);
    }

    [Fact]
    public async Task GetPostsAsync_succeeds()
    {
      var posts = await client.GetPostsAsync();

      Assert.NotNull(posts);
      Assert.True(posts.Any());
    }

    [Fact]
    public async Task GetPostAsync_succeeds()
    {
      var post = await client.GetPostAsync(1);

      Assert.NotNull(post);
    }

    [Fact]
    public async Task CreatePostAsync_succeeds()
    {
      var post = await client.CreatePostAsync(new Post
      {
        Title = "Test",
        Body  = "Test test test"
      });

      Assert.NotNull(post);
    }

    [Fact]
    public async Task DeletePostAsync_succeeds()
    {
      await client.DeletePostAsync(66);
    }

    [Serializer(typeof(NewtonsoftSerializer))]
    [Deserializer(typeof(NewtonsoftDeserializer))]
    [Version(1, 0, 0)]
    [Header("Disposition", "Friendly, thanks for sharing!")]
    public interface ITestClient
    {
      [Get("/posts")]
      [RelativeCache(60, TimeScale.Seconds, Representation = CachedRepresentation.Gzip)]
      Post[] GetPosts();

      [Get("/posts/{id}")]
      Post GetPost(int id);

      [Post("/posts")]
      Post CreatePost([Body] Post post);

      [Delete("/posts/{id}")]
      void DeletePost(int id);

      [Get("/posts")]
      Task<Post[]> GetPostsAsync();

      [Get("/posts/{id}")]
      Task<Post> GetPostAsync(int id);

      [Post("/posts")]
      Task<Post> CreatePostAsync([Body] Post post);

      [Delete("/posts/{id}")]
      Task DeletePostAsync(int id);
    }

    [DataContract]
    public sealed class Post
    {
      [DataMember(Name = "userId")]
      public int UserId { get; set; }

      [DataMember(Name = "id")]
      public int Id { get; set; }

      [DataMember(Name = "title")]
      public string Title { get; set; }

      [DataMember(Name = "body")]
      public string Body { get; set; }
    }
  }
}