namespace Slf.EntLibFacade.Diagnostics
{
    /// <summary>
    /// Defines property name constants used in application logging.
    /// </summary>
    internal struct LogProperty
    {
        // General properties
        public const string ApplicationName = "ApplicationName";
        public const string Category = "Category";
        public const string Context = "Context";
        // Environment properties
        public const string EnvCurrentDirectory = "EnvCurrentDirectory";
        public const string EnvOSVersion = "EnvOSVersion";
        public const string EnvUserDomainName = "EnvUserDomainName";
        public const string EnvUserName = "EnvUserName";
        public const string EnvVersion = "EnvVersion";
        public const string EnvWorkingSet = "EnvWorkingSet";
        // Thread properties
        public const string ThreadName = "ThreadName";
        public const string ThreadApartmentState = "ThreadApartmentState";
        public const string ThreadCurrentCulture = "ThreadCurrentCulture";
        public const string ThreadCurrentUICulture = "ThreadCurrentUICulture";
        public const string ThreadID = "ThreadID";
        public const string ThreadIsBackground = "ThreadIsBackground";
        public const string ThreadIsPooled = "ThreadIsPooled";
        public const string ThreadPriority = "ThreadPriority";
        public const string ThreadState = "ThreadState";
        public const string ThreadWin32ID = "ThreadWin32ID";
        // Process properties
        public const string ProcessID = "ProcessID";
        public const string ProcessName = "ProcessName";
        public const string ProcessBasePriority = "ProcessBasePriority";
        public const string ProcessHandle = "ProcessHandle";
        public const string ProcessHandleCount = "ProcessHandleCount";
        public const string ProcessMaxWorkingSet = "ProcessMaxWorkingSet";
        public const string ProcessNonpagedSystemMemory = "ProcessNonpagedSystemMemory";
        public const string ProcessPagedMemorySize = "ProcessPagedMemorySize";
        public const string ProcessPagedSystemMemorySize = "ProcessPagedSystemMemorySize";
        public const string ProcessPeakPagedMemorySize = "ProcessPeakPagedMemorySize";
        public const string ProcessPeakVirtualMemorySize = "ProcessPeakVirtualMemorySize";
        public const string ProcessPeakWorkingSet = "ProcessPeakWorkingSet";
        public const string ProcessPrivateMemorySize = "ProcessPrivateMemorySize";
        public const string ProcessPrivilegedProcessorTime = "ProcessPrivilegedProcessorTime";
        public const string ProcessStartTime = "ProcessStartTime";
        public const string ProcessThreadCount = "ProcessThreadCount";
        public const string ProcessTotalProcessorTime = "ProcessTotalProcessorTime";
        public const string ProcessUserProcessorTime = "ProcessUserProcessorTime";
        public const string ProcessVirtualMemorySize = "ProcessVirtualMemorySize";
        public const string ProcessWorkingSet = "ProcessWorkingSet";
        // AppDomain properties 
        public const string AppDomainName = "AppDomainName";
        public const string AppDomainBaseDirectory = "AppDomainBaseDirectory";
        public const string AppDomainDynamicDirectory = "AppDomainDynamicDirectory";
        public const string AppDomainRelativeSearchPath = "AppDomainRelativeSearchPath";
        public const string AppDomainConfigFile = "AppDomainConfigFile";
        // Type / assembly properties
        public const string TypeName = "TypeName";
        public const string AssemblyName = "AssemblyName";
        public const string AssemblyPath = "AssemblyPath";
    }
}
