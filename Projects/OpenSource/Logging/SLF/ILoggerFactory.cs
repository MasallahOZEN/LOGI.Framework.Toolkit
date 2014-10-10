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
  /// Defines factory methods for obtaining <see cref="ILogger"/>
  /// instances. These factory methods may create new instances
  /// or retrieve cached / pooled instances depending on the the
  /// name of the requested logger.
  /// </summary>
  public interface ILoggerFactory
  {
    /// <summary>
    /// Obtains an <see cref="ILogger"/> instance that is identified by
    /// the given name.
    /// </summary>
    /// <param name="name">The logger name. Submit <see cref="LoggerService.DefaultLoggerName"/>
    /// in order to request a default (unnamed) logger.</param>
    /// <returns>An <see cref="ILogger"/> instance.</returns>
    //TODO should we return the default logger if name is null, or throw an exception?
    ILogger GetLogger(string name);
  }
}
