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

namespace Slf
{
  /// <summary>
  /// A logger decorator that forwards logging
  /// messages to an arbitrary number of underlying
  /// loggers.
  /// </summary>
  public class CompositeLogger : LoggerBase
  {
    /// <summary>
    /// The underlying loggers that receive logging messages.
    /// </summary>
    public List<ILogger> Loggers { get; private set; }


    /// <summary>
    /// Creates a named empty logger that does not produce any output
    /// as long as the <see cref="Loggers"/> list does not
    /// contain and <see cref="ILogger"/> instances.
    /// </summary>
    /// <param name="name">The logger name.</param>
    public CompositeLogger(string name)
      : base(name)
    {
      Loggers = new List<ILogger>();
    }

    /// <summary>
    /// Creates an empty logger that does not produce any output
    /// as long as the <see cref="Loggers"/> list does not
    /// contain and <see cref="ILogger"/> instances.
    /// </summary>
    public CompositeLogger()
      : base()
    {
      Loggers = new List<ILogger>();
    }


    /// <summary>
    /// Inits a named logger with a number of underlying loggers
    /// that are supposed to receive logging messages.
    /// </summary>
    /// <param name="name">The logger name.</param>
    /// <param name="loggers">The loggers that will all receive
    /// the logged messages</param>
    /// <exception cref="ArgumentNullException">If <paramref name="loggers"/>
    /// is a null reference.</exception>
    public CompositeLogger(string name, params ILogger[] loggers)
      : base(name)
    {
      Ensure.ArgumentNotNull(loggers, "loggers");
      Loggers = new List<ILogger>(loggers);
    }

    /// <summary>
    /// Inits a logger with a number of underlying loggers
    /// that are supposed to receive logging messages.
    /// </summary>
    /// <param name="loggers">The loggers that will all receive
    /// the logged messages</param>
    /// <exception cref="ArgumentNullException">If <paramref name="loggers"/>
    /// is a null reference.</exception>
    public CompositeLogger(params ILogger[] loggers)
      : base()
    {
      Ensure.ArgumentNotNull(loggers, "loggers");
      Loggers = new List<ILogger>(loggers);
    }


    /// <summary>
    /// Forwards a given log entry to the underlying
    /// <see cref="Loggers"/>.
    /// </summary>
    /// <param name="item">Encaspulates logging information.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="item"/>
    /// is a null reference.</exception>
    public override void Log(LogItem item)
    {
      Ensure.ArgumentNotNull(item, "item");

      foreach (var logger in Loggers)
      {
        logger.Log(item);
      }
    }
  }
}
