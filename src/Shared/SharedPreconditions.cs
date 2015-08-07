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
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using JetBrains.Annotations;

    /// <summary>
    /// Static utilities for precondition checking.
    /// </summary>
    internal static class Check
    {
        /// <summary>
        /// Asserts that the given <see cref="reference"/> is not null, and generates an exception if it is.
        /// </summary>
        /// <exception cref="ArgumentNullException">If <see cref="ArgumentNullException"/> is null.</exception>
        [Conditional("DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull<T>([NoEnumeration] T reference, string name)
            where T : class
        {
            if (reference == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Asserts that the given <see cref="value"/> is not a default value, and generates an exception if it is.
        /// </summary>
        [Conditional("DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotDefault<T>(T value, string name)
            where T : struct
        {
            if (default(T).Equals(value))
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Asserts that the given string <see cref="value"/> is not <see cref="string.IsNullOrEmpty"/>, and generates an exception if it is.
        /// </summary>
        /// <exception cref="ArgumentNullException">If <see cref="value"/> is null or empty.</exception>
        [Conditional("DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrEmpty(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Asserts that the given <see cref="condition"/> is true, and generates an exception if it is not.
        /// </summary>
        /// <exception cref="ArgumentException">If <see cref="condition"/> is false.</exception>
        [Conditional("DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void That(bool condition, string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message);
            }
        }
    }
}
