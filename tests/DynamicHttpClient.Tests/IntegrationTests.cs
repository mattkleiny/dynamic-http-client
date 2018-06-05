using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace DynamicHttpClient.Tests
{
  /// <summary>
  /// Basic integration tests for consuming a real REST interface.
  /// </summary>
  public class IntegrationTests
  {
    /// <summary>
    /// The <see cref="RestClientFactory"/> for our integration test clients.
    /// </summary>
    private static readonly RestClientFactory Factory = new RestClientFactory
    {
      Cache    = new ObjectCacheAdapter(new MemoryCache("REST Cache")),
      Executor = new HttpClientRequestExecutor("http://jsonplaceholder.typicode.com/")
    };

    /// <summary>
    /// A <see cref="ITestClient"/> for our tests.
    /// </summary>
    private readonly ITestClient client = Factory.Build<ITestClient>();

    /// <summary>
    /// A simple test client for http://jsonplaceholder.typicode.com/.
    /// </summary>
    [Serializer(typeof(NewtonsoftSerializer))]
    [Deserializer(typeof(NewtonsoftDeserializer))]
    [Header("Disposition", "Friendly, thanks for sharing!")]
    public interface ITestClient
    {
      [Get("/posts")]
      [RelativeCache(60, TimeScale.Seconds, Representation = CachedRepresentation.Compressed)]
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

    /// <summary>
    /// Represents a post on http://jsonplaceholder.typicode.com/posts/.
    /// </summary>
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

    #region Sync Methods

    [Fact, Category(Categories.Slow)]
    public void GetPosts_Succeeds()
    {
      var posts = this.client.GetPosts();

      Assert.NotNull(posts);
      Assert.True(posts.Any());
    }

    [Fact, Category(Categories.Slow)]
    public void GetPost_Succeeds()
    {
      var post = this.client.GetPost(1);

      Assert.NotNull(post);
    }

    [Fact, Category(Categories.Slow)]
    public void CreatePost_Succeeds()
    {
      var post = this.client.CreatePost(new Post
      {
        Title = "Test",
        Body  = "Test test test"
      });

      Assert.NotNull(post);
    }

    [Fact, Category(Categories.Slow)]
    public void DeletePost_Succeeds()
    {
      this.client.DeletePost(66);
    }

    #endregion

    #region Async Methods

    [Fact, Category(Categories.Slow)]
    public async Task GetPostsAsync_Succeeds()
    {
      var posts = await this.client.GetPostsAsync();

      Assert.NotNull(posts);
      Assert.True(posts.Any());
    }

    [Fact, Category(Categories.Slow)]
    public async Task GetPostAsync_Succeeds()
    {
      var post = await this.client.GetPostAsync(1);

      Assert.NotNull(post);
    }

    [Fact, Category(Categories.Slow)]
    public async Task CreatePostAsync_Succeeds()
    {
      var post = await this.client.CreatePostAsync(new Post
      {
        Title = "Test",
        Body  = "Test test test"
      });

      Assert.NotNull(post);
    }

    [Fact, Category(Categories.Slow)]
    public async Task DeletePostAsync_Succeeds()
    {
      await this.client.DeletePostAsync(66);
    }

    #endregion
  }
}