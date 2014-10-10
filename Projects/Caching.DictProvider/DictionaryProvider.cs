using System;
using System.Linq;
using System.Collections.Generic;
using System.Timers;
using LOGI.Framework.Toolkit.Core.Extensions.ExtObject;
using LOGI.Framework.Toolkit.Core.Extensions.ExtDictionary;
using LOGI.Framework.Toolkit.Core.Threading;
using LOGI.Framework.Toolkit.Foundation.Caching;


namespace LOGI.Framework.Toolkit.Core.Caching.Provider.DictProvider
{
    ///<summary>
    /// CacheWrapper
    ///</summary>
    public class DictionaryProvider : ICacheWrapper
    {

        #region Static Variable

        // Private static variable which holds the key value pair
        private object _sharedLock = new object();
        private static readonly Dictionary<string, object> CacheObject = new Dictionary<string, object>();
        private static readonly Dictionary<string, TimeSpan> ExpTimeForCacheObject = new Dictionary<string, TimeSpan>();

        private static readonly Timer TimeTracker = new Timer(1000);

        #endregion

        #region Ctor
        ~DictionaryProvider()
        {
            TimeTracker.Elapsed += new ElapsedEventHandler(TimeTracker_Elapsed);
        }

        public DictionaryProvider()
        {
            TimeTracker.Elapsed += new ElapsedEventHandler(TimeTracker_Elapsed);
            TimeTracker.Start();
        }
        #endregion

        void TimeTracker_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (_sharedLock)
            {
                for (int counter = 0; counter < ExpTimeForCacheObject.Count; counter++)
                {
                    var item = ExpTimeForCacheObject.ElementAt(counter);
                    if (DateTime.Now.TimeOfDay > item.Value)
                    {
                        if (CacheObject.Remove(item.Key))
                        {
                            ExpTimeForCacheObject.Remove(item.Key);
                        }

                    }
                }
            }
        }

        #region Public Methods

        /// <summary>
        /// <remarks>This function gets the object for the given key</remarks>
        /// <param name="key"></param>
        /// <returns>Boolean</returns>
        /// </summary>
        public T Get<T>(string key)
        {
            using (TimedLock.Lock(_sharedLock, new TimeSpan(0, 0, 10,0))) //TODO: timeout süresini config'e al
            {
                object returnObject;

                try
                {
                    CacheObject.TryGetValue(key.Trim(), out returnObject);
                }
                catch (ArgumentNullException argumentNullException)
                {
                    throw argumentNullException;
                }
                catch (KeyNotFoundException keyNotFoundException)
                {
                    throw keyNotFoundException;
                }
                if (returnObject != null)
                {
                    return returnObject.To<T>(returnDefaultValue: true);
                }
                else
                {
                    return default(T);
                }
            }

        }

        /// <summary>
        /// This function gets the object for the given key
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="transform"></param>
        /// <param name="SomethingElse"></param>
        /// <returns></returns>
        public T ValueOrSomethingElse<V, T>(string Key, Func<object, T> transform, Func<T> SomethingElse)
        {
            return CacheObject.ValueOrSomethingElse(Key, transform, SomethingElse);
        }

        /// <summary>
        /// <remarks>This function checks for the given key</remarks>
        /// <param name="key"></param>
        /// <returns>Boolean</returns>
        /// </summary>
        public bool Exist(string key)
        {
            using (TimedLock.Lock(_sharedLock, new TimeSpan(0, 0, 10,0))) //TODO: timeout süresini config'e al
            {
                return CacheObject.ContainsKey(key.Trim());
            }
        }

        public IEnumerable<T> Find<T>(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        ///<summary>
        /// Find
        ///</summary>
        ///<param name="predicate"></param>
        ///<typeparam name="T"></typeparam>
        ///<returns></returns>
        public IEnumerable<KeyValuePair<string, T>> Find<T>(Func<T, bool> predicate, Func<KeyValuePair<string, T>, bool> SomethingElse)
        {
            using (TimedLock.Lock(_sharedLock, new TimeSpan(0, 0, 10,0))) //TODO: timeout süresini config'e al
            {
                return ((IEnumerable<KeyValuePair<string, T>>)CacheObject.AsQueryable()).Where(SomethingElse);
            }
        }

        /// <summary>
        /// <remarks>This function addeds the given object in the Dictionary object</remarks>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>Boolean</returns>
        /// </summary>
        public bool Save<T>(string key, T data, TimeSpan? timeout = null, bool overriteIfExist = false)
        {
            using (TimedLock.Lock(_sharedLock, new TimeSpan(0, 0, 10,0))) //TODO: timeout süresini config'e al
            {
                bool bResult = false;

                lock (CacheObject)
                {
                    try
                    {
                        if (!CacheObject.ContainsKey(key))
                        {
                            CacheObject.Add(key.Trim(), data);
                            if (timeout.HasValue)
                            {
                                ExpTimeForCacheObject.Add(key.Trim(), DateTime.Now.Add(timeout.Value).TimeOfDay);
                            }
                            bResult = true;
                        }
                    }
                    catch (ArgumentNullException argumentNullException)
                    {
                        throw argumentNullException;
                    }
                    catch (ArgumentException argumentException)
                    {
                        throw argumentException;
                    }
                }
                return bResult;
            }
        }

        /// <summary>
        /// <remarks>This function remove the given key from Dictionary</remarks>
        /// <param name="key"></param>
        /// <returns>Boolean</returns>
        /// </summary>
        public bool Remove(string key)
        {
            using (TimedLock.Lock(_sharedLock, new TimeSpan(0, 0, 10,0))) //TODO: timeout süresini config'e al
            {
                bool result;

                lock (CacheObject)
                    if (CacheObject.ContainsKey(key.Trim()))
                    {
                        CacheObject.Remove(key.Trim());

                        result = true;
                    }

                    else
                    {
                        result = false;
                    }

                return result;
            }
        }

        public bool Remove<T>(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears all avaialble keys and values in the cache
        /// </summary>
        public bool Clear()
        {
            using (TimedLock.Lock(_sharedLock, new TimeSpan(0, 0, 10,0))) //TODO: timeout süresini config'e al
            {
                CacheObject.Clear();
                return true;
            }
        }

        #endregion
    }
}
