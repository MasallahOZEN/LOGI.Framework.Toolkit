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

namespace Slf
{
  /// <summary>
  /// A dummy implementation of the <see cref="ILogger"/>
  /// interface, which doesn't log anything. Use this implementation
  /// in order not to have to check for null references when
  /// using an <see cref="ILogger"/>.<br/>
  /// This logger implementation is a Singleton. You can get the singleton
  /// instance through the <see cref="Instance"/> property.
  /// </summary>
  public class NullLogger : ILogger
  {
    #region singleton

    /// <summary>
    /// Singleton instance.
    /// </summary>
    private static readonly NullLogger instance = new NullLogger();

    /// <summary>
    /// Provides access to the singleton instance of
    /// the class.
    /// </summary>
    public static NullLogger Instance
    {
      get { return instance; }
    }

    /// <summary>
    /// Private constructor. A reference to the Singleton
    /// instance of this class is available through the
    /// static <see cref="Instance"/> property.
    /// </summary>
    private NullLogger()
    {
    }

    #endregion


    /// <summary>
    /// Always returns <see cref="LoggerService.DefaultLoggerName"/>.
    /// </summary>
    public string Name
    {
      get { return LoggerService.DefaultLoggerName; }
    }


    /// <summary>
    /// Logs an arbitrary object with the <see cref="LogLevel.Debug"/> level. 
    /// </summary>
    /// <param name="obj">The message object to log, which will be converted
    /// into a string during logging.</param>
    public void Debug(object obj)
    {
    }

    /// <summary>
    /// Logs a debug message (<see cref="LogLevel.Debug"/>).
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void Debug(string message)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Debug"/> level. 
    /// </summary>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Debug(string format, params object[] args)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Debug"/> level. 
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> which provides
    /// culture-specific formatting capabilities.</param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Debug(IFormatProvider provider, string format, params object[] args)
    {
    }

    /// <summary>
    /// Logs an exception with a logging level of <see cref="LogLevel.Debug"/>.
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    public void Debug(Exception exception)
    {
    }

    /// <summary>
    /// Logs an exception and an additional message with a logging level of
    /// <see cref="LogLevel.Debug"/>.
    /// </summary>
    /// <param name="exception"> The exception to log.</param>
    /// <param name="message">Additional information regarding the
    /// logged exception.</param>
    public void Debug(Exception exception, string message)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Debug"/> level. 
    /// </summary>
    /// <param name="exception">The exception to log </param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Debug(Exception exception, string format, params object[] args)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Debug"/> level. 
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    /// <param name="provider">An <see cref="IFormatProvider"/> which provides
    /// culture-specific formatting capabilities.</param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Debug(Exception exception, string format, IFormatProvider provider, params object[] args)
    {
    }

    /// <summary>
    /// Logs an arbitrary object with the <see cref="LogLevel.Info"/> level. 
    /// </summary>
    /// <param name="obj">The message object to log, which will be converted
    /// into a string during logging.</param>
    public void Info(object obj)
    {
    }

    /// <summary>
    /// Logs an informational message (<see cref="LogLevel.Info"/>).
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void Info(string message)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Info"/> level. 
    /// </summary>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Info(string format, params object[] args)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Info"/> level. 
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> which provides
    /// culture-specific formatting capabilities.</param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Info(IFormatProvider provider, string format, params object[] args)
    {
    }

    /// <summary>
    /// Logs an exception with a logging level of <see cref="LogLevel.Info"/>.
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    public void Info(Exception exception)
    {
    }

    /// <summary>
    /// Logs an exception and an additional message with a logging level of
    /// <see cref="LogLevel.Info"/>.
    /// </summary>
    /// <param name="exception"> The exception to log.</param>
    /// <param name="message">Additional information regarding the
    /// logged exception.</param>
    public void Info(Exception exception, string message)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Info"/> level. 
    /// </summary>
    /// <param name="exception">The exception to log </param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Info(Exception exception, string format, params object[] args)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Info"/> level. 
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    /// <param name="provider">An <see cref="IFormatProvider"/> which provides
    /// culture-specific formatting capabilities.</param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Info(Exception exception, string format, IFormatProvider provider, params object[] args)
    {
    }

    /// <summary>
    /// Logs an arbitrary object with the <see cref="LogLevel.Warn"/> level. 
    /// </summary>
    /// <param name="obj">The message object to log, which will be converted
    /// into a string during logging.</param>
    public void Warn(object obj)
    {
    }

    /// <summary>
    /// Logs a warning message (<see cref="LogLevel.Warn"/>).
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void Warn(string message)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Warn"/> level. 
    /// </summary>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Warn(string format, params object[] args)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Warn"/> level. 
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> which provides
    /// culture-specific formatting capabilities.</param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Warn(IFormatProvider provider, string format, params object[] args)
    {
    }

    /// <summary>
    /// Logs an exception with a logging level of <see cref="LogLevel.Warn"/>.
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    public void Warn(Exception exception)
    {
    }

    /// <summary>
    /// Logs an exception and an additional message with a logging level of
    /// <see cref="LogLevel.Warn"/>.
    /// </summary>
    /// <param name="exception"> The exception to log.</param>
    /// <param name="message">Additional information regarding the
    /// logged exception.</param>
    public void Warn(Exception exception, string message)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Warn"/> level. 
    /// </summary>
    /// <param name="exception">The exception to log </param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Warn(Exception exception, string format, params object[] args)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Warn"/> level. 
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    /// <param name="provider">An <see cref="IFormatProvider"/> which provides
    /// culture-specific formatting capabilities.</param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Warn(Exception exception, string format, IFormatProvider provider, params object[] args)
    {
    }

    /// <summary>
    /// Logs an arbitrary object with the <see cref="LogLevel.Error"/> level. 
    /// </summary>
    /// <param name="obj">The message object to log, which will be converted
    /// into a string during logging.</param>
    public void Error(object obj)
    {
    }

    /// <summary>
    /// Logs an error message (<see cref="LogLevel.Error"/>).
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void Error(string message)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Error"/> level. 
    /// </summary>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Error(string format, params object[] args)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Error"/> level. 
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> which provides
    /// culture-specific formatting capabilities.</param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Error(IFormatProvider provider, string format, params object[] args)
    {
    }

    /// <summary>
    /// Logs an exception with a logging level of <see cref="LogLevel.Error"/>.
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    public void Error(Exception exception)
    {
    }

    /// <summary>
    /// Logs an exception and an additional message with a logging level of
    /// <see cref="LogLevel.Error"/>.
    /// </summary>
    /// <param name="exception"> The exception to log.</param>
    /// <param name="message">Additional information regarding the
    /// logged exception.</param>
    public void Error(Exception exception, string message)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Error"/> level. 
    /// </summary>
    /// <param name="exception">The exception to log </param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Error(Exception exception, string format, params object[] args)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Error"/> level. 
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    /// <param name="provider">An <see cref="IFormatProvider"/> which provides
    /// culture-specific formatting capabilities.</param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Error(Exception exception, string format, IFormatProvider provider, params object[] args)
    {
    }

    /// <summary>
    /// Logs an arbitrary object with the <see cref="LogLevel.Fatal"/> level. 
    /// </summary>
    /// <param name="obj">The message object to log, which will be converted
    /// into a string during logging.</param>
    public void Fatal(object obj)
    {
    }

    /// <summary>
    /// Logs a fatal error message (<see cref="LogLevel.Fatal"/>).
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void Fatal(string message)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Fatal"/> level. 
    /// </summary>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Fatal(string format, params object[] args)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Fatal"/> level. 
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> which provides
    /// culture-specific formatting capabilities.</param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Fatal(IFormatProvider provider, string format, params object[] args)
    {
    }

    /// <summary>
    /// Logs an exception with a logging level of <see cref="LogLevel.Fatal"/>.
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    public void Fatal(Exception exception)
    {
    }

    /// <summary>
    /// Logs an exception and an additional message with a logging level of
    /// <see cref="LogLevel.Fatal"/>.
    /// </summary>
    /// <param name="exception"> The exception to log.</param>
    /// <param name="message">Additional information regarding the
    /// logged exception.</param>
    public void Fatal(Exception exception, string message)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Fatal"/> level. 
    /// </summary>
    /// <param name="exception">The exception to log </param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Fatal(Exception exception, string format, params object[] args)
    {
    }

    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Fatal"/> level. 
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    /// <param name="provider">An <see cref="IFormatProvider"/> which provides
    /// culture-specific formatting capabilities.</param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    public void Fatal(Exception exception, string format, IFormatProvider provider, params object[] args)
    {
    }

    /// <summary>
    /// Creates a new log entry based on a given log item.
    /// </summary>
    /// <param name="item">Encaspulates logging information.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="item"/>
    /// is a null reference.</exception>
    public void Log(LogItem item)
    {
      Ensure.ArgumentNotNull(item, "item");
    }
  }
}