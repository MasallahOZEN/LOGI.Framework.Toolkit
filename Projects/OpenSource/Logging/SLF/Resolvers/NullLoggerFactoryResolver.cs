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
  /// A resolver that always returns a <see cref="NullLoggerFactory"/>,
  /// which in turn creates a <see cref="NullLogger"/> instance.
  /// </summary>
  public class NullLoggerFactoryResolver : IFactoryResolver
  {
    /// <summary>
    /// The logger factory instance.
    /// </summary>
    private static readonly ILoggerFactory factory = NullLoggerFactory.Instance;


    #region singleton

    /// <summary>
    /// Singleton instance.
    /// </summary>
    private static readonly NullLoggerFactoryResolver instance = new NullLoggerFactoryResolver();

    /// <summary>
    /// Provides access to the singleton instance of
    /// the class.
    /// </summary>
    public static NullLoggerFactoryResolver Instance
    {
      get { return instance; }
    }

    /// <summary>
    /// Private constructor. A reference to the Singleton
    /// instance of this class is available through the
    /// static <see cref="Instance"/> property.
    /// </summary>
    private NullLoggerFactoryResolver()
    {
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
