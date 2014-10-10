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

namespace Slf.Factories
{
  public class ConsoleLoggerFactory : NamedLoggerFactoryBase<ConsoleLogger>
  {
    protected override ConsoleLogger CreateLogger(string name)
    {
      return new ConsoleLogger(name);
    }
  }

  public class DebugLoggerFactory : NamedLoggerFactoryBase<DebugLogger>
  {
    protected override DebugLogger CreateLogger(string name)
    {
      return new DebugLogger(name);
    }
  }

  public class TestLoggerFactory : NamedLoggerFactoryBase<TestLogger>
  {
    protected override TestLogger CreateLogger(string name)
    {
      return new TestLogger(name);
    }
  }



  /// <summary>
  /// A logger factory that provides delegate loggers with naming
  /// support.
  /// </summary>
  public class DelegateLoggerFactory : NamedLoggerFactoryBase<DelegateLogger>
  {
    /// <summary>
    /// A delegate that is invoked by the
    /// factory's loggers, which is supposed to perform the
    /// actual logging.
    /// </summary>
    public Action<LogItem> LogAction { get; private set; }

    /// <summary>
    /// Creates the factory.
    /// </summary>
    /// <param name="logAction">A delegate that is invoked by the
    /// factory's loggers, which is supposed to perform the
    /// actual logging.
    /// </param>
    /// <exception cref="ArgumentNullException">If <paramref name="logAction"/>
    /// is a null reference.</exception>
    public DelegateLoggerFactory(Action<LogItem> logAction)
    {
      Ensure.ArgumentNotNull(logAction, "logAction");
      LogAction = logAction;
    }


    /// <summary>
    /// Constructs a logger with the given name
    /// </summary>
    /// <param name="name">The logger name.</param>
    /// <returns>A logger with the given name.</returns>
    protected override DelegateLogger CreateLogger(string name)
    {
      return new DelegateLogger(name, LogAction);
    }
  }


#if !SILVERLIGHT

  public class TraceLoggerFactory : NamedLoggerFactoryBase<TraceLogger>
  {
    protected override TraceLogger CreateLogger(string name)
    {
      return new TraceLogger(name);
    }
  }

#endif
}
