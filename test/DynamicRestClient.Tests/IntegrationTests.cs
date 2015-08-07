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
    using DynamicRestClient.IO.Caching;
    using DynamicRestClient.IO.Serialization;
    using DynamicRestClient.Utilities;
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
        /// A <see cref="IJsonPlaceholderClient"/> for our tests.
        /// </summary>
        private readonly IJsonPlaceholderClient client = Factory.Build<IJsonPlaceholderClient>();

        [Fact, Category(Categories.Slow)]
        public async Task GetPosts_Able_To_Request_And_Retrieve_Post_Information()
        {
            var posts = await this.client.GetPosts();

            Assert.NotNull(posts);
            Assert.True(posts.Any());
        }

        /// <summary>
        /// A simple test client for http://jsonplaceholder.typicode.com/.
        /// </summary>
        [Serializer(typeof (NewtonsoftSerializer))]
        [Deserializer(typeof (NewtonsoftDeserializer))]
        public interface IJsonPlaceholderClient
        {
            [Get("/posts")]
            [RelativeCache(60, TimeScale.Seconds, Representation = CachedRepresentation.Compressed)]
            Task<Post[]> GetPosts();
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
    }
}
