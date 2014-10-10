using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LOGI.Framework.Toolkit.Core.Threading
{
    public class ThreadSafeHelper<TKey>
    {
        ConcurrentDictionary<TKey, object> _locks = new ConcurrentDictionary<TKey, object>();

        public ThreadSafeHelper()
        {
        }

        public object this[TKey key]
        {
            get
            {
                return _locks.GetOrAdd(key, k => new Object());
            }
        }

        public void Reset()
        {
            _locks.Clear();
        }

        public static void Execute(object lockObject, Action closure)
        {
            lock (lockObject)
                closure();
        }


        public static bool Execute(object lockObject, Action closure, int timeout, bool throwsOnTimeout)
        {
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(lockObject, timeout, ref lockTaken);
                if (!lockTaken)
                {
                    if (throwsOnTimeout)
                        throw new TimeoutException("Could not gain a lock within the timeout specified.");

                    return false;
                }

                closure();
            }
            finally
            {
                if (lockTaken) Monitor.Exit(lockObject);
            }

            return true;
        }


        public void Execute(TKey key, Action closure)
        {
            Execute(this[key], closure);
        }

        public void Execute(TKey key, Action closure, int timeout, bool throwsOnTimeout)
        {
            Execute(this[key], closure, timeout, throwsOnTimeout);
        }

        public static void Execute(object lockObject, Func<bool> condition, Action closure)
        {
            if (condition())
                lock (lockObject)
                    if (condition())
                        closure();
        }

        public static bool Execute(object lockObject, Func<bool> condition, Action closure, int timeout, bool throwsOnTimeout)
        {
            if (condition())
            {
                bool lockTaken = false;
                try
                {

                    Monitor.TryEnter(lockObject, timeout, ref lockTaken);
                    if (!lockTaken)
                    {
                        if (throwsOnTimeout)
                            throw new TimeoutException("Could not gain a lock within the timeout specified.");

                        return false;
                    }

                    if (condition())
                        closure();

                }
                finally
                {
                    if (lockTaken) Monitor.Exit(lockObject);
                }
            }

            return true;
        }

        public void Execute(TKey key, Func<bool> condition, Action closure)
        {
            Execute(this[key], condition, closure);
        }

        public void Execute(TKey key, Func<bool> condition, Action closure, int timeout, bool throwsOnTimeout)
        {
            Execute(this[key], condition, closure, timeout, throwsOnTimeout);
        }

        public static ushort LOCKCLEANUPDELAY = 100;

        public static bool AllowDelayLockCleanup
        {
            get { return LOCKCLEANUPDELAY != 0; }
        }

        static readonly Dictionary<string, ReaderWriterLockSlim> FileAccessLocks = new Dictionary<string, ReaderWriterLockSlim>();
        static ReaderWriterLockSlim RegisterAccessLockThread(string path)
        {
            lock (FileAccessLocks)
            {
                ReaderWriterLockSlim result;
                if (FileAccessLocks.ContainsKey(path))
                    result = FileAccessLocks[path];
                else
                {
                    FileAccessLocks.Add(path, result = new ReaderWriterLockSlim());
                    //System.Diagnostics.Debug.Print("FileAccessLock Count: +" + FileAccessLocks.Count);
                }

                return result;
            }
        }

        // No new locks without exclusive access...
        //static readonly ReaderWriterLockSlim permFileAccessLock = new ReaderWriterLockSlim();
        static ReaderWriterLockSlim GetLock(string path, bool write)
        {
            // Need to be able to enter a lock before releasing acesss in order to prevent removal... 
            lock (FileAccessLocks)
            {
                ReaderWriterLockSlim result = RegisterAccessLockThread(path);
                if (AllowDelayLockCleanup)
                    ClearLockCleanup();
                try
                {
                    if (write)
                        result.EnterWriteLock();
                    else
                        result.EnterReadLock();
                }
                finally
                {
                    //result = null; // This is all quite unnecessary but a good pattern to follow...
                }

                return result;
            }
        }
        static ReaderWriterLockSlim GetReadLock(string path)
        {
            return GetLock(path, false);
        }

        static ReaderWriterLockSlim GetWriteLock(string path)
        {
            return GetLock(path, true);
        }

        static object LockCleanupTimeStampLock = new Object();
        static DateTime LockCleanupTimeStamp = DateTime.MaxValue;
        static void ClearLockCleanup()
        {
            lock (LockCleanupTimeStampLock)
                LockCleanupTimeStamp = DateTime.MaxValue;
        }

        static Task LockCleanupTask;
        static void SetLockCleanup()
        {
            lock (LockCleanupTimeStampLock)
            {
                LockCleanupTimeStamp = DateTime.Now;

                LazyInitializer.EnsureInitialized(ref LockCleanupTask, () =>
                {
                    Task t = new Task(LockCleanupCheck);
                    t.Start();
                    return t;
                });
            }
        }

        static void LockCleanupCheck()
        {
            while (DateTime.Now.AddMilliseconds(-LOCKCLEANUPDELAY) < LockCleanupTimeStamp)
                Thread.Sleep(LOCKCLEANUPDELAY / 4);

            LockCleanup();
            lock (LockCleanupTimeStampLock)
                LockCleanupTask = null;
        }

        static void LockCleanup()
        {
            lock (FileAccessLocks)
            {
                if (AllowDelayLockCleanup)
                    Debug.Print("FileAccessLock Count Before Cleanup: " + FileAccessLocks.Count);
                // Search for dormant file locks.
                foreach (string key in FileAccessLocks.Keys.ToArray())
                {
                    ReaderWriterLockSlim tempLock = FileAccessLocks[key];
                    if (IsLockFree(tempLock))
                    {
                        FileAccessLocks.Remove(key);
                        tempLock.Dispose();
                    }
                }
                if (AllowDelayLockCleanup)
                    Debug.Print("FileAccessLock Count After Cleanup: " + FileAccessLocks.Count);
            }
        }

        static bool IsLockFree(ReaderWriterLockSlim tempLock)
        {
            return tempLock.CurrentReadCount == 0
                        && tempLock.RecursiveReadCount == 0
                        && tempLock.RecursiveUpgradeCount == 0
                        && tempLock.RecursiveWriteCount == 0
                        && tempLock.WaitingReadCount == 0
                        && tempLock.WaitingWriteCount == 0
                        && tempLock.WaitingUpgradeCount == 0;
        }

        static void ReleaseLock(ReaderWriterLockSlim tempLock, bool write)
        {
            if (tempLock != null)
            {
                if (write)
                    tempLock.ExitWriteLock();
                else
                    tempLock.ExitReadLock();

                if (AllowDelayLockCleanup)
                    SetLockCleanup();
                else
                    LockCleanup();
            }
        }
        static void ReleaseReadLock(ReaderWriterLockSlim tempLock)
        {
            ReleaseLock(tempLock, false);
        }

        static void ReleaseWriteLock(ReaderWriterLockSlim tempLock)
        {
            ReleaseLock(tempLock, true);
        }

        public static void ExecuteWrite(ReaderWriterLockSlim lockObject, Action closure)
        {
            try
            {

                if (lockObject != null)
                    lockObject.EnterWriteLock();

                closure();
            }
            finally
            {
                ReleaseWriteLock(lockObject);
            }
        }
        public static void ExecuteWrite(string path, Action closure)
        {
            ReaderWriterLockSlim lockObject = null;
            try
            {
                lockObject = GetWriteLock(path); // THIS ACQUIRES THE LOCK!!!
                closure();
            }
            finally
            {
                ReleaseWriteLock(lockObject);
            }
        }

        public static void ExecuteRead(ReaderWriterLockSlim lockObject, Action closure)
        {
            try
            {
                if (lockObject != null)
                    lockObject.EnterReadLock();

                closure();
            }
            finally
            {
                ReleaseReadLock(lockObject);
            }
        }
        public static void ExecuteRead(string path, Action closure)
        {
            ReaderWriterLockSlim lockObject = null;
            try
            {
                lockObject = GetReadLock(path);
                closure();
            }
            finally
            {
                ReleaseReadLock(lockObject);
            }
        }

        public static T ExecuteRead<T>(string path, Func<T> closure)
        {
            ReaderWriterLockSlim lockObject = null;
            T result;
            try
            {
                lockObject = GetReadLock(path);
                result = closure();
            }
            finally
            {
                ReleaseReadLock(lockObject);
            }
            return result;
        }

    }

    public class ThreadSafeHelper : ThreadSafeHelper<string>
    {
    }
}
