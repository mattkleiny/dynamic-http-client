// The MIT License (MIT)
// 
// Copyright (C) 2015, Matthew Kleinschafer.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace DynamicRestClient.Tests
{
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;
    using Attributes;
    using Attributes.Methods;
    using Caching;
    using DynamicRestClient.IO;
    using DynamicRestClient.IO.Serialization;
    using Xunit;

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
            Cache = new ObjectCacheAdapter(new MemoryCache("REST Cache")),
            Executor = new HttpClientRequestExecutor("http://jsonplaceholder.typicode.com/")
        };

        /// <summary>
        /// A <see cref="ITestClient"/> for our tests.
        /// </summary>
        private readonly ITestClient client = Factory.Build<ITestClient>();

        /// <summary>
        /// A simple test client for http://jsonplaceholder.typicode.com/.
        /// </summary>
        [Serializer(typeof (NewtonsoftSerializer))]
        [Deserializer(typeof (NewtonsoftDeserializer))]
        public interface ITestClient
        {
            [Get("/posts")]
            Post[] GetPosts();

            [Get("/posts/{id}")]
            Post GetPost(int id);

            [Post("/posts")]
            void CreatePost([Body] Post post);

            [Delete("/posts/{id}")]
            void DeletePost(int id);

            [Get("/posts")]
            Task<Post[]> GetPostsAsync();

            [Get("/posts/{id}")]
            Task<Post> GetPostAsync(int id);

            [Post("/posts")]
            Task CreatePostAsync([Body] Post post);

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

        #region Blocking Methods

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
            this.client.CreatePost(new Post
            {
                Title = "Test",
                Body = "Test test test"
            });
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
            await this.client.CreatePostAsync(new Post
            {
                Title = "Test",
                Body = "Test test test"
            });
        }

        [Fact, Category(Categories.Slow)]
        public async Task DeletePostAsync_Succeeds()
        {
            await this.client.DeletePostAsync(66);
        }

        #endregion
    }
}
