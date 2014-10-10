using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using LOGI.Framework.Toolkit.HelperLibrary.Constants;

namespace Slf.EntLibFacade.Diagnostics
{  
    /// <summary>
    /// A <see cref="System.Diagnostics.TraceListener"/> that writes to a database, formatting the output with an <see cref="ILogFormatter"/>.
    /// </summary>
    [ConfigurationElementType(typeof(VerboseDatabaseTraceListenerDataWingsServices))]
    public class VerboseDatabaseTraceListenerWingsServices : FormattedTraceListenerBase
    {
        string writeLogStoredProcName = String.Empty;
        string addCategoryStoredProcName = String.Empty;
        Database database;

        /// <summary>
        /// Initializes a new instance of <see cref="VerboseDatabaseTraceListenerWingsServices"/>.
        /// </summary>
        /// <param name="database">The database for writing the log.</param>
        /// <param name="writeLogStoredProcName">The stored procedure name for writing the log.</param>
        /// <param name="addCategoryStoredProcName">The stored procedure name for adding a category for this log.</param>
        /// <param name="formatter">The formatter.</param>        
        public VerboseDatabaseTraceListenerWingsServices(
            Database database,
            string writeLogStoredProcName,
            string addCategoryStoredProcName,
            ILogFormatter formatter)
            : base(formatter)
        {
            this.writeLogStoredProcName = writeLogStoredProcName;
            this.addCategoryStoredProcName = addCategoryStoredProcName;
            this.database = database;
        }

        /// <summary>
        /// The Write method 
        /// </summary>
        /// <param name="message">The message to log</param>
        public override void Write(string message)
        {
            ExecuteWriteLogStoredProcedure(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, TraceEventType.Information, string.Empty, DateTime.Now, string.Empty,
                                            string.Empty, string.Empty, string.Empty, null, null, message, database);
        }

        /// <summary>
        /// The WriteLine method.
        /// </summary>
        /// <param name="message">The message to log</param>
        public override void WriteLine(string message)
        {
            Write(message);
        }

        /// <summary>
        /// Delivers the trace data to the underlying database.
        /// </summary>
        /// <param name="eventCache">The context information provided by <see cref="System.Diagnostics"/>.</param>
        /// <param name="source">The name of the trace source that delivered the trace data.</param>
        /// <param name="eventType">The type of event.</param>
        /// <param name="id">The id of the event.</param>
        /// <param name="data">The data to trace.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                if (data is LogEntry)
                {
                    LogEntry logEntry = data as LogEntry;
                    if (ValidateParameters(logEntry))
                        ExecuteStoredProcedure(logEntry);
                }
                else if (data is string)
                {
                    Write(data as string);
                }
                else
                {
                    base.TraceData(eventCache, source, eventType, id, data);
                }
            }
        }

        /// <summary>
        /// Declare the supported attributes for <see cref="VerboseDatabaseTraceListenerWingsServices"/>
        /// </summary>
        protected override string[] GetSupportedAttributes()
        {
            return new string[4] { "formatter", "writeLogStoredProcName", "addCategoryStoredProcName", "databaseInstanceName" };
        }

        /// <summary>
        /// Validates that enough information exists to attempt executing the stored procedures
        /// </summary>
        /// <param name="logEntry">The LogEntry to validate.</param>
        /// <returns>A Boolean indicating whether the parameters for the LogEntry configuration are valid.</returns>
        private bool ValidateParameters(LogEntry logEntry)
        {
            bool valid = true;

            if (writeLogStoredProcName == null ||
                writeLogStoredProcName.Length == 0)
            {
                return false;
            }

            if (addCategoryStoredProcName == null ||
                addCategoryStoredProcName.Length == 0)
            {
                return false;
            }

            return valid;
        }

        /// <summary>
        /// Executes the stored procedures
        /// </summary>
        /// <param name="logEntry">The LogEntry to store in the database</param>
        private void ExecuteStoredProcedure(LogEntry logEntry)
        {
            using (DbConnection connection = database.CreateConnection())
            {
                connection.Open();
                try
                {
                    using (DbTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            int logID = Convert.ToInt32(ExecuteWriteLogStoredProcedure(logEntry, database, transaction));
                            ExecuteAddCategoryStoredProcedure(logEntry, logID, database, transaction);
                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }

                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Executes the WriteLog stored procedure
        /// </summary>
        
        /// <param name="searchId">The searchId for this LogEntry.</param>
       
        /// <param name="severity">The severity for this LogEntry.</param>
        /// <param name="title">The title for this LogEntry.</param>
        /// <param name="timeStamp">The timestamp for this LogEntry.</param>
        /// <param name="machineName">The machine name for this LogEntry.</param>
        /// <param name="appDomainName">The appDomainName for this LogEntry.</param>
        /// <param name="processId">The process id for this LogEntry.</param>
        /// <param name="processName">The processName for this LogEntry.</param>
        /// <param name="managedThreadName">The managedthreadName for this LogEntry.</param>
        /// <param name="win32ThreadId">The win32threadID for this LogEntry.</param>
        /// <param name="message">The message for this LogEntry.</param>
        /// <param name="db">An instance of the database class to use for storing the LogEntry</param>
        /// <returns>An integer for the LogEntry Id</returns>
        private int ExecuteWriteLogStoredProcedure(string referenceNumber, string sourceType, string processType, string processTitle, string quarableXmlData, string originalData, TraceEventType severity, string title, DateTime timeStamp,
                                                    string machineName, string appDomainName, string processId, string processName,
                                                    string managedThreadName, string win32ThreadId, string message, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand(writeLogStoredProcName);
            
            db.AddParameter(cmd, "severity", DbType.String, 32, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, severity.ToString());
            db.AddParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.SourceType, DbType.String, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, sourceType);
            //db.AddInParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ModuleId, DbType.Int32, moduleId);
            //db.AddInParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ModuleItemId, DbType.Int32, moduleItemId);
            db.AddParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ProcessType, DbType.String, 100, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, processType);
            db.AddParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ProcessTitle, DbType.String, 100, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, processTitle);
            db.AddParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ReferenceNumber, DbType.String, 512, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, referenceNumber);
            db.AddParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.QuarableXmlData, DbType.Xml, 100, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, quarableXmlData);
            db.AddParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.OriginalData, DbType.Xml, 100, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, originalData);

            db.AddInParameter(cmd, "timestamp", DbType.DateTime, timeStamp);
            db.AddParameter(cmd, "machineName", DbType.String, 32, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, machineName);
            db.AddParameter(cmd, "message", DbType.String, 1500, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, message);
            db.AddInParameter(cmd, "formattedmessage", DbType.String, message);

            db.AddOutParameter(cmd, "LogId", DbType.Int32, 4);

            db.ExecuteNonQuery(cmd);
            int logId = Convert.ToInt32(cmd.Parameters[cmd.Parameters.Count - 1].Value, CultureInfo.InvariantCulture);
            return logId;
        }

        /// <summary>
        /// Executes the WriteLog stored procedure
        /// </summary>
        /// <param name="logEntry">The LogEntry to store in the database.</param>
        /// <param name="db">An instance of the database class to use for storing the LogEntry</param>
        /// <param name="transaction">The transaction that wraps around the execution calls for storing the LogEntry</param>
        /// <returns>An integer for the LogEntry Id</returns>
        private int ExecuteWriteLogStoredProcedure(LogEntry logEntry, Database db, DbTransaction transaction)
        {
            DbCommand cmd = db.GetStoredProcCommand(writeLogStoredProcName);

            var referenceNumber = "";
            var sourceType = "";
            var processType = "";
            var processTitle = "";
            var quarableXmlData = "";
            var originalData = "";
            var userName = "";
            var sepetLogKey = "";
            var sessionId = "";

            var userNameToken = "UserName";
            var sepetLogKeyToken = "SepetLogKey";
            var sessionIdToken = "SessionId";

            #region LogEntry.ExtendedProperties
            if (logEntry.ExtendedProperties != null && logEntry.ExtendedProperties.Count > 0)
            {
                if (logEntry.ExtendedProperties.ContainsKey(userNameToken))
                {
                    userName = logEntry.ExtendedProperties[userNameToken].ToString();
                    logEntry.ExtendedProperties.Remove(userNameToken);
                }

                if (logEntry.ExtendedProperties.ContainsKey(sepetLogKeyToken))
                {
                    sepetLogKey = logEntry.ExtendedProperties[sepetLogKeyToken].ToString();
                    logEntry.ExtendedProperties.Remove(sepetLogKeyToken);
                }

                if (logEntry.ExtendedProperties.ContainsKey(sessionIdToken))
                {
                    sessionId = logEntry.ExtendedProperties[sessionIdToken].ToString();
                    logEntry.ExtendedProperties.Remove(sessionIdToken);
                }

                if (logEntry.ExtendedProperties.ContainsKey(ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ReferenceNumber))
                {
                    referenceNumber = logEntry.ExtendedProperties[ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ReferenceNumber].ToString();
                    logEntry.ExtendedProperties.Remove(ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ReferenceNumber);
                }

                if (logEntry.ExtendedProperties.ContainsKey(ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.SourceType))
                {
                    sourceType = logEntry.ExtendedProperties[ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.SourceType].ToString();
                    logEntry.ExtendedProperties.Remove(ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.SourceType);
                }

                if (logEntry.ExtendedProperties.ContainsKey(ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ProcessType))
                {
                    processType = logEntry.ExtendedProperties[ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ProcessType].ToString();
                    logEntry.ExtendedProperties.Remove(ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ProcessType);
                }

                if (logEntry.ExtendedProperties.ContainsKey(ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ProcessTitle))
                {
                    processTitle = logEntry.ExtendedProperties[ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ProcessTitle].ToString();
                    logEntry.ExtendedProperties.Remove(ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ProcessTitle);
                }

                if (logEntry.ExtendedProperties.ContainsKey(ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.QuarableXmlData))
                {
                    quarableXmlData = logEntry.ExtendedProperties[ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.QuarableXmlData].ToString();
                    logEntry.ExtendedProperties.Remove(ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.QuarableXmlData);
                }

                if (logEntry.ExtendedProperties.ContainsKey(ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.OriginalData))
                {
                    originalData = logEntry.ExtendedProperties[ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.OriginalData].ToString();
                    logEntry.ExtendedProperties.Remove(ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.OriginalData);
                }

            }
            #endregion

            
            db.AddParameter(cmd, "severity", DbType.String, 32, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, logEntry.Severity.ToString());
            db.AddParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.SourceType, DbType.String, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, sourceType);
            //db.AddInParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ModuleId, DbType.Int32, moduleId);
            //db.AddInParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ModuleItemId, DbType.Int32, moduleItemId);
            db.AddInParameter(cmd, userNameToken, DbType.String, userName);
            db.AddInParameter(cmd, sepetLogKeyToken, DbType.String, sepetLogKey);
            db.AddInParameter(cmd, sessionIdToken, DbType.String, sessionId);
            db.AddParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ProcessType, DbType.String, 100, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, processType);
            db.AddParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ProcessTitle, DbType.String, 100, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, processTitle);
            db.AddParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.ReferenceNumber, DbType.String, 512, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, referenceNumber);
            db.AddInParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.QuarableXmlData, DbType.Xml, quarableXmlData);
            db.AddInParameter(cmd, ConstantHelper.TravelPortalCore.LogColumns.ModulesLogData.OriginalData, DbType.String,originalData);

            db.AddInParameter(cmd, "timestamp", DbType.DateTime, logEntry.TimeStamp);
            db.AddParameter(cmd, "machineName", DbType.String, 32, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, logEntry.MachineName);
            db.AddParameter(cmd, "message", DbType.String, 1500, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, logEntry.Message);
            
            if (Formatter != null)
                db.AddInParameter(cmd, "formattedmessage", DbType.String, Formatter.Format(logEntry));
            else
                db.AddInParameter(cmd, "formattedmessage", DbType.String, logEntry.Message);

            db.AddOutParameter(cmd, "LogId", DbType.Int32, 4);

            db.ExecuteNonQuery(cmd, transaction);
            int logId = Convert.ToInt32(cmd.Parameters[cmd.Parameters.Count - 1].Value, CultureInfo.InvariantCulture);
            return logId;
        }

        /// <summary>
        /// Executes the AddCategory stored procedure
        /// </summary>
        /// <param name="logEntry">The LogEntry to store in the database.</param>
        /// <param name="logID">The unique identifer for the LogEntry as obtained from the WriteLog Stored procedure.</param>
        /// <param name="db">An instance of the database class to use for storing the LogEntry</param>
        /// <param name="transaction">The transaction that wraps around the execution calls for storing the LogEntry</param>
        private void ExecuteAddCategoryStoredProcedure(LogEntry logEntry, int logID, Database db, DbTransaction transaction)
        {
            foreach (string category in logEntry.Categories)
            {
                DbCommand cmd = db.GetStoredProcCommand(addCategoryStoredProcName);
                db.AddInParameter(cmd, "categoryName", DbType.String, category);
                db.AddInParameter(cmd, "logID", DbType.Int32, logID);
                db.ExecuteNonQuery(cmd, transaction);
            }
        }
    }
}
