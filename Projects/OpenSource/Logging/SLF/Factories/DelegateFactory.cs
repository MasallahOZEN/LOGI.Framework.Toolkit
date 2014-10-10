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



namespace Slf.Factories
{
  /// <summary>
  /// A delegate that is used by the <see cref="DelegateFactory"/>
  /// in order to get a named logger if its
  /// <see cref="DelegateFactory.GetLogger(string)"/> method is being
  /// invoked.
  /// </summary>
  /// <returns>The logger implementation that is being resolved based on the
  /// request.</returns>
  public delegate ILogger LoggerRequestHandler(string loggerName);

  /// <summary>
  /// A factory implementation that delegates requests to default and/or
  /// named loggers to delegates.
  /// </summary>
  public class DelegateFactory : ILoggerFactory
  {

    /// <summary>
    /// A delegate which is called if the factory's <see cref="GetLogger(string)"/>
    /// method is being invoked.<br/>
    /// Setting the delegate to null (default value) causes the factory to
    /// return a <see cref="NullLogger"/> instance if <see cref="GetLogger(string)"/>
    /// is being invoked.
    /// </summary>
    public LoggerRequestHandler RequestHandler { get; set; }

    /// <summary>
    /// Empty default constructor, which causes the factory to return <see cref="NullLogger"/>
    /// instances if <see cref="GetLogger(string)"/> being invoked,
    /// until the <see cref="RequestHandler"/> property is being set.
    /// </summary>
    public DelegateFactory()
    {   
    }


    /// <summary>
    /// Initializes the factory with a delegate that receives requests for named
    /// logger instances.
    /// </summary>
    /// <param name="requestHandler">A delegate which is called if the
    /// factory's <see cref="GetLogger(string)"/> method is being invoked.<br/>
    /// Submitting a null reference causes the factory to
    /// return a <see cref="NullLogger"/> instance if <see cref="GetLogger(string)"/>
    /// is being invoked.</param>
    public DelegateFactory(LoggerRequestHandler requestHandler)
    {
      //null references are being accepted
      RequestHandler = requestHandler;
    }


    /// <summary>
    /// Obtains an <see cref="ILogger"/> instance that is identified by
    /// the given name by invoking the <see cref="RequestHandler"/> delegate.
    /// If the delegate is not set, a <see cref="NullLogger"/> instance is being returned instead.
    /// </summary>
    /// <param name="name">The logger name.</param>
    /// <returns>An <see cref="ILogger"/> instance</returns>
    public ILogger GetLogger(string name)
    {
      return RequestHandler == null ? NullLogger.Instance : RequestHandler(name);
    }
  }
}
