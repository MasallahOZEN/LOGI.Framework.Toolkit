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
  /// A factory resolver that always returns a given
  /// <see cref="ILoggerFactory"/>, which can be set
  /// at runtime.
  /// </summary>
  public class SimpleFactoryResolver : IFactoryResolver
  {
    private ILoggerFactory factory = NullLoggerFactory.Instance;

    /// <summary>
    /// Gets or sets the logger factory that is used by
    /// this resolver. If a null reference is being assigned,
    /// this resolver automatically falls back to a
    /// <see cref="NullLoggerFactory"/> instance.
    /// </summary>
    public ILoggerFactory Factory
    {
      get { return factory; }
      set { factory = value ?? NullLoggerFactory.Instance; }
    }


    #region construction
    
    /// <summary>
    /// Initializes a new instance of the resolver, which uses an
    /// <see cref="NullLoggerFactory"/> instance until the <see cref="Factory"/>
    /// property is set explicitly. 
    /// </summary>
    public SimpleFactoryResolver()
    {
    }


    /// <summary>
    /// Initializes a new instance of the resolver with the logger to be
    /// maintained.
    /// </summary>
    /// <param name="factory">The factory to be returned by this resolver's
    /// <see cref="GetFactory"/> method.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="factory"/>
    /// is a null reference.</exception>
    public SimpleFactoryResolver(ILoggerFactory factory)
    {
      Ensure.ArgumentNotNull(factory, "factory");
      Factory = factory;
    }

    #endregion



    /// <summary>
    /// Determines a factory which cab create an
    /// <see cref="ILogger"/> instance based on a
    /// request for a named logger.
    /// </summary>
    /// <param name="loggerName">The logger name.</param>
    /// <returns>This implementation always returns the currently
    /// assigned <see cref="Factory"/>, regardless of the submitted
    /// <paramref name="loggerName"/>.</returns>
    public ILoggerFactory GetFactory(string loggerName)
    {
      return factory;
    }
  }
}
