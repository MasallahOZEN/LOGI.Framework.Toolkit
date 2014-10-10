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
using Slf.Formatters;

namespace Slf
{
  /// <summary>
  /// Base class that extends <see cref="LoggerBase"/> with a custom
  /// <see cref="ILogItemFormatter"/> by implementing the
  /// <see cref="IFormattableLogger"/> interface.<br/>
  /// Derive from this base class if you want to use simple
  /// string conversion for logged <see cref="LogItem"/> instances.
  /// </summary>
  public abstract class FormattableLoggerBase : LoggerBase, IFormattableLogger
  {
    private ILogItemFormatter formatter;

    /// <summary>
    /// Gets or sets a given formatter which is used to produce
    /// the output of the logger.
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/>
    /// is a null reference.</exception>
    public ILogItemFormatter Formatter
    {
      get { return formatter; }
      set
      {
        Ensure.ArgumentNotNull(value, "value");
        formatter = value;
      }
    }


    /// <summary>
    /// Creates a named logger, and uses the default <see cref="SimpleLogItemFormatter"/>
    /// to format logged <see cref="LogItem"/> instances.
    /// </summary>
    /// <param name="name">The logger name.</param>
    protected FormattableLoggerBase(string name)
      : this(name, SimpleLogItemFormatter.Instance)
    {
    }

    /// <summary>
    /// Creates the logger, and uses the default <see cref="SimpleLogItemFormatter"/>
    /// to format logged <see cref="LogItem"/> instances.
    /// </summary>
    /// <param name="name">The logger name.</param>
    protected FormattableLoggerBase()
      : this(SimpleLogItemFormatter.Instance)
    {
    }

    /// <summary>
    /// Creates the logger with a given formatter.
    /// </summary>
    /// <param name="formatter">The formatter to be used in order to
    /// create string representations of logged <see cref="LogItem"/>
    /// instances.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="formatter"/>
    /// is a null reference.</exception>
    protected FormattableLoggerBase(ILogItemFormatter formatter)
      : base()
    {
      Ensure.ArgumentNotNull(formatter, "formatter");
      Formatter = formatter;
    }

    /// <summary>
    /// Creates a named logger with a given formatter.
    /// </summary>
    /// <param name="name">The logger name.</param>
    /// <param name="formatter">The formatter to be used in order to
    /// create string representations of logged <see cref="LogItem"/>
    /// instances.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="formatter"/>
    /// is a null reference.</exception>
    protected FormattableLoggerBase(string name, ILogItemFormatter formatter)
      : base(name)
    {
      Ensure.ArgumentNotNull(formatter, "formatter");
      Formatter = formatter;
    }


    /// <summary>
    /// Simple helper method which returns the formatted string
    /// representation of the submitted <see cref="LogItem"/> by
    /// invoking the <see cref="ILogItemFormatter.FormatItem"/>
    /// method of the assigned <see cref="Formatter"/>.
    /// </summary>
    /// <param name="item">The item to be processed.</param>
    /// <returns>Formatted logging data.</returns>
    protected string FormatItem(LogItem item)
    {
      return Formatter.FormatItem(item);
    }
  }
}