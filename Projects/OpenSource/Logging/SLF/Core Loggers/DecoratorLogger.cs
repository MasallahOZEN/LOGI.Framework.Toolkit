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
  /// A function that can be used to filter and/or transform
  /// a given log item.
  /// </summary>
  /// <param name="item">The log entry to process.</param>
  /// <returns>True if the item should be forwarded to the underlying
  /// <see cref="ILogger"/>.</returns>
  public delegate bool LogFilter(LogItem item);


  /// <summary>
  /// Simple <see cref="ILogger"/> implementation that decorates
  /// an existing logger implementation and allows to transform and/or
  /// filter logged messages.
  /// </summary>
  public class DecoratorLogger : LoggerBase
  {
    /// <summary>
    /// A filtering mechanism that allows to programmatically intercept or
    /// transform a given <see cref="LogItem"/> before having it logged through
    /// the underyling <see cref="Logger"/>.
    /// </summary>
    public LogFilter Filter { get; private set; }


    /// <summary>
    /// The decorated <see cref="ILogger"/> instance. This logger's
    /// <see cref="ILogger.Log"/> method is being invoked if the
    /// <see cref="Filter"/> function returns true.
    /// </summary>
    public ILogger Logger { get; private set; }


    /// <summary>
    /// Creates a named logger with a decorated <see cref="ILogger"/>
    /// implementation and a filter function that can be used to update or discard
    /// the message before delegating the actual logging to the decorated
    /// <paramref name="logger"/>.
    /// </summary>
    /// <param name="name">The logger name.</param>
    /// <param name="logger">A decorated logger. If the <paramref name="filter"/>
    /// function </param>
    /// <param name="filter">A filter message that can be used to transform
    /// a submitted <see cref="LogItem"/> before getting it logged by the
    /// underlying logger, or to ignore it completely (if the function returns
    /// false).</param>
    /// <exception cref="ArgumentNullException">If <paramref name="logger"/>
    /// is a null reference.</exception>
    /// <exception cref="ArgumentNullException">If <paramref name="filter"/>
    /// is a null reference.</exception>
    public DecoratorLogger(string name, ILogger logger, LogFilter filter)
      : base(name)
    {
      Ensure.ArgumentNotNull(logger, "logger");
      Ensure.ArgumentNotNull(filter, "filter");

      Filter = filter;
      Logger = logger;
    }

    /// <summary>
    /// Creates a logger with a decorated <see cref="ILogger"/>
    /// implementation and a filter function that can be used to update or discard
    /// the message before delegating the actual logging to the decorated
    /// <paramref name="logger"/>.
    /// </summary>
    /// <param name="logger">A decorated logger. If the <paramref name="filter"/>
    /// function </param>
    /// <param name="filter">A filter message that can be used to transform
    /// a submitted <see cref="LogItem"/> before getting it logged by the
    /// underlying logger, or to ignore it completely (if the function returns
    /// false).</param>
    /// <exception cref="ArgumentNullException">If <paramref name="logger"/>
    /// is a null reference.</exception>
    /// <exception cref="ArgumentNullException">If <paramref name="filter"/>
    /// is a null reference.</exception>
    public DecoratorLogger(ILogger logger, LogFilter filter)
      : base()
    {
      Ensure.ArgumentNotNull(logger, "logger");
      Ensure.ArgumentNotNull(filter, "filter");

      Filter = filter;
      Logger = logger;
    }


    /// <summary>
    /// Creates a new log entry based on a given log item.
    /// </summary>
    /// <param name="item">Encaspulates logging information.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="item"/>
    /// is a null reference.</exception>
    public override void Log(LogItem item)
    {
      Ensure.ArgumentNotNull(item, "item");

      //submit the item to the filter / transformator, then write to delegated logger
      if (Filter(item))
      {
        Logger.Log(item);
      }
    }

  }

}
