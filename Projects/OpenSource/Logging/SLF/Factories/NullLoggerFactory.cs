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
  /// A dummy implementation of the <see cref="ILoggerFactory"/>
  /// which simply returns a <see cref="NullLogger"/> instance.
  /// </summary>
  public class NullLoggerFactory : ILoggerFactory
  {
    /// <summary>
    /// The shared <see cref="NullLogger"/> instance.
    /// </summary>
    private readonly ILogger logger = NullLogger.Instance;


    #region singleton

    /// <summary>
    /// Singleton instance.
    /// </summary>
    private static readonly NullLoggerFactory instance = new NullLoggerFactory();

    /// <summary>
    /// Provides access to the singleton instance of
    /// the class.
    /// </summary>
    public static NullLoggerFactory Instance
    {
      get { return instance; }
    }

    /// <summary>
    /// Private constructor. A reference to the Singleton
    /// instance of this class is available through the
    /// static <see cref="Instance"/> property.
    /// </summary>
    private NullLoggerFactory()
    {
    }

    #endregion


    /// <summary>
    /// Obtains an <see cref="ILogger"/> instance that is identified by
    /// the given name.
    /// </summary>
    /// <param name="name">The logger name.</param>
    /// <returns>An <see cref="ILogger"/> instance</returns>
    public ILogger GetLogger(string name)
    {
      return logger;
    }
  }
}