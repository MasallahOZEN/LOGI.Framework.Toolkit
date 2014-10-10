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

namespace Slf.Resolvers
{
  /// <summary>
  /// A resolver function that returns a given factory that
  /// creates a named logger.
  /// </summary>
  /// <param name="name">The name of the requested logger. Used to determine
  /// the correct factory.</param>
  /// <returns>A factory which in turn creates an instance of the requested
  /// named logger.</returns>
  public delegate ILoggerFactory FactoryRequestHandler(string name);


  /// <summary>
  /// A resolver that uses a delegate in order to create a requested
  /// factory.
  /// </summary>
  public class DelegateFactoryResolver : IFactoryResolver
  { 
    /// <summary>
    /// The resolver function that returns a factory which is responsible
    /// for creating a named logger.
    /// </summary>
    public FactoryRequestHandler RequestHandler { get; private set; }


    /// <summary>
    /// Creates the resolver based on two resolver functions.
    /// </summary>
    /// <param name="requestHandler">The resolver function that returns
    /// a factory which in turn is responsible for creating a given <see cref="ILogger"/>.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="requestHandler"/>
    /// is a null reference.</exception>
    public DelegateFactoryResolver(FactoryRequestHandler requestHandler)
    {
      Ensure.ArgumentNotNull(requestHandler, "requestHandler");
      RequestHandler = requestHandler;
    }


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
      return RequestHandler(loggerName);
    }
  }
}
