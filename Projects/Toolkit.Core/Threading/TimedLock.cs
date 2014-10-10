using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGI.Framework.Toolkit.Core.Common.Configurations;
using LOGI.Framework.Toolkit.Core.Configurations.Base;
using LOGI.Framework.Toolkit.Core.Exceptions;

namespace LOGI.Framework.Toolkit.Core.Threading
{
    using System;
    using System.Threading;

    /// <summary>
    /// A resource locking class which contains built in deadlock handling.
    /// Use in preference to using the lock statemet ("lock( myObject)") as the
    /// TimedLock object has built in deadlock handling.
    /// </summary>
    /// <example>Intented use:
    /// // Place lock on resource
    /// using( TimedLock.Lock( myObject ))
    /// {
    ///		// .. do stuff 
    ///	} 
    /// </example>
    public struct TimedLock : IDisposable
    {
        #region Private Variables

        /// <summary>
        /// The object being locked.
        /// </summary>
        private object _lockObject;
        private static TimeSpan _defaultTimeOut = TimeSpan.FromSeconds(20);

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Private constructor to create a new TimeLock.
        /// </summary>
        /// <param name="lockObject">The object to lock.</param>
        private TimedLock(object lockObject)
        {
            _lockObject = lockObject;
#if DEBUG
            _undisposedLockDetector = new UndisposedLockDetector(lockObject);
#endif
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the default time to wait for the lock on the specified object.
        /// </summary>
        public static TimeSpan DefaultTimeOut
        {
            get { return _defaultTimeOut; }
            set { _defaultTimeOut = value; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Applies a lock to the specified resource (using the default timeout <c>DefaultTimeOut</c>)..
        /// </summary>
        /// <param name="lockObject">The resource to lock.</param>
        /// <returns>Returns the lock.</returns>
        /// <exception cref="TimedLockException">Thrown when the DefaultTimeOut is exceeded.</exception>
        /// <example>Intented use:
        /// // Place lock on resource
        /// using( TimedLock.Lock ( myObject ))
        /// {
        ///		// .. do stuff 
        ///	} 
        /// </example>
        public static IDisposable Lock(object lockObject)
        {
            return Lock(lockObject, DefaultTimeOut, null);
        }

        /// <summary>
        /// Applies a lock to the specified resource (using the default timeout <c>DefaultTimeOut</c>)..
        /// </summary>
        /// <param name="lockObject">The resource to lock.</param>
        /// <param name="timeoutErrorMessage">Information to add to the error message if the lock times out (nullable).</param>
        /// <returns>Returns the lock (must be Disposed of).</returns>
        /// <exception cref="TimedLockException">Thrown when the DefaultTimeOut is exceeded.</exception>
        /// <example>Intented use:
        /// // Place lock on resource
        /// using( TimedLock.Lock ( myObject ))
        /// {
        ///		// .. do stuff 
        ///	} 
        /// </example>
        public static IDisposable Lock(object lockObject, string timeoutErrorMessage)
        {
            return Lock(lockObject, DefaultTimeOut, timeoutErrorMessage);
        }

        /// <summary>
        /// Applies a lock to the specified resource (using the specified timeout)..
        /// </summary>
        /// <param name="lockObject">The resource to lock.</param>
        /// <param name="timeout">The deadlock timeout</param>
        /// <returns>Returns the lock.</returns>
        /// <exception cref="TimedLockException">Thrown when the <c>DefaultTimeOut</c> is exceeded.</exception>
        /// <example>Intented use:
        /// using( TimedLock.Lock ( myObject ))
        /// {
        ///		// .. do stuff 
        ///	} 
        /// </example>
        public static IDisposable Lock(object lockObject, TimeSpan timeout)
        {
            return Lock(lockObject, timeout, null);
        }

        /// <summary>
        /// Applies a lock to the specified resource (using the specified timeout)..
        /// </summary>
        /// <param name="lockObject">The resource to lock.</param>
        /// <param name="timeout">The deadlock timeout</param>
        /// <param name="timeoutErrorMessage">Information to add to the error message if the lock times out (nullable).</param>
        /// <returns>Returns the lock.</returns>
        /// <exception cref="TimedLockException">Thrown when the <c>DefaultTimeOut</c> is exceeded.</exception>
        /// <example>Intented use:
        /// using( TimedLock.Lock ( myObject ))
        /// {
        ///		// .. do stuff 
        ///	} 
        /// </example>
        public static IDisposable Lock(object lockObject, TimeSpan timeout, string timeoutErrorMessage)
        {
            // Check parameters
            if (lockObject == null)
            {
                // Throw an exception
                throw new ArgumentNullException("lockObject", "Please specify a lockObject.");
            }

            // Create new lock
            TimedLock timedLock = new TimedLock(lockObject);
            var extraTimeAsSecond = new XmlConfig().Get<int>("TimedLock.ExtraTimeAsSecond", true); 

            if (extraTimeAsSecond>0)
            {
                timeout=timeout.Add(new TimeSpan(0, 0, 0, extraTimeAsSecond));
            }

            // Try a lock on the resource)
            if (!Monitor.TryEnter(lockObject, timeout))
            {
#if DEBUG
                // Failed to get the lock, so prevent the finalizer from being called
                System.GC.SuppressFinalize(timedLock._undisposedLockDetector);
#endif
                // Throw new timeout exception
                string message = "Lock timeout expired!";
                if (timeoutErrorMessage != null)
                {
                    message += Environment.NewLine + timeoutErrorMessage;
                }
                throw new LockTimeoutException(message);
            }
            return timedLock;
        }

        #endregion Public Methods

        #region IDisposable Members

        /// <summary>
        /// Disposes of the timed lock resource (MUST BE CALLED - USE USING STATEMENTS).
        /// </summary>
        public void Dispose()
        {
            Monitor.Exit(_lockObject);
            _lockObject = null;
            // It's a bad error if someone forgets to call Dispose,
            // so in Debug builds, we put a finalizer in to detect
            // the error. If Dispose is called, we suppress the
            // finalizer.
#if DEBUG
            // Prevent finalizer from being called.
            GC.SuppressFinalize(_undisposedLockDetector);
#endif
        }

        #endregion

        #region [Debug] UndisposedLockDetector Class and Variables

#if DEBUG
        /// <summary>
        /// Debug object to detect any undisposed locks.
        /// </summary>
        private UndisposedLockDetector _undisposedLockDetector;

        #region UndisposedLockDetector (Private Class)

        /// Debug object to detect and warn developers of any undisposed locks.
        private class UndisposedLockDetector
        {
            #region Private Variables

            private object _lockObject = null;

            #endregion Private Variables

            #region Constructors

            /// <summary>
            /// Default constructor.
            /// </summary>
            /// <param name="lockObject"></param>
            public UndisposedLockDetector(object lockObject)
            {
                _lockObject = lockObject;
            }

            #endregion Constructors

            #region Finalizer

            /// <summary>
            /// Finalizer. This should not get called if the object has been correctly disposed of.
            /// </summary>
            ~UndisposedLockDetector()
            {
                string message;
                // If this finalizer runs, someone somewhere failed to
                // call Dispose, which means we've failed to leave
                // a monitor!
                if (_lockObject != null)
                {
                    message = "Error. Undisposed TimedLock on object: " + Environment.NewLine + _lockObject.ToString();
                }
                else
                {
                    message = "Error. Undisposed TimedLock on a null object.";
                }
                Console.WriteLine(message);
                System.Diagnostics.Debug.Fail(message);
            }

            #endregion Finalizer
        }

        #endregion UndisposedLockDetector (Private Class)

#endif

        #endregion [Debug] UndisposedLockDetector Class and Variables
    }




}
