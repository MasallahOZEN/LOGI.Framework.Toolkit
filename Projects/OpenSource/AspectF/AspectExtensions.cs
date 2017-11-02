using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Transactions;
using Slf;
using LOGI.Framework.Toolkit.Foundation.Caching;

namespace LOGI.Framework.Toolkit.OpenSource.AspectF
{
    public static class AspectExtensions
    {
        [DebuggerStepThrough]
        public static void DoNothing()
        {
        }

        [DebuggerStepThrough]
        public static void DoNothing(params object[] whatever)
        {
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Retry(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspects, ILogger logger)
        {
            return aspects.Combine(work =>
                                   Retry(1000, 1, error => DoNothing(error), x => DoNothing(), work, logger));
        }


        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Retry(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspects, int retryDuration=500)
        {
            return Retry(aspects,1,retryDuration);
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Retry(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspects, Action<IEnumerable<Exception>> failHandler, ILogger logger)
        {
            return aspects.Combine(work =>
                                   Retry(1000, 1, error => DoNothing(error), x => DoNothing(), work, logger));
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Retry(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspects, Action<IEnumerable<Exception>> failHandler)
        {
            return Retry(aspects, failHandler, Let.Logger());
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Retry(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspects, int retryDuration, ILogger logger)
        {
            return aspects.Combine(work =>
                                   Retry(retryDuration, 1, error => DoNothing(error), x => DoNothing(), work, logger));
        }
        
        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Retry(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspects, int retryCount,int retryDuration=500 )
        {
            return aspects.Combine(work =>
                                   Retry(retryDuration, retryCount, x => DoNothing(), x => DoNothing(), work, Let.Logger()));
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Retry(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspects, int retryDuration,
                                    Action<Exception> errorHandler, ILogger logger)
        {
            return aspects.Combine(work =>
                                   Retry(retryDuration, 1, errorHandler, x => DoNothing(), work, logger));
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Retry(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspects, int retryDuration,
                                    Action<Exception> errorHandler)
        {
            return Retry(aspects, retryDuration, errorHandler, Let.Logger());
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Retry(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspects, int retryDuration,
                                    int retryCount, Action<Exception> errorHandler, ILogger logger)
        {
            return aspects.Combine(work =>
                                   Retry(retryDuration, retryCount, errorHandler, x => DoNothing(), work, logger));
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Retry(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspects, int retryDuration,
                                    int retryCount, Action<Exception> errorHandler)
        {
            return Retry(aspects, retryDuration, retryCount, errorHandler, Let.Logger());
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Retry(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspects, int retryDuration,
                                    int retryCount, Action<Exception> errorHandler, Action<IEnumerable<Exception>> retryFailed, ILogger logger)
        {
            return aspects.Combine(work =>
                                   Retry(retryDuration, retryCount, errorHandler, retryFailed, work, logger));
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Retry(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspects, int retryDuration,
                                    int retryCount, Action<Exception> errorHandler, Action<IEnumerable<Exception>> retryFailed)
        {
            return Retry(aspects, retryDuration, retryCount, errorHandler, retryFailed, Let.Logger());
        }

        [DebuggerStepThrough]
        public static void Retry(int retryDuration, int retryCount,
                                 Action<Exception> errorHandler, Action<IEnumerable<Exception>> retryFailed, Action work, ILogger logger)
        {
            List<Exception> errors = null;
            do
            {
                try
                {
                    work();
                    return;
                }
                catch (Exception x)
                {
                    if (null == errors)
                        errors = new List<Exception>();
                    errors.Add(x);
                    if (logger!=null)
                    {
                        logger.Error(x);
                    }
                    errorHandler(x);
                    System.Threading.Thread.Sleep(retryDuration);
                }
            } while (retryCount-- > 0);
            retryFailed(errors);
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Delay(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, int milliseconds)
        {
            return aspect.Combine(work =>
                                      {
                                          System.Threading.Thread.Sleep(milliseconds);
                                          work();
                                      });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF MustBeNonDefault<T>(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, params T[] args)
            where T : IComparable
        {
            return aspect.Combine(work =>
            {
                for (var i = 0; i < args.Length; i++)
                {
                    T arg = args[i];
                    if (arg == null || arg.Equals(default(T)))
                        throw new ArgumentException(
                            string.Format("Parameter at index {0} is null", i));
                }

                work();
            });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF MustBeNonNull(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, params object[] args)
        {
            return aspect.Combine(work =>
                                      {
                                          for (var i = 0; i < args.Length; i++)
                                          {
                                              var arg = args[i];
                                              if (arg == null)
                                                  throw new ArgumentException(
                                                      string.Format("Parameter at index {0} is null", i));
                                          }

                                          work();
                                      });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF MustBeNonNull(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect,Exception thrownExp, params object[] args)
        {
            return aspect.Combine(work =>
            {
                for (var i = 0; i < args.Length; i++)
                {
                    var arg = args[i];
                    if (arg == null)
                        throw thrownExp;
                }

                work();
            });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Until(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, Func<bool> test)
        {
            return aspect.Combine(work =>
                                      {
                                          while (!test())
                                          {
                                          }

                                          work();
                                      });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF While(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, Func<bool> test)
        {
            return aspect.Combine(work =>
                                      {
                                          while (test())
                                              work();
                                      });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF WhenTrue(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, params Func<bool>[] conditions)
        {
            return aspect.Combine(work =>
                                      {
                                          if (conditions.Any(condition => !condition()))
                                              return;

                                          work();
                                      });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Log(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, ILogger logger,
                                  string logMessage, params object[] arg)
        {
            return aspect.Combine(work =>
                                      {
                                          logger.Info(string.Format(logMessage, arg));

                                          work();
                                      });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Log(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect,
                                  string logMessage, params object[] arg)
        {
            return Log(aspect, Let.Logger(), logMessage, arg);
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Log(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, ILogger logger, 
                                  string beforeMessage, string afterMessage)
        {
            return aspect.Combine(work =>
                                      {
                                          bool hasError = true;
                                          try
                                          {
                                              logger.Info(beforeMessage);
                                              Let.Us.Do(work);
                                              hasError = false;
                                          }
                                          finally
                                          {
                                              afterMessage = hasError
                                                                 ? afterMessage + "(Error Occured !!!)"
                                                                 : afterMessage;
                                              logger.Info(afterMessage);
                                          }
                                          
                                      });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Log(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect,
                                  string beforeMessage, string afterMessage)
        {
            return Log(aspect, Let.Logger(), beforeMessage, afterMessage);
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Log(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, ILogger logger,
                                  Action beforeAction, Action afterAction)
        {
            return aspect.Combine(work =>
            {
                try
                {
                    Let.Us.TrapLog().Do(beforeAction);
                    Let.Us.TrapLogThrow().Do(work); 
                }
                finally
                {
                    Let.Us.TrapLog().Do(afterAction);
                }
                
            });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Log(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect,
                                  Action beforeAction, Action afterAction)
        {
            return Log(aspect, Let.Logger(), beforeAction, afterAction);
        }

        /// <summary>
        /// Bir methodun max çalışacağı süreyi belirtir.
        /// </summary>
        /// <param name="aspect"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF CallWithTimeout(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect,TimeSpan timeout)
        {
            return aspect.Combine(work =>
            {
                Thread threadToKill = null;
                Action wrappedAction = () =>
                {
                    threadToKill = Thread.CurrentThread;
                    work();
                };
                Stopwatch watch=new Stopwatch();
                watch.Start();
                IAsyncResult result = wrappedAction.BeginInvoke(null, null);
                if (result.AsyncWaitHandle.WaitOne(timeout))
                {
                    wrappedAction.EndInvoke(result);
                    watch.Stop();
                }
                else
                {
                    threadToKill.Abort();
                    watch.Stop();
                    throw new TimeoutException(string.Format("Elapsed Time (sec):{0}",watch.Elapsed.TotalSeconds));
                }


                //var thread = new Thread(() => work());  
                //thread.Start();
                //var Completed = thread.Join(timeout);  
                //if (!Completed)
                //{
                //    thread.Abort();
                //    throw new TimeoutException();
                //}  
                
            });
        }

        /// <summary>
        /// HowLong
        /// </summary>
        /// <param name="aspect"></param>
        /// <param name="onSuccessHandler">Başarılı olduğunda çalıştırılacak iş bloğu</param>
        /// <param name="onFailHandler">Başarısız olduğunda çalıştırılacak iş bloğu</param>
        /// <param name="onErrorhandler">Başarılı olduğunda catch içerisinde yapılacak iş (rethrow gibi)</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF HowLong(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, Action<TimeSpan> onSuccessHandler, Action<TimeSpan,Exception> onFailHandler, Action<Exception> onErrorhandler=null)
        {
            
            var retValue= aspect.Combine(work =>
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                var errorOccured = false;
                Exception exc = null;
                try
                {
                    work();
                }
                catch(Exception exception)
                {
                    exc = exception;
                    errorOccured = true;

                    onErrorhandler?.Invoke(exception);

                }
                finally
                {
                    watch.Stop();
                    TimeSpan duration = watch.Elapsed;

                    if (errorOccured)
                    {
                        onFailHandler(duration, exc);
                    }
                    else
                    {
                        onSuccessHandler(duration);
                    }

                    
                }
            });
            
            return retValue;
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF HowLong(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, ILogger logger,
                                      string startMessage, string endMessage)
        {
            return aspect.Combine(work =>
                                      {
                                          Stopwatch watch=new Stopwatch();
                                          watch.Start();
                                          logger.Info(startMessage);

                                          try
                                          {
                                              work();
                                          }
                                          finally
                                          {
                                              watch.Stop();
                                              TimeSpan duration = watch.Elapsed;

                                              logger.Info(string.Format(endMessage, duration.TotalMilliseconds,
                                                                       duration.TotalSeconds, duration.TotalMinutes, duration.TotalHours,
                                                                       duration.TotalDays));   
                                          }
                                      });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF HowLong(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, ILogger logger,
                                      Action invokeThisAfterLogedStartMessage, string startMessage, string endMessage)
        {
            return aspect.Combine(work =>
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                logger.Info(startMessage);

                aspect.TrapLog().Do(invokeThisAfterLogedStartMessage);

                try
                {
                    work();
                }
                finally
                {
                    watch.Stop();
                    TimeSpan duration = watch.Elapsed;

                    logger.Info(string.Format(endMessage, duration.TotalMilliseconds,
                                             duration.TotalSeconds, duration.TotalMinutes, duration.TotalHours,
                                             duration.TotalDays));
                }
            });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF HowLong(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect,
                                      string startMessage, string endMessage)
        {
            return HowLong(aspect, Let.Logger(), startMessage, endMessage);
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF HowLong(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect,Action invokeThisAfterLogedStartMessage,
                                      string startMessage, string endMessage)
        {
            return HowLong(aspect, Let.Logger(), invokeThisAfterLogedStartMessage, startMessage, endMessage);
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF TrapLog(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, ILogger logger)
        {
            return aspect.Combine(work =>
                                      {
                                          try
                                          {
                                              work();
                                          }
                                          catch (Exception x)
                                          {
                                              logger.Error(x);
                                          }
                                      });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF TrapLog(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect)
        {
            return TrapLog(aspect, Let.Logger());
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF TrapLog(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, ILogger logger, Action<Exception> errorhandler)
        {
            return aspect.Combine(work =>
            {
                try
                {
                    work();
                }
                catch (Exception x)
                {
                    Let.Us.TrapLog().Do(() => errorhandler.Invoke(x));
                }
            });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF TrapLog(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, Action<Exception> errorhandler)
        {
            return TrapLog(aspect, Let.Logger(),errorhandler);
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF TrapLogThrow(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, ILogger logger)
        {
            return aspect.Combine(work =>
                                      {
                                          try
                                          {
                                              work();
                                          }
                                          catch (Exception x)
                                          {
                                              logger.Error(x);
                                              throw;
                                          }
                                      });
        }
        
        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF TrapLogThrow(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, Action<Exception> errorhandler)
        {
            return aspect.Combine(work =>
                                      {
                                          try
                                          {
                                              work();
                                          }
                                          catch (Exception x)
                                          {
                                              errorhandler.Invoke(x);
                                              throw;
                                          }
                                      });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF TrapLogThrow(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect)
        {
            return TrapLogThrow(aspect, Let.Logger());
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF RunAsync(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, Action completeCallback)
        {
            return aspect.Combine(work => work.BeginInvoke(asyncresult =>
                                                               {
                                                                   work.EndInvoke(asyncresult); completeCallback();
                                                               }, null));
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF RunAsync(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect)
        {
            return aspect.Combine(work => work.BeginInvoke(work.EndInvoke, null));
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Cache<TReturnType>(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect,
                                                 ICacheWrapper cacheResolver, string key)
        {
            return aspect.Combine(work => Cache<TReturnType>(aspect, cacheResolver, key, work, cached => cached));
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Cache<TReturnType>(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect,
                                                 string key)
        {
            return Cache<TReturnType>(aspect, Let.Cache(), key);
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF CacheList<TItemType, TListType>(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect,
                                                              ICacheWrapper cacheResolver, string listCacheKey, Func<TItemType, string> getItemKey)
            where TListType : IList<TItemType>, new()
        {
            return aspect.Combine(work =>
                                      {
                                          var workDelegate = aspect.WorkDelegate as Func<TListType>;

                                          // Replace the actual work delegate with a new delegate so that
                                          // when the actual work delegate returns a collection, each item
                                          // in the collection is stored in cache individually.
                                          Func<TListType> newWorkDelegate = () =>
                                                                                {
                                                                                    if (workDelegate != null)
                                                                                    {
                                                                                        TListType collection = workDelegate();
                                                                                        foreach (TItemType item in collection)
                                                                                        {
                                                                                            string key = getItemKey(item);
                                                                                            cacheResolver.Save(key, item);
                                                                                        }
                                                                                        return collection;
                                                                                    }
                                                                                    return default(TListType);
                                                                                };
                                          aspect.WorkDelegate = newWorkDelegate;

                                          // Get the collection from cache or real source. If collection is returned
                                          // from cache, resolve each item in the collection from cache
                                          Cache<TListType>(aspect, cacheResolver, listCacheKey, work,
                                                           cached =>
                                                               {
                                                                   // Get each item from cache. If any of the item is not in cache
                                                                   // then discard the whole collection from cache and reload the 
                                                                   // collection from source.
                                                                   var itemList = new TListType();
                                                                   foreach (TItemType cachedItem in cached.Select(item => cacheResolver.Get<TItemType>(getItemKey(item))))
                                                                   {
                                                                       if (null != cachedItem)
                                                                       {
                                                                           itemList.Add(cachedItem);
                                                                       }
                                                                       else
                                                                       {
                                                                           // One of the item is missing from cache. So, discard the 
                                                                           // cached list.
                                                                           return default(TListType);
                                                                       }
                                                                   }

                                                                   return itemList;
                                                               });
                                      });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF CacheList<TItemType, TListType>(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect,
                                                              string listCacheKey, Func<TItemType, string> getItemKey)
            where TListType : IList<TItemType>, new()
        {
            return CacheList<TItemType, TListType>(aspect, Let.Cache(), listCacheKey, getItemKey);
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF CacheRetry<TReturnType>(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect,
                                                      ICacheWrapper cacheResolver,
                                                      ILogger logger,
                                                      string key)
        {
            return aspect.Combine(work =>
                                      {
                                          try
                                          {
                                              Cache<TReturnType>(aspect, cacheResolver, key, work, cached => cached);
                                          }
                                          catch (Exception x)
                                          {
                                              logger.Error(x);
                                              System.Threading.Thread.Sleep(1000);

                                              //Retry
                                              try
                                              {
                                                  Cache<TReturnType>(aspect, cacheResolver, key, work, cached => cached);
                                              }
                                              catch (Exception ex)
                                              {
                                                  logger.Error(ex);
                                                  throw;
                                              }
                                          }
                                      });
        }

        [DebuggerStepThrough]
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF CacheRetry<TReturnType>(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect,
                                                      string key)
        {
            return CacheRetry<TReturnType>(aspect, Let.Cache(), Let.Logger(), key);
        }

        private static void Cache<TReturnType>(LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, ICacheWrapper cacheResolver,
                                               string key, Action work, Func<TReturnType, TReturnType> foundInCache)
        {
            TReturnType cachedData = cacheResolver.Get<TReturnType>(key);
            if (cachedData == null)
            {
                GetListFromSource<TReturnType>(aspect, cacheResolver, key);
            }
            else
            {
                // Give caller a chance to shape the cached item before it is returned
                TReturnType cachedType = foundInCache(cachedData);
                if (cachedType == null)
                {
                    GetListFromSource<TReturnType>(aspect, cacheResolver, key);
                }
                else
                {
                    aspect.WorkDelegate = new Func<TReturnType>(() => cachedType);
                }
            }

            work();
        }

        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Expected<TException>(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect)
            where TException : Exception
        {
            return aspect.Combine(work =>
                                      {
                                          try
                                          {
                                              work();
                                          }
                                          catch (TException x)
                                          {
                                              Debug.WriteLine(x.ToString());
                                          }
                                      });
        }

        /// <summary>
        /// Transaction
        /// (The "Distributed Transaction Coordinator" service must be started)
        /// </summary>
        /// <param name="aspect"></param>
        /// <param name="complete"></param>
        /// <returns></returns>
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Transaction(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, bool complete=true)
        {
            return aspect.Combine(work =>
                                      {
                                          using (var scope = new TransactionScope())
                                          {
                                              work();
                                              if (complete)
                                              scope.Complete();
                                          }
                                      });
        }

        ///<summary>
        /// Transaction
        /// (The "Distributed Transaction Coordinator" service must be started)
        ///</summary>
        ///<param name="aspect"></param>
        ///<param name="transactionScopeOption"></param>
        ///<param name="complete"></param>
        ///<returns></returns>
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Transaction(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, TransactionScopeOption transactionScopeOption, bool complete = true)
        {
            return aspect.Combine(work =>
            {
                using (var scope = new TransactionScope(transactionScopeOption))
                {
                    work();
                    if (complete)
                        scope.Complete();
                }
            });
        }

        ///<summary>
        /// Transaction
        /// (The "Distributed Transaction Coordinator" service must be started)
        ///</summary>
        ///<param name="aspect"></param>
        ///<param name="transactionScopeOption"></param>
        ///<param name="transactionOptions"></param>
        ///<param name="complete"></param>
        ///<returns></returns>
        public static LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF Transaction(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, TransactionScopeOption transactionScopeOption, TransactionOptions transactionOptions, bool complete = true)
        {
            return aspect.Combine(work =>
                                      {
                                          using (var scope = new TransactionScope(transactionScopeOption,transactionOptions))
                                          {
                                              work();
                                              if (complete)
                                                  scope.Complete();
                                          }
                                      });
        }

        private static void GetListFromSource<TReturnType>(LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, ICacheWrapper cacheResolver,
                                                           string key)
        {
            var workDelegate = aspect.WorkDelegate as Func<TReturnType>;
            if (workDelegate != null)
            {
                TReturnType realObject = workDelegate();
                cacheResolver.Save(key, realObject);
                workDelegate = () => realObject;
            }
            aspect.WorkDelegate = workDelegate;
        }

        /// <summary>
        /// Returns the instance of old object with new operations applied on.
        /// </summary>
        /// <typeparam name="TReturnType">The type of the object new operations will be applied on.</typeparam>
        /// <param name="aspect"></param>
        /// <param name="item">The object need to be modified.</param>
        /// <param name="action">The delegate which performs on the object supplied.</param>
        /// <returns>Returns the old object with new operations applied on.</returns>
        [DebuggerStepThrough]
        public static TReturnType Use<TReturnType>(this LOGI.Framework.Toolkit.OpenSource.AspectF.AspectF aspect, TReturnType item, Action<TReturnType> action)
        {
            return aspect.Return(() =>
                                     {
                                         action(item);
                                         return item;
                                     });
        }
    }
}