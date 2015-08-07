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

namespace DynamicRestClient.IO
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Static helpers related to <see cref="IRequest"/>s.
    /// </summary>
    public static class RequestHelpers
    {
        /// <summary>
        /// Substitutes the given URL segment parameters in the given string using the given delegate to encode parameters.
        /// </summary>
        public static string SubstituteUrlParameters(string url, IEnumerable<KeyValuePair<string, string>> segments)
        {
            return SubstituteUrlParameters(url, segments, _ => _);
        }

        /// <summary>
        /// Substitutes the given URL segment parameters in the given string using the given delegate to encode parameters.
        /// </summary>
        public static string SubstituteUrlParameters(string url, IEnumerable<KeyValuePair<string, string>> segments, Func<string, string> urlEncodeDelegate)
        {
            Check.NotNullOrEmpty(url, "A valid url was expected.");
            Check.NotNull(segments, "A valid segment sequence was expected.");
            Check.NotNull(urlEncodeDelegate, "A valid url encoding delegate was expected.");

            var builder = new StringBuilder(url);

            foreach (var segment in segments)
            {
                builder.Replace("{" + segment.Key + "}", urlEncodeDelegate(segment.Value));
            }

            return builder.ToString();
        }
    }
}
