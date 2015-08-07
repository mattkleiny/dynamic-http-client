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

namespace DynamicRestClient.Utilities
{
    using System;
    using System.Threading;

    /// <summary>
    /// Extension methods related to <see cref="ReaderWriterLockSlim"/>.
    /// </summary>
    internal static class ThreadingExtensions
    {
        /// <summary>
        /// Creates a <see cref="IDisposable"/> scoped read-lock from the <see cref="ReaderWriterLockSlim"/>.
        /// </summary>
        public static IDisposable ScopedReadLock(this ReaderWriterLockSlim @lock)
        {
            Check.NotNull(@lock, nameof(@lock));

            @lock.EnterReadLock();

            return new AnonymousDisposable(@lock.ExitReadLock);
        }

        /// <summary>
        /// Creates a <see cref="IDisposable"/> scoped upgradeable read-lock from the <see cref="ReaderWriterLockSlim"/>.
        /// </summary>
        public static IDisposable ScopedUpgradeableReadLock(this ReaderWriterLockSlim @lock)
        {
            Check.NotNull(@lock, nameof(@lock));

            @lock.EnterUpgradeableReadLock();

            return new AnonymousDisposable(@lock.ExitUpgradeableReadLock);
        }

        /// <summary>
        /// Creates a <see cref="IDisposable"/> scoped write-lock from the <see cref="ReaderWriterLockSlim"/>.
        /// </summary>
        public static IDisposable ScopedWriteLock(this ReaderWriterLockSlim @lock)
        {
            Check.NotNull(@lock, nameof(@lock));

            @lock.EnterWriteLock();

            return new AnonymousDisposable(@lock.ExitWriteLock);
        }

        /// <summary>
        /// An anonymous, delegate-based <see cref="IDisposable"/> implementation.
        /// </summary>
        private sealed class AnonymousDisposable : IDisposable
        {
            private readonly Action disposeDelegate;

            public AnonymousDisposable(Action disposeDelegate)
            {
                Check.NotNull(disposeDelegate, nameof(disposeDelegate));

                this.disposeDelegate = disposeDelegate;
            }

            public void Dispose()
            {
                this.disposeDelegate();
            }
        }
    }
}
