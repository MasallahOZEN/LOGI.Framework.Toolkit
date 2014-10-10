using System;
using System.Collections;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using LOGI.Framework.Toolkit.HelperLibrary.Constants;

namespace Slf.EntLibFacade
{
  /// <summary>
  /// An implementation of the <see cref="ILogger"/>
  /// interface which outputs logged data using
  /// the <see cref="Logger"/> of the MS Enterprise
  /// Library.
  /// </summary>
  public class EnterpriseLibraryLogger : LoggerBase
  {
    /// <summary>
    /// Creates a named logger.
    /// </summary>
    /// <param name="name">The logger name.</param>
    public EnterpriseLibraryLogger(string name)
      : base(name)
    {      
    }

    /// <summary>
    /// Writes a log entry to the Enterprise Library's
    /// logging block. Output depends on the logging
    /// block's configuration.
    /// </summary>
    /// <param name="item">A <see cref="LogItem"/> which encapsulates
    /// information to be logged.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="item"/>
    /// is a null reference.</exception>
    public override void Log(LogItem item)
    {
        if (item == null) throw new ArgumentNullException("item");

        LogEntry entry = ConvertLogItem(item, this.Name);

        if (entry!=null && entry.ExtendedProperties.ContainsKey("LogID"))
        {
            item.ExtendedProperties.Add("LogID", entry.ExtendedProperties["LogID"]);
        }

        Logger.Write(entry);
    }


    /// <summary>
    /// Creates a <c>LogEntry</c> instance which can be processed
    /// by the Enterprise Library based on a <see cref="LogItem"/>.
    /// </summary>
    /// <param name="item">A <see cref="LogItem"/> which encapsulates information
    /// to be logged.</param>
    /// <returns>An Enterprise Library item which corresponds
    /// to the submitted <c>LogItem</c>.</returns>
    private static LogEntry ConvertLogItem(LogItem item, string name)
    {
      //assign properties
      LogEntry entry = new LogEntry();
      entry.Message = item.Message;
      entry.Title = item.Title;
      entry.EventId = item.EventId ?? 0;
      entry.Severity = GetTraceEventType(item.LogLevel);
      entry.TimeStamp = item.Timestamp.DateTime;

        switch (item.LogLevel)
        {
            case LogLevel.Undefined:
                entry.Priority = -2;
                break;
            case LogLevel.Debug:
                entry.Priority = 1;
                break;
            case LogLevel.Info:
                entry.Priority = 2;
                break;
            case LogLevel.Warn:
                entry.Priority = 3;
                break;
            case LogLevel.Error:
                entry.Priority = 4;
                break;
            case LogLevel.Fatal:
                entry.Priority = 5;
                entry.Categories.Add(ConstantHelper.Logging.Log2EMailCategoryName);
                break;
            default:
                break;
        }
      if (item.Exception != null)
      {
        entry.AddErrorMessage(item.Exception.ToString());
      }

      #region Map categories
      // Map logger name to EntLib categories
      if (!string.IsNullOrEmpty(item.LoggerName))
      {
          string[] categories = item.LoggerName.Split(',');
          foreach (string category in categories)
          {
              entry.Categories.Add(category.Trim());
          }
      }

      // Map logger name to EntLib categories
      if (!string.IsNullOrEmpty(name))
      {
          string[] categories = name.Split(',');
          foreach (string category in categories)
          {
              entry.Categories.Add(category.Trim());
          }
      } 
      #endregion

      #region Map ExtendedProps
      if (item.ExtendedProperties!=null && item.ExtendedProperties.Count>0)
      {
          foreach (var extendedProperty in item.ExtendedProperties)
          {
              entry.ExtendedProperties.Add(extendedProperty);
          }
      }
      #endregion

      #region Map Exception Data As ExtendedProps
      if (item.Exception != null && item.Exception.Data !=null && item.Exception.Data.Count > 0)
      {
          try
          {
              foreach (DictionaryEntry extendedProperty in item.Exception.Data)
              {
                  try
                  {
                      entry.ExtendedProperties.Add(extendedProperty.Key.ToString(), extendedProperty.Value);
                  }
                  catch (Exception)
                  {
                  }
              }
          }
          catch (Exception)
          {
          }
      }
      #endregion


      return entry;
    }


    private static TraceEventType GetTraceEventType(LogLevel logLevel)
    {
      switch (logLevel)
      {
        case LogLevel.Debug:
          return TraceEventType.Verbose;
        case LogLevel.Undefined:
        case LogLevel.Info:
          return TraceEventType.Information;
        case LogLevel.Warn:
          return TraceEventType.Warning;
        case LogLevel.Error:
          return TraceEventType.Error;
        case LogLevel.Fatal:
          return TraceEventType.Critical;
        default:
          System.Diagnostics.Debug.Fail("Unknown log level received: " + logLevel);
          return TraceEventType.Critical;
      }
    }
  }
}