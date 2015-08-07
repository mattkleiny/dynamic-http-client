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

namespace DynamicRestClient.Tests.Metadata
{
    using System.Threading.Tasks;
    using Attributes;
    using Attributes.Methods;
    using Caching;
    using DynamicRestClient.IO.Serialization;
    using DynamicRestClient.Metadata;
    using DynamicRestClient.Utilities;
    using Xunit;

    public sealed class MetadataFactoryTests
    {
        [Fact]
        public void Serializer_And_Deserializer_Extracted()
        {
            var method = typeof (ITestInterface).GetMethod("WithPathAndMethod");
            var metadata = MetadataFactory.CreateMetadata(method);

            Assert.NotNull(metadata.Serializer);
            Assert.NotNull(metadata.Deserializer);
        }

        [Fact]
        public void Body_Metadata_Extracted()
        {
            var method = typeof (ITestInterface).GetMethod("WithBody");
            var metadata = MetadataFactory.CreateMetadata(method);

            Assert.NotNull(metadata.Body);
        }

        [Fact]
        public void Url_Segments_Extracted()
        {
            var method = typeof (ITestInterface).GetMethod("WithParameters");
            var metadata = MetadataFactory.CreateMetadata(method);

            Assert.NotEmpty(metadata.UrlSegments);
        }

        [Fact]
        public void Caching_Policy_Extracted()
        {
            var method = typeof (ITestInterface).GetMethod("WithCachingPolicy");
            var metadata = MetadataFactory.CreateMetadata(method);

            Assert.NotNull(metadata.CachingPolicy);
        }

        [Fact]
        public void Method_Metadata_Complains_About_Method_Attribute_Missing()
        {
            Assert.Throws<InvalidMetadataException>(() =>
            {
                var method = typeof (ITestInterface).GetMethod("MissingMethodAttribute");
                return MetadataFactory.CreateMetadata(method);
            });
        }

        [Fact]
        public void GetReturnType_Supports_Generic_Task_Objects()
        {
            var method = typeof (ITestInterface).GetMethod("WithTaskResult");
            var returnType = MetadataFactory.GetReturnType(method);

            Assert.True(typeof (string).IsAssignableFrom(returnType));
        }

        [Fact]
        public void Get_Return_Type_Supports_Plain_Objects()
        {
            var method = typeof (ITestInterface).GetMethod("WithPlainResult");
            var returnType = MetadataFactory.GetReturnType(method);

            Assert.True(typeof (string).IsAssignableFrom(returnType));
        }

        [Fact]
        public void Inspect_Type_Complains_About_Lack_Of_Serializer_And_Deserializer()
        {
            Assert.Throws<InvalidMetadataException>(() => MetadataFactory.InspectType(typeof (ITestClientWithoutSerializer)));
        }

        [Fact]
        public void InspectType_Complains_About_Invalid_Method_Metadata()
        {
            Assert.Throws<InvalidMetadataException>(() => MetadataFactory.InspectType(typeof (ITestInterface)));
        }

        [Fact]
        public void Inspect_Method_Complains_About_Lack_Of_Method_Metadata()
        {
            Assert.Throws<InvalidMetadataException>(() => MetadataFactory.InspectMethod(typeof (ITestInterface).GetMethod("MissingMethodAttribute")));
        }

        /// <summary>
        /// A plain old interface without [Serializer] or [Deserializer] attributes (for generating exceptions).
        /// </summary>
        private interface ITestClientWithoutSerializer
        {
        }

        /// <summary>
        /// A test interface with a variety of different metadata states
        /// </summary>
        [Serializer(typeof (NewtonsoftSerializer))]
        [Deserializer(typeof (NewtonsoftDeserializer))]
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
