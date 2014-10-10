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
  /// Common interface of an arbitrary implementation that provides
  /// logging capabilities.
  /// </summary>
  public interface ILogger
  {
    /// <summary>
    /// Gets this logger's name. If the instance is the default logger,
    /// it returns <see cref="LoggerService.DefaultLoggerName"/>, which
    /// is an empty string.
    /// </summary>
    string Name { get; }


    #region Debug

    /// <summary>
    /// Logs an arbitrary object with the <see cref="LogLevel.Debug"/> level. 
    /// </summary>
    /// <param name="obj">The message object to log, which will be converted
    /// into a string during logging.</param>
    void Debug(object obj);


    /// <summary>
    /// Logs a debug message (<see cref="LogLevel.Debug"/>).
    /// </summary>
    /// <param name="message">The message to log.</param>
    void Debug(string message);


    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Debug"/> level. 
    /// </summary>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    void Debug(string format, params object[] args);


    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Debug"/> level. 
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> which provides
    /// culture-specific formatting capabilities.</param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    void Debug(IFormatProvider provider, string format, params object[] args);


    /// <summary>
    /// Logs an exception with a logging level of <see cref="LogLevel.Debug"/>.
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    void Debug(Exception exception);


    /// <summary>
    /// Logs an exception and an additional message with a logging level of
    /// <see cref="LogLevel.Debug"/>.
    /// </summary>
    /// <param name="exception"> The exception to log.</param>
    /// <param name="message">Additional information regarding the
    /// logged exception.</param>
    void Debug(Exception exception, string message);


    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Debug"/> level. 
    /// </summary>
    /// <param name="exception">The exception to log </param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    void Debug(Exception exception, string format, params object[] args);


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
    void Debug(Exception exception, string format, IFormatProvider provider, params object[] args);

    #endregion

    #region Info

    /// <summary>
    /// Logs an arbitrary object with the <see cref="LogLevel.Info"/> level. 
    /// </summary>
    /// <param name="obj">The message object to log, which will be converted
    /// into a string during logging.</param>
    void Info(object obj);


    /// <summary>
    /// Logs an informational message (<see cref="LogLevel.Info"/>).
    /// </summary>
    /// <param name="message">The message to log.</param>
    void Info(string message);


    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Info"/> level. 
    /// </summary>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    void Info(string format, params object[] args);


    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Info"/> level. 
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> which provides
    /// culture-specific formatting capabilities.</param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    void Info(IFormatProvider provider, string format, params object[] args);


    /// <summary>
    /// Logs an exception with a logging level of <see cref="LogLevel.Info"/>.
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    void Info(Exception exception);


    /// <summary>
    /// Logs an exception and an additional message with a logging level of
    /// <see cref="LogLevel.Info"/>.
    /// </summary>
    /// <param name="exception"> The exception to log.</param>
    /// <param name="message">Additional information regarding the
    /// logged exception.</param>
    void Info(Exception exception, string message);


    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Info"/> level. 
    /// </summary>
    /// <param name="exception">The exception to log </param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    void Info(Exception exception, string format, params object[] args);


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
    void Info(Exception exception, string format, IFormatProvider provider, params object[] args);

    #endregion

    #region Warn

    /// <summary>
    /// Logs an arbitrary object with the <see cref="LogLevel.Warn"/> level. 
    /// </summary>
    /// <param name="obj">The message object to log, which will be converted
    /// into a string during logging.</param>
    void Warn(object obj);


    /// <summary>
    /// Logs a warning message (<see cref="LogLevel.Warn"/>).
    /// </summary>
    /// <param name="message">The message to log.</param>
    void Warn(string message);


    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Warn"/> level. 
    /// </summary>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    void Warn(string format, params object[] args);


    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Warn"/> level. 
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> which provides
    /// culture-specific formatting capabilities.</param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    void Warn(IFormatProvider provider, string format, params object[] args);


    /// <summary>
    /// Logs an exception with a logging level of <see cref="LogLevel.Warn"/>.
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    void Warn(Exception exception);


    /// <summary>
    /// Logs an exception and an additional message with a logging level of
    /// <see cref="LogLevel.Warn"/>.
    /// </summary>
    /// <param name="exception"> The exception to log.</param>
    /// <param name="message">Additional information regarding the
    /// logged exception.</param>
    void Warn(Exception exception, string message);


    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Warn"/> level. 
    /// </summary>
    /// <param name="exception">The exception to log </param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    void Warn(Exception exception, string format, params object[] args);


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
    void Warn(Exception exception, string format, IFormatProvider provider, params object[] args);

    #endregion

    #region Error

    /// <summary>
    /// Logs an arbitrary object with the <see cref="LogLevel.Error"/> level. 
    /// </summary>
    /// <param name="obj">The message object to log, which will be converted
    /// into a string during logging.</param>
    void Error(object obj);


    /// <summary>
    /// Logs an error message (<see cref="LogLevel.Error"/>).
    /// </summary>
    /// <param name="message">The message to log.</param>
    void Error(string message);


    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Error"/> level. 
    /// </summary>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    void Error(string format, params object[] args);


    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Error"/> level. 
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> which provides
    /// culture-specific formatting capabilities.</param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    void Error(IFormatProvider provider, string format, params object[] args);


    /// <summary>
    /// Logs an exception with a logging level of <see cref="LogLevel.Error"/>.
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    void Error(Exception exception);


    /// <summary>
    /// Logs an exception and an additional message with a logging level of
    /// <see cref="LogLevel.Error"/>.
    /// </summary>
    /// <param name="exception"> The exception to log.</param>
    /// <param name="message">Additional information regarding the
    /// logged exception.</param>
    void Error(Exception exception, string message);


    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Error"/> level. 
    /// </summary>
    /// <param name="exception">The exception to log </param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    void Error(Exception exception, string format, params object[] args);


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
    void Error(Exception exception, string format, IFormatProvider provider, params object[] args);

    #endregion

    #region Fatal

    /// <summary>
    /// Logs an arbitrary object with the <see cref="LogLevel.Fatal"/> level. 
    /// </summary>
    /// <param name="obj">The message object to log, which will be converted
    /// into a string during logging.</param>
    void Fatal(object obj);


    /// <summary>
    /// Logs a fatal error message (<see cref="LogLevel.Fatal"/>).
    /// </summary>
    /// <param name="message">The message to log.</param>
    void Fatal(string message);


    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Fatal"/> level. 
    /// </summary>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    void Fatal(string format, params object[] args);


    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Fatal"/> level. 
    /// </summary>
    /// <param name="provider">An <see cref="IFormatProvider"/> which provides
    /// culture-specific formatting capabilities.</param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    void Fatal(IFormatProvider provider, string format, params object[] args);


    /// <summary>
    /// Logs an exception with a logging level of <see cref="LogLevel.Fatal"/>.
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    void Fatal(Exception exception);


    /// <summary>
    /// Logs an exception and an additional message with a logging level of
    /// <see cref="LogLevel.Fatal"/>.
    /// </summary>
    /// <param name="exception"> The exception to log.</param>
    /// <param name="message">Additional information regarding the
    /// logged exception.</param>
    void Fatal(Exception exception, string message);


    /// <summary>
    /// Logs a formatted message string with the <see cref="LogLevel.Fatal"/> level. 
    /// </summary>
    /// <param name="exception">The exception to log </param>
    /// <param name="format">A composite format string that contains placeholders for the
    /// arguments.</param>
    /// <param name="args">An <see cref="object"/> array containing zero or more objects
    /// to format.</param>
    void Fatal(Exception exception, string format, params object[] args);


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
    void Fatal(Exception exception, string format, IFormatProvider provider, params object[] args);

    #endregion


    /// <summary>
    /// Creates a new log entry based on a given log item.
    /// </summary>
    /// <param name="item">Encaspulates logging information.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="item"/>
    /// is a null reference.</exception>
    void Log(LogItem item);
  }
}