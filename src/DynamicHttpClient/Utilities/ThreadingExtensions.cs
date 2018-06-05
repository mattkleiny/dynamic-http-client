using System;
using System.Threading;

namespace DynamicHttpClient.Utilities
{
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