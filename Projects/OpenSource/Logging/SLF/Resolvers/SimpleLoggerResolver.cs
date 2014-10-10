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

namespace Slf.Resolvers
{
  /// <summary>
  /// A factory resolver that provides a simplified API by
  /// allowing to directly set the <see cref="ILogger"/> instance
  /// that is being returned by its internal <see cref="ILoggerFactory"/>
  /// instance.<br/>
  /// Use this class in order to provide a simple resolver that does not
  /// need to resolve factories or loggers for different logger names,
  /// but always returns the same <see cref="ILogger"/>.
  /// </summary>
  public class SimpleLoggerResolver : IFactoryResolver
  {
    /// <summary>
    /// Internal factory - not exposed.
    /// </summary>
    private readonly SimpleLoggerFactory factory = new SimpleLoggerFactory();


    /// <summary>
    /// Gets or sets the logger that is being returned by the
    /// factory. If a null value is being assigned, the factory
    /// falls back to a <see cref="NullLogger"/> instance.
    /// </summary>
    public ILogger Logger
    {
      get { return factory.Logger; }
      set { factory.Logger = value; }
    }


    #region construction

    
    /// <summary>
    /// Initializes a new instance of the resolver, which uses an
    /// <see cref="NullLogger"/> instance until the <see cref="Logger"/>
    /// property is set explicitly. 
    /// </summary>
    public SimpleLoggerResolver()
    {
    }


    /// <summary>
    /// Initializes a new instance of the resolver with the logger to be
    /// maintained.
    /// </summary>
    /// <param name="logger">The logger to be returned by this resolver's
    /// factory.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="logger"/>
    /// is a null reference.</exception>
    public SimpleLoggerResolver(ILogger logger)
    {
      Ensure.ArgumentNotNull(logger, "logger");
      Logger = logger;
    }


    #endregion



    /// <summary>
    /// Determines a factory which cab create an
    /// <see cref="ILogger"/> instance based on a
    /// request for a named logger.
    /// </summary>
    /// <param name="loggerName">The logger name.</param>
    /// <returns>A factory which in turn is responsible for creating
    /// a given <see cref="ILogger"/> implementation.</returns>
    public ILoggerFactory GetFactory(string loggerName)
    {
      return factory;
    }
  }
}
