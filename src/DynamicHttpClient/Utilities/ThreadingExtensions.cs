using System;
using System.Threading;

namespace DynamicHttpClient.Utilities
{
  internal static class ThreadingExtensions
  {
    public static ScopedReadLockToken ScopedReadLock(this ReaderWriterLockSlim target)
    {
      Check.NotNull(target, nameof(target));

      target.EnterReadLock();

      return new ScopedReadLockToken(target);
    }

    public static ScopedUpgradeableReadLockToken ScopedUpgradeableReadLock(this ReaderWriterLockSlim target)
    {
      Check.NotNull(target, nameof(target));

      target.EnterUpgradeableReadLock();

      return new ScopedUpgradeableReadLockToken(target);
    }

    public static ScopedWriteLockToken ScopedWriteLock(this ReaderWriterLockSlim target)
    {
      Check.NotNull(target, nameof(target));

      target.EnterWriteLock();

      return new ScopedWriteLockToken(target);
    }

    public struct ScopedReadLockToken : IDisposable
    {
      private readonly ReaderWriterLockSlim target;

      public ScopedReadLockToken(ReaderWriterLockSlim target) => this.target = target;

      public void Dispose()
      {
        if (target.IsReadLockHeld)
        {
          target.ExitReadLock();
        }
      }
    }

    public struct ScopedUpgradeableReadLockToken : IDisposable
    {
      private readonly ReaderWriterLockSlim target;

      public ScopedUpgradeableReadLockToken(ReaderWriterLockSlim target) => this.target = target;

      public void Dispose()
      {
        if (target.IsUpgradeableReadLockHeld)
        {
          target.ExitUpgradeableReadLock();
        }
      }
    }

    public struct ScopedWriteLockToken : IDisposable
    {
      private readonly ReaderWriterLockSlim target;

      public ScopedWriteLockToken(ReaderWriterLockSlim target) => this.target = target;

      public void Dispose()
      {
        if (target.IsWriteLockHeld)
        {
          target.ExitWriteLock();
        }
      }
    }
  }
}