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
using System.Collections.Generic;
using System.Text;
using Slf;
using NLog;
using Slf.Formatters;
using NLog.Fluent;

namespace SLF.NLogFacade
{
    /// <summary>
    /// An implementation of the <see cref="ILogger"/>
    /// interface which logs messages via the NLog
    /// framework.
    /// </summary>
    public class NLogLogger : FormattableLoggerBase
    {
        /// <summary>
        /// The NLog logger which this class wraps.
        /// </summary>
        private Logger logger;

        /// <summary>
        /// Constructs an instance of <see cref="NLogLogger"/>
        /// by wrapping a NLog logger
        /// </summary>
        /// <param name="logger">The NLog logger to wrap</param>
        internal NLogLogger(Logger logger) : base(SingleLineFormatter.Instance)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Logs the given message. Output depends on the associated
        /// log4net configuration.
        /// </summary>
        /// <param name="item">A <see cref="LogItem"/> which encapsulates
        /// information to be logged.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="item"/>
        /// is a null reference.</exception>
        public override void Log(LogItem item)
        {
            if (item == null) throw new ArgumentNullException("item");
            
            string message = FormatItem(item);
            LogBuilder logBuilder = null;

            #region Initialize Log Builder
            switch (item.LogLevel)
            {
                case Slf.LogLevel.Fatal:
                    logBuilder = logger.Fatal();
                    break;

                case Slf.LogLevel.Error:
                    logBuilder = logger.Error();
                    break;

                case Slf.LogLevel.Warn:
                    logBuilder = logger.Warn();
                    break;

                case Slf.LogLevel.Info:
                    logBuilder = logger.Info();
                    break;

                case Slf.LogLevel.Debug:
                    logBuilder = logger.Debug();
                    break;

                default:
                    logBuilder = logger.Info();
                    break;
            } 
            #endregion

            if (item.Exception != null)
            {
                logBuilder = logBuilder.Exception(item.Exception);
            }
            if (item.ExtendedProperties?.Count>0)
            {
                foreach (var prpItem in item.ExtendedProperties)
                {
                    logBuilder.Property(prpItem.Key, prpItem.Value);
                }
            }

            logBuilder.Message(message).Write();
        }

        /// <summary>
        /// Overriden to delegate to the NLog IsXxxEnabled
        /// properties.
        /// </summary>
        protected override bool IsLogLevelEnabled(Slf.LogLevel level)
        {
            switch (level)
            {
                case Slf.LogLevel.Debug:
                    return logger.IsDebugEnabled;
                case Slf.LogLevel.Error:
                    return logger.IsErrorEnabled;
                case Slf.LogLevel.Fatal:
                    return logger.IsFatalEnabled;
                case Slf.LogLevel.Info:
                    return logger.IsInfoEnabled;
                case Slf.LogLevel.Warn:
                    return logger.IsWarnEnabled;
                default:
                    return true;
            }
        }
    }
}
