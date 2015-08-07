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

namespace DynamicRestClient
{
    using System;

    /// <summary>
    /// Static utilities for computing <see cref="TimeSpan"/>s from <see cref="TimeScale"/>s.
    /// </summary>
    internal static class TimeScaleHelpers
    {
        /// <summary>
        /// Given an <see cref="interval"/> and a <see cref="scale"/>, 
        /// builds a <see cref="TimeSpan"/> representing the measure.
        /// </summary>
        public static TimeSpan BuildTimeSpan(int interval, TimeScale scale)
        {
            Check.That(interval > 0, "A positive interval was expected.");

            switch (scale)
            {
                case TimeScale.Milliseconds:
                    return TimeSpan.FromMilliseconds(interval);

                case TimeScale.Seconds:
                    return TimeSpan.FromSeconds(interval);

                case TimeScale.Minutes:
                    return TimeSpan.FromMinutes(interval);

                case TimeScale.Hours:
                    return TimeSpan.FromHours(interval);

                case TimeScale.Days:
                    return TimeSpan.FromDays(interval);

                default:
                    throw new ArgumentException("An unrecognized scale was requested: " + scale);
            }
        }
    }
}
