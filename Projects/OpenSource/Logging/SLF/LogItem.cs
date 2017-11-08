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
using System.Xml.Serialization;

using System.Collections.Generic;


namespace Slf
{
    /// <summary>
    /// Encapsulates logging data that can be submitted to any
    /// given <see cref="ILogger"/> implementation.
    /// </summary>
    public class LogItem
    {

        #region properties

        /// <summary>
        /// The logging level, which defaults to <see cref="Slf.LogLevel.Info"/>.
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Date and time of the log entry. If no explicitly
        /// set, this property provides the timestamp of
        /// the object's creation.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// A summarizing title for the logged entry. Defaults to
        /// <c>String.Empty</c>.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The logged message body. Defaults to
        /// <c>String.Empty</c>.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The name of the logger used to log this item.
        /// </summary>
        public string LoggerName { get; set; }

        /// <summary>
        /// Allows to attach an exception to the message.
        /// Defaults to <c>null</c>.
        /// </summary>
        [XmlIgnore]
        public Exception Exception { get; set; }

        /// <summary>
        /// Event number or identifier. Defaults to null.
        /// </summary>
        public int? EventId { get; set; }

        /// <summary>
        /// EntLib LogEntry Categories
        /// </summary>
        public ICollection<string> Categories { get; set; }

        /// <summary>
        /// EntLib LogEntry ExtendedProperties
        /// </summary>
        public List<LogItemProperty> LogItemProperties { get; set; }

        /// <summary>
        /// EntLib LogEntry Priority
        /// </summary>
        public int Priority { get; set; }
        #endregion


        /// <summary>
        /// Inits an new <see cref="LogItem"/> instance which
        /// is initialized with default values.
        /// </summary>
        public LogItem()
        {
            Categories = new System.Collections.ObjectModel.Collection<string>();
            LogItemProperties = new List<LogItemProperty>();
            Title = String.Empty;
            Message = String.Empty;
            LogLevel = LogLevel.Info;
            LoggerName = String.Empty;
            Priority = 0;
            Categories.Clear();
            Timestamp = DateTimeOffset.Now;
        }

        /// <summary>
        /// Inits an new <see cref="LogItem"/> instance which
        /// is initialized with default values.
        /// </summary>
        public LogItem(int priority)
        {
            Categories = new System.Collections.ObjectModel.Collection<string>();
            LogItemProperties = new List<LogItemProperty>();
            Title = String.Empty;
            Message = String.Empty;
            LogLevel = LogLevel.Info;
            LoggerName = String.Empty;
            Priority = priority;
            Categories.Clear();
            Timestamp = DateTimeOffset.Now;
        }


        #region clone

        /// <summary>
        /// Creates a new <see cref="LogItem"/> that is a copy of the current instance.
        /// </summary>
        /// <returns>A new <c>LogItem</c> that is a copy of the current instance.</returns>
        public LogItem Clone()
        {
            LogItem clone = new LogItem
            {
                Title = Title,
                Message = Message,
                Exception = Exception,
                EventId = EventId,
                LogLevel = LogLevel,
                Timestamp = Timestamp,
                LoggerName = LoggerName,
                Priority = Priority,
                Categories = Categories,
                LogItemProperties = LogItemProperties
            };
            return clone;
        }

        #endregion

    }
}