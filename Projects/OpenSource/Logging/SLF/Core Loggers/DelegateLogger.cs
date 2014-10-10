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
  /// An <see cref="ILogger"/> implementation that delegates
  /// the actual logging to an <see cref="Action{T}"/> delegate.
  /// </summary>
  public class DelegateLogger : LoggerBase
  {
    /// <summary>
    /// The action delegate that is being invoked for every <see cref="LogItem"/>
    /// to be logged.
    /// </summary>
    protected Action<LogItem> LogAction { get; private set; }


    /// <summary>
    /// Creates a named logger with the action to be invoked for logged items.
    /// </summary>
    /// <param name="logAction">The action delegate that is being
    /// invoked for every <see cref="LogItem"/> to be logged.</param>
    /// <param name="name">The logger name.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="logAction"/>
    /// is a null reference.</exception>
    public DelegateLogger(string name, Action<LogItem> logAction)
      : base(name)
    {
      Ensure.ArgumentNotNull(logAction, "logAction");
      LogAction = logAction;
    }

    /// <summary>
    /// Creates a logger with the action to be invoked for logged items.
    /// </summary>
    /// <param name="logAction">The action delegate that is being
    /// invoked for every <see cref="LogItem"/> to be logged.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="logAction"/>
    /// is a null reference.</exception>
    public DelegateLogger(Action<LogItem> logAction)
      : base()
    {
      Ensure.ArgumentNotNull(logAction, "logAction");
      LogAction = logAction;
    }



    /// <summary>
    /// Invokes the <see cref="LogAction"/> for a given
    /// <see cref="LogItem"/>.
    /// </summary>
    /// <param name="item">Encaspulates logging information.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="item"/>
    /// is a null reference.</exception>
    public override void Log(LogItem item)
    {
      Ensure.ArgumentNotNull(item, "item");
      LogAction(item);
    }
  }
}
