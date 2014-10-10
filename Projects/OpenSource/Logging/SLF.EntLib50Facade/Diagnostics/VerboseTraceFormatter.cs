using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Slf.EntLibFacade.Diagnostics;
using SysEnvironment = System.Environment;

namespace Slf.EntLibFacade.Diagnostics
{
    /// <summary>
    /// A custom trace formatter that implements the <see cref="ILogFormatter"/>
    /// interface to provide verbose logging.
    /// </summary>
    /// <remarks>
    /// This class supports the following configurable attributes for controlling
    /// the type of data that is collected when an event is logged:
    /// <list type="bulleted">
    /// <term>includeEnvironment</term>
    /// <term>includeThread</term>
    /// <term>includeAppDomain</term>
    /// <term>includeProcess</term>
    /// <term>includeType</term>
    /// </list>
    /// </remarks>
    [ConfigurationElementType(typeof(CustomFormatterData))]
    public class VerboseTraceFormatter : TextFormatter
    {
        private NameValueCollection _Attributes;
        /// <summary>
        /// Creates a new <see cref="VerboseTraceFormatter"/> instance using the
        /// supplied attribute collection.
        /// </summary>
        /// <param name="attributes"></param>
        public VerboseTraceFormatter(NameValueCollection attributes)
        {
            // assign the collection reference
            _Attributes = attributes;
        }

        #region ILogFormatter Members

        /// <summary>
        /// Formats the supplied <see cref="LogEntry"/> object instance based on the 
        /// configured attribute values.  This method implements the 
        /// <see cref="ILogFormatter.Format"/> interface member.
        /// </summary>
        /// <param name="logEntry"></param>
        /// <returns></returns>
        public override string Format(LogEntry logEntry)
        {
            // check the argument
            if (null == logEntry)
                throw new ArgumentNullException("logEntry");

            // default return value
            string __result = null;

            // reference the extended properties of the instance
            var __originalEntry = logEntry.Clone() as LogEntry;
            
            IDictionary<string, object> __ExtendedProperties = __originalEntry.ExtendedProperties;

            #region Environment Information
            try
            {
                // check to see if environment information should be collected
                if (bool.Parse(_Attributes["includeEnvironment"]))
                {
                    // log the environment properties
                    __ExtendedProperties.Add(LogProperty.EnvCurrentDirectory, SysEnvironment.CurrentDirectory);
                    __ExtendedProperties.Add(LogProperty.EnvOSVersion, SysEnvironment.OSVersion.ToString());
                    __ExtendedProperties.Add(LogProperty.EnvUserDomainName, SysEnvironment.UserDomainName);
                    __ExtendedProperties.Add(LogProperty.EnvUserName, SysEnvironment.UserName);
                    __ExtendedProperties.Add(LogProperty.EnvVersion, SysEnvironment.Version);
                    __ExtendedProperties.Add(LogProperty.EnvWorkingSet, SysEnvironment.WorkingSet);
                }
            }
            catch
            {
                // ignore
            }
            #endregion

            #region Thread Information
            try
            {
                // check to see if thread information should be collected
                if (bool.Parse(_Attributes["includeThread"]))
                {
                    // log the thread properties
                    System.Threading.Thread __Thread = System.Threading.Thread.CurrentThread;
                    __ExtendedProperties.Add(LogProperty.ThreadName, __Thread.Name);
                    __ExtendedProperties.Add(LogProperty.ThreadApartmentState, __Thread.GetApartmentState().ToString());
                    __ExtendedProperties.Add(LogProperty.ThreadCurrentCulture, __Thread.CurrentCulture.EnglishName);
                    __ExtendedProperties.Add(LogProperty.ThreadCurrentUICulture, __Thread.CurrentUICulture.EnglishName);
                    __ExtendedProperties.Add(LogProperty.ThreadID, __Thread.ManagedThreadId);
                    __ExtendedProperties.Add(LogProperty.ThreadIsBackground, __Thread.IsBackground);
                    __ExtendedProperties.Add(LogProperty.ThreadIsPooled, __Thread.IsThreadPoolThread);
                    __ExtendedProperties.Add(LogProperty.ThreadPriority, __Thread.Priority.ToString());
                    __ExtendedProperties.Add(LogProperty.ThreadState, __Thread.ThreadState.ToString());
                    __ExtendedProperties.Add(LogProperty.ThreadWin32ID, logEntry.Win32ThreadId ?? "");
                }
            }
            catch
            {
                // ignore
            }
            #endregion

            #region Process Information
            try
            {
                // check to see if process information should be collected
                if (bool.Parse(_Attributes["includeProcess"]))
                {
                    // log the process properties
                    Process __Process = Process.GetCurrentProcess();
                    __ExtendedProperties.Add(LogProperty.ProcessID, __Process.Id);
                    __ExtendedProperties.Add(LogProperty.ProcessName, __Process.ProcessName);
                    __ExtendedProperties.Add(LogProperty.ProcessBasePriority, __Process.BasePriority);
                    __ExtendedProperties.Add(LogProperty.ProcessHandle, __Process.Handle.ToString());
                    __ExtendedProperties.Add(LogProperty.ProcessHandleCount, __Process.HandleCount);
                    __ExtendedProperties.Add(LogProperty.ProcessMaxWorkingSet, __Process.MaxWorkingSet.ToInt64());
                    __ExtendedProperties.Add(LogProperty.ProcessNonpagedSystemMemory, __Process.NonpagedSystemMemorySize64);
                    __ExtendedProperties.Add(LogProperty.ProcessPagedMemorySize, __Process.PagedMemorySize64);
                    __ExtendedProperties.Add(LogProperty.ProcessPagedSystemMemorySize, __Process.PagedSystemMemorySize64);
                    __ExtendedProperties.Add(LogProperty.ProcessPeakPagedMemorySize, __Process.PeakPagedMemorySize64);
                    __ExtendedProperties.Add(LogProperty.ProcessPeakVirtualMemorySize, __Process.PeakVirtualMemorySize64);
                    __ExtendedProperties.Add(LogProperty.ProcessPeakWorkingSet, __Process.PeakWorkingSet64);
                    __ExtendedProperties.Add(LogProperty.ProcessPrivateMemorySize, __Process.PrivateMemorySize64);
                    __ExtendedProperties.Add(LogProperty.ProcessPrivilegedProcessorTime, __Process.PrivilegedProcessorTime);
                    __ExtendedProperties.Add(LogProperty.ProcessStartTime, __Process.StartTime);
                    __ExtendedProperties.Add(LogProperty.ProcessThreadCount, __Process.Threads.Count);
                    __ExtendedProperties.Add(LogProperty.ProcessTotalProcessorTime, __Process.TotalProcessorTime);
                    __ExtendedProperties.Add(LogProperty.ProcessUserProcessorTime, __Process.UserProcessorTime);
                    __ExtendedProperties.Add(LogProperty.ProcessVirtualMemorySize, __Process.VirtualMemorySize64);
                    __ExtendedProperties.Add(LogProperty.ProcessWorkingSet, __Process.WorkingSet64);
                }
            }
            catch
            {
                // ignore
            }
            #endregion

            #region AppDomain Information
            try
            {
                // check to see if application domain information should be collected
                if (bool.Parse(_Attributes["includeAppDomain"]))
                {
                    // log the app domain properties
                    AppDomain __AppDomain = AppDomain.CurrentDomain;
                    __ExtendedProperties.Add(LogProperty.AppDomainName, __AppDomain.FriendlyName);
                    __ExtendedProperties.Add(LogProperty.AppDomainBaseDirectory, __AppDomain.BaseDirectory);
                    __ExtendedProperties.Add(LogProperty.AppDomainDynamicDirectory, __AppDomain.DynamicDirectory);
                    __ExtendedProperties.Add(LogProperty.AppDomainRelativeSearchPath, __AppDomain.RelativeSearchPath);
                    __ExtendedProperties.Add(LogProperty.AppDomainConfigFile, __AppDomain.SetupInformation.ConfigurationFile);
                }
            }
            catch
            {
                // ignore
            }
            #endregion

            #region Type Information
            try
            {
                // check to see if the type information should be collected
                if (bool.Parse(_Attributes["includeType"]) && __ExtendedProperties.ContainsKey("Context"))
                {
                    // get the context object
                    object context = __ExtendedProperties["Context"];

                    // check to see if a context object was supplied
                    if (null != context)
                    {
                        // remove the context entry
                        __ExtendedProperties.Remove("Context");

                        // get the type of the context object
                        Type __Type = (context is Type) ? context as Type : context.GetType();
                        // log information about the type
                        __ExtendedProperties.Add(LogProperty.TypeName, __Type.FullName);
                        __ExtendedProperties.Add(LogProperty.AssemblyName, __Type.Assembly.FullName);
                        __ExtendedProperties.Add(LogProperty.AssemblyPath, __Type.Assembly.Location);
                    }
                }
            }
            catch
            {
                // ignore
            }
            #endregion

            // return the base format
            try
            {
                #region Null değerler format içerisinde hata verdiğinden eleme yapılyor
                var nullExtProps = __ExtendedProperties.Where(x => x.Value == null).ToList();
                for (int expProp = 0; expProp < nullExtProps.Count; expProp++)
                {
                    __ExtendedProperties.Remove(nullExtProps[expProp]);
                } 
                #endregion

                __result = base.Format(logEntry);
            }
            catch (Exception)
            {
                
            }

            // return the formatted result
            return __result;
        }

        #endregion
    }
}
