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

namespace Slf
{
  /// <summary>
  /// Responsible for finding and creating <see cref="ILoggerFactory"/>
  /// instances which are being used to create loggers of a given implementation.
  /// </summary>
  public interface IFactoryResolver
  {
    /// <summary>
    /// Determines a factory which in turn creates an
    /// <see cref="ILogger"/> instance based on a
    /// request for a named logger.
    /// </summary>
    /// <param name="loggerName">The name of the requested logger. This name will also
    /// be used to determine the factory that is responsible to create the logger
    /// instance.</param>
    /// <returns>A factory which in turn is responsible for creating
    /// a given <see cref="ILogger"/> implementation.</returns>
    ILoggerFactory GetFactory(string loggerName);
  }
}
