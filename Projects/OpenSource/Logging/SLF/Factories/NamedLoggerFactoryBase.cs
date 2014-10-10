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



using System.Collections.Generic;
using System.Globalization;

namespace Slf.Factories
{
  /// <summary>
  /// A base class for factories which created named logger
  /// instance.
  /// </summary>
  /// <typeparam name="TLogger">The type of logger which this factory
  /// creates</typeparam>
  public abstract class NamedLoggerFactoryBase<TLogger> : ILoggerFactory
    where TLogger : ILogger
  {
    /// <summary>
    /// A cache of named loggers
    /// </summary>
    private readonly Dictionary<string, TLogger> loggers = new Dictionary<string, TLogger>();

    /// <summary>
    /// An object which is used for locking
    /// </summary>
    private readonly object lockObj = new object();


    /// <summary>
    /// Gets a list of all currently cached loggers.
    /// </summary>
    /// <remarks>Invoking this method is thread safe.</remarks>
    protected IEnumerable<TLogger> GetCachedLoggers()
    {
      lock(lockObj)
      {
        return new List<TLogger>(loggers.Values);
      }
    }


    /// <summary>
    /// Gets this class' synchronization token.
    /// </summary>
    protected object GetLockObject()
    {
      return lockObj;
    }


    /// <summary>
    /// Obtains an <see cref="ILogger"/> instance that is identified by
    /// the given name.
    /// </summary>
    /// <param name="name">The logger name. If this parameter is a null
    /// reference, <see cref="LoggerService.DefaultLoggerName"/> is
    /// used instead.</param>
    /// <returns>A factory that can be identified by the 
    /// given name in the target output for this logger</returns>
    public ILogger GetLogger(string name)
    {
      if (name == null) name = LoggerService.DefaultLoggerName;
      string lowerName = name.ToLower(CultureInfo.InvariantCulture);

      lock (lockObj)
      {
        TLogger namedLogger;
        if (!loggers.TryGetValue(lowerName, out namedLogger))
        {
          namedLogger = CreateLogger(name);
          loggers.Add(lowerName, namedLogger);
        }
        return namedLogger;
      }
    }


    /// <summary>
    /// Constructs a logger with the given name
    /// </summary>
    /// <param name="name">The logger name.</param>
    /// <returns>A logger with the given name.</returns>
    protected abstract TLogger CreateLogger(string name);
   
  }
}
