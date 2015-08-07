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
    using System;
    using Xunit;

    public class TimeScaleHelpersTests
    {
        [Fact]
        public void BuildTimeSpan_Honours_Interval_And_Scale()
        {
            Assert.Equal(TimeSpan.FromMilliseconds(10), TimeScaleHelpers.BuildTimeSpan(10, TimeScale.Milliseconds));
            Assert.Equal(TimeSpan.FromSeconds(10), TimeScaleHelpers.BuildTimeSpan(10, TimeScale.Seconds));
            Assert.Equal(TimeSpan.FromHours(10), TimeScaleHelpers.BuildTimeSpan(10, TimeScale.Hours));
            Assert.Equal(TimeSpan.FromMinutes(10), TimeScaleHelpers.BuildTimeSpan(10, TimeScale.Minutes));
            Assert.Equal(TimeSpan.FromDays(10), TimeScaleHelpers.BuildTimeSpan(10, TimeScale.Days));
        }

        [Fact]
        public void BuildTimeSpan_Complains_About_Unknown_Scale()
        {
            Assert.Throws<ArgumentException>(() => TimeScaleHelpers.BuildTimeSpan(10, (TimeScale) 15));
        }
    }
}
