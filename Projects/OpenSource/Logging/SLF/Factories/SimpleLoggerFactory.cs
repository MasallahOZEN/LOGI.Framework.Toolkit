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
  /// <summary>
  /// A factory implementation that always returns the same
  /// <see cref="Logger"/> instance.<br/>
  /// Keep in mind that using this factory disables support for
  /// named loggers - it will always return the same logger instance,
  /// no matter what logger name is submitted.
  /// </summary>
  public class SimpleLoggerFactory : ILoggerFactory
  {
    private ILogger logger = NullLogger.Instance;
    
    /// <summary>
    /// Gets or sets the logger that is being returned by the
    /// factory. If a null value is being assigned, the factory
    /// falls back to a <see cref="NullLogger"/> instance.
    /// </summary>
    public ILogger Logger
    {
      get { return logger; }
      set { logger = value ?? NullLogger.Instance; }
    }


    /// <summary>
    /// Initializes a new instance of the factory, which uses an
    /// <see cref="NullLogger"/> instance until the <see cref="Logger"/>
    /// property is set explicitly. 
    /// </summary>
    public SimpleLoggerFactory()
    {
    }

    /// <summary>
    /// Initializes a new instance of the factory with the logger to be
    /// maintained by the factory.
    /// </summary>
    /// <param name="logger">The logger to be returned by the factory's
    /// <see cref="GetLogger"/> method.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="logger"/>
    /// is a null reference.</exception>
    public SimpleLoggerFactory(ILogger logger)
    {
      Ensure.ArgumentNotNull(logger, "logger");
      Logger = logger;
    }


    /// <summary>
    /// Obtains an <see cref="ILogger"/> instance that is identified by
    /// the given name.
    /// </summary>
    /// <param name="name">The logger name.</param>
    /// <returns>This factory always returns the currently assigned
    /// <see cref="Logger"/> property, regardless of the submitted
    /// <paramref name="name"/>.</returns>
    public ILogger GetLogger(string name)
    {
      return logger;
    }
  }
}
