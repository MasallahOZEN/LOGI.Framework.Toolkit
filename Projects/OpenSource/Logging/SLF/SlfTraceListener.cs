// SLF.NET - Simple Logging Façade for .NET
// Contact and Information: http://slf.codeplex.com
//
// This library is free software; you can redistribute it and/or
// modify it in any way you see fit as long as this copyright
// notice is not being removed.
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//
// THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE


using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Slf
{
  /// <summary>
  /// A trace listener that redirect logging information
  /// from Trace to SLf.
  /// </summary>
  public class SlfTraceListener : TraceListener
  {
    #region fail

    public override void Fail(string message, string detailMessage)
    {
      string msg = message + Environment.NewLine + detailMessage;
      Fail(msg);
    }


    public override void Fail(string message)
    {
      Log(LogLevel.Error, message);
    }

    #endregion


    #region write

    /// <summary>
    /// Forwards a given message as an information to SLF.
    /// </summary>
    /// <param name="message">A message to write.</param>
    public override void Write(string message)
    {
      Log(LogLevel.Info, message);
    }

    /// <summary>
    /// Forwards a given message as an information to SLF.
    /// </summary>
    /// <param name="message">A message to write.</param>
    public override void WriteLine(string message)
    {
      Log(LogLevel.Info, message + Environment.NewLine);
    }

    #endregion


    #region trace data

    public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id,
                                   object data)
    {
      TraceData(eventCache, source, eventType, id, new[] {data});
    }


    public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id,
                                   params object[] data)
    {
      if ((Filter == null) || Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
      {
        StringBuilder builder = new StringBuilder();

        //create header
        CreateHeader(builder, source, eventType, id);

        //append data
        if (data != null)
        {
          for (int i = 0; i < data.Length; i++)
          {
            if (i != 0)
            {
              builder.Append(", ");
            }
            if (data[i] != null)
            {
              builder.Append(data[i].ToString());
            }
          }
        }

        //create footer
        CreateFooter(builder, eventCache);

        //log full message
        Log(eventType, builder);
      }
    }

    #endregion


    #region trace event

    public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id,
                                    string message)
    {
      if ((Filter == null) || Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
      {
        StringBuilder builder = new StringBuilder();
        CreateHeader(builder, source, eventType, id);
        builder.AppendLine(message);
        CreateFooter(builder, eventCache);

        Log(eventType, builder);
      }
    }

    public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id,
                                    string format, params object[] args)
    {
      if ((Filter == null) || Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
      {
        StringBuilder builder = new StringBuilder();
        CreateHeader(builder, source, eventType, id);
        if (args != null)
        {
          builder.AppendFormat(CultureInfo.InvariantCulture, format, args);
          builder.AppendLine();
        }
        else
        {
          builder.AppendLine(format);
        }
        CreateFooter(builder, eventCache);
        Log(eventType, builder);
      }
    }

    #endregion


    #region create message header / footer

    private static void CreateHeader(StringBuilder builder, string source, TraceEventType eventType, int id)
    {
      builder.AppendFormat(CultureInfo.InvariantCulture, "{0} {1}: {2} : ", new object[] {source, eventType, id});
    }


    private void CreateFooter(StringBuilder builder, TraceEventCache eventCache)
    {
      if (eventCache != null)
      {
        if (IsOptionEnabled(TraceOptions.ProcessId))
        {
          builder.AppendLine("ProcessId=" + eventCache.ProcessId);
        }

        if (IsOptionEnabled(TraceOptions.LogicalOperationStack))
        {
          builder.Append("LogicalOperationStack=");
          Stack logicalOperationStack = eventCache.LogicalOperationStack;
          bool flag = true;
          foreach (object obj2 in logicalOperationStack)
          {
            if (!flag)
            {
              builder.Append(", ");
            }
            else
            {
              flag = false;
            }
            builder.Append(obj2.ToString());
          }
          builder.AppendLine(string.Empty);
        }
        if (IsOptionEnabled(TraceOptions.ThreadId))
        {
          builder.AppendLine("ThreadId=" + eventCache.ThreadId);
        }
        if (IsOptionEnabled(TraceOptions.DateTime))
        {
          builder.AppendLine("DateTime=" + eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
        }
        if (IsOptionEnabled(TraceOptions.Timestamp))
        {
          builder.AppendLine("Timestamp=" + eventCache.Timestamp);
        }
        if (IsOptionEnabled(TraceOptions.Callstack))
        {
          builder.AppendLine("Callstack=" + eventCache.Callstack);
        }
      }
    }

    #endregion


    #region helpers

    private bool IsOptionEnabled(TraceOptions opts)
    {
      return ((opts & TraceOutputOptions) != TraceOptions.None);
    }

    private static LogLevel GetLevel(TraceEventType eventType)
    {
      switch (eventType)
      {
        case TraceEventType.Critical:
          return LogLevel.Fatal;
        case TraceEventType.Error:
          return LogLevel.Error;
        case TraceEventType.Warning:
          return LogLevel.Warn;
        case TraceEventType.Verbose:
          return LogLevel.Debug;
        case TraceEventType.Information:
        case TraceEventType.Start:
        case TraceEventType.Stop:
        case TraceEventType.Suspend:
        case TraceEventType.Resume:
        case TraceEventType.Transfer:
          return LogLevel.Info;
        default:
          throw new ArgumentOutOfRangeException("eventType");
      }
    }

    #endregion


    protected static void Log(TraceEventType eventType, StringBuilder builder)
    {
      LogLevel level = GetLevel(eventType);
      Log(level, builder.ToString());
    }

    protected static void Log(LogLevel level, string message)
    {
      var item = new LogItem {LogLevel = level, Message = message};
      LoggerService.GetLogger().Log(item);
    }

  }
}