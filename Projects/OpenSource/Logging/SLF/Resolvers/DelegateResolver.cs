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



using Slf.Factories;

namespace Slf.Resolvers
{
  /// <summary>
  /// A resolver that works upon a single <see cref="DelegateFactory"/>
  /// which can be configured with custom delegates in order to resolve
  /// a given logger.<br/>
  /// This is a convenience class that allows you to quickly configure more
  /// complex configuration scenarios through code.
  /// </summary>
  public class DelegateResolver : IFactoryResolver
  {
    /// <summary>
    /// The underlying logger factory which maintains the assigned
    /// delegates.
    /// </summary>
    private readonly DelegateFactory factory;


    /// <summary>
    /// A delegate which is called if the factory's <see cref="ILoggerFactory.GetLogger"/>
    /// method is being invoked.<br/>
    /// Setting the delegate to null (default value) causes the factory to
    /// return a <see cref="NullLogger"/> instance if <see cref="ILoggerFactory.GetLogger"/>
    /// is being invoked.
    /// </summary>
    public LoggerRequestHandler RequestHandler
    {
      get { return factory.RequestHandler; }
      set { factory.RequestHandler = value; }
    }


    /// <summary>
    /// Empty default constructor, which causes the underlying factory to return <see cref="NullLogger"/>
    /// instances for <see cref="ILoggerFactory.GetLogger(string)"/>
    /// until the <see cref="RequestHandler"/> property is being set.
    /// </summary>
    public DelegateResolver()
    {
      factory = new DelegateFactory();
    }


    /// <summary>
    /// Initializes the underlying factory with delegates that receive requests for both the
    /// default and named loggers.
    /// </summary>
    /// <param name="requestHandler">A delegate which is called if the underlying
    /// factory's <see cref="ILoggerFactory.GetLogger(string)"/> method is being invoked.<br/>
    /// Submitting a null reference causes the underlying factory to
    /// return a <see cref="NullLogger"/> instance if <see cref="ILoggerFactory.GetLogger(string)"/>
    /// is being invoked.</param>
    public DelegateResolver(LoggerRequestHandler requestHandler)
    {
      //null references are being accepted
      factory = new DelegateFactory(requestHandler);
    }


    /// <summary>
    /// Determines a factory which cab create an
    /// <see cref="ILogger"/> instance based on a
    /// request for a named logger. This method returns a factory
    /// that uses the assigned <see cref="RequestHandler"/> in order
    /// to resolve a logger based on a named request.
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
