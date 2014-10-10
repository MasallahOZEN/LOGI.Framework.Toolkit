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
using Slf.Factories;
using Slf.Resolvers;

namespace Slf
{
  /// <summary>
  /// Provides a global repository that provides access to <see cref="ILogger"/>
  /// instances. This class ensures that both the <see cref="GetLogger()"/>
  /// and <see cref="GetLogger(string)"/> methods always return a valid
  /// <see cref="ILogger"/> instance - if no matching logger can be resolved,
  /// the service automatically falls back to a <see cref="NullLogger"/> instance.
  /// </summary>
  public static class LoggerService
  {
    /// <summary>
    /// The name of the default logger, which is just an empty
    /// string.
    /// </summary>
    /// <remarks>
    /// SLF supports named loggers, meaning that a request for a logger with
    /// a particular name may return a specific <see cref="ILogger"/> instance.
    /// This also applies to the default logger that is being requested
    /// through the parameterless <see cref="GetLogger()"/> method, which just
    /// makes a query for a logger with a name of <see cref="String.Empty"/>.
    /// </remarks>
    public const string DefaultLoggerName = "";

    /// <summary>
    /// Non-Silverlight projects: Rely on configuration settings per default
    /// in order to make sure declarative configuration in app.config does
    /// not require setting the resolver programmatically.<br/>
    /// Silverlight: Use the NullLogger per default. 
    /// </summary>
    private static IFactoryResolver factoryResolver;

    /// <summary>
    /// A logger factory that maintains internally used diagnostic loggers, which 
    /// log information is something goes wrong within SLF.
    /// </summary>
    private static ILoggerFactory diagnosticLoggerFactory = new DebugLoggerFactory();

    /// <summary>
    /// Provides access to factories for both the default and
    /// named loggers.
    /// </summary>
    public static IFactoryResolver FactoryResolver
    {
      get { return factoryResolver; }
      set
      {
        factoryResolver = value ?? NullLoggerFactoryResolver.Instance;
      }
    }


    /// <summary>
    /// Initializes the logger service with a default resolver:<br/>
    /// Non-Silverlight projects: Rely on configuration settings per default
    /// in order to make sure declarative configuration in app.config does
    /// not require setting the resolver programmatically.<br/>
    /// Silverlight: Use the NullLogger per default. 
    /// </summary>
    static LoggerService()
    {
      Reset();
    }


    /// <summary>
    /// Gets the internal logger that is used to output debug information
    /// in case something went wrong within SLF.
    /// </summary>
    /// <param name="loggerName">Requested logger name.</param>
    /// <returns>A logger that outputs SLF's internal debug information.</returns>
    internal static ILogger GetDiagnosticLogger(string loggerName)
    {
      return diagnosticLoggerFactory.GetLogger(loggerName);
    }


    /// <summary>
    /// Provides all means to exchanged the internal factory that is used to
    /// manage internal diagnostic loggers which are returned by
    /// <see cref="GetDiagnosticLogger"/>.
    /// </summary>
    /// <param name="factory">The factory to be plugged in. Submit a null value
    /// to reset the factory to its default (a new <see cref="DebugLoggerFactory"/>).</param>
    internal static void SetDiagnosticLoggerFactory(ILoggerFactory factory)
    {
      diagnosticLoggerFactory = factory ?? new DebugLoggerFactory();
    }


    /// <summary>
    /// Discards customizations and resets the configured logger
    /// facilities to their defaults. For Silverlight, this means
    /// that the service returns <see cref="NullLogger"/> instances
    /// for all requests. In other environments, the class falls
    /// back to loggers that were configured through <c>app.config</c>.
    /// </summary>
    public static void Reset()
    {
#if SILVERLIGHT
      factoryResolver = NullLoggerFactoryResolver.Instance;
#else
      factoryResolver = new AppConfigFactoryResolver();
#endif
    }


    /// <summary>
    /// Gets a named  <see cref="ILogger"/> instance. This is the
    /// logger instance set by the <see cref="SetLogger"/> method, or
    /// a logger with the specified name if a factory can be resolved that
    /// returns.
    /// </summary>
    /// <param name="name">The name of the requested logger. If this parameter
    /// is a null reference, the <see cref="DefaultLoggerName"/> will be used instead.</param>
    /// <returns>Always returns a valid <see cref="ILogger"/> instance. If no logger can
    /// be resolved, an instance of <see cref="NullLogger"/> is returned.</returns>
    /// <remarks>This method applies several fallback strategies when trying to determine
    /// the requested logger.</remarks>
    public static ILogger GetLogger(string name)
    {
      if (name == null) name = DefaultLoggerName;

      //the name is used to resolve both the factory...
      ILoggerFactory factory = FactoryResolver.GetFactory(name);
      if (factory == null) return NullLogger.Instance;

      //...and logger instance
      return factory.GetLogger(name) ?? NullLogger.Instance;
    }


    /// <summary>
    /// Gets a default <see cref="ILogger"/> instance. This is the
    /// logger instance set by the <see cref="SetLogger"/> method, or
    /// a logger with a default name if a factory can be resolved through
    /// the <see cref="FactoryResolver"/>. Invoking this method is syntactically
    /// equivalent to invoking the <see cref="GetLogger(string)"/> method with
    /// a logger name of <see cref="DefaultLoggerName"/>.
    /// </summary>
    /// <returns>Always returns a valid <see cref="ILogger"/> instance. If no logger can
    /// be resolved, an instance of <see cref="NullLogger"/> is returned.</returns>
    public static ILogger GetLogger()
    {
      //call overload
      return GetLogger(DefaultLoggerName);
    }



    /// <summary>
    /// Plugs in a single <see cref="ILogger"/> instance that will be returned
    /// for all requests, including requests for named loggers
    /// (both <see cref="GetLogger()"/> and <see cref="GetLogger(string)"/>
    /// methods).
    /// </summary>
    /// <param name="loggerImplementation">The logger to be
    /// used globally. A null reference is a valid parameter value which resets
    /// the service to return a <see cref="NullLogger"/> instance.</param>
    /// <remarks>Assigning a logger through this method creates a custom
    /// <see cref="IFactoryResolver"/>. This resolver replaces the current
    /// <see cref="FactoryResolver"/> property in order to always return
    /// the submitted <paramref name="loggerImplementation"/> if
    /// <see cref="GetLogger()"/> or <see cref="GetLogger(string)"/> is being
    /// invoked.</remarks>
    public static void SetLogger(ILogger loggerImplementation)
    {
      ILogger logger = loggerImplementation ?? NullLogger.Instance;
      FactoryResolver = new SimpleLoggerResolver(logger);
    }
  }
}
