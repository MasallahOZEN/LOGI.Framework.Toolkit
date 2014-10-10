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


namespace Slf.Formatters
{
  /// <summary>
  /// A delegate used to forward formatting requests
  /// for a given log item.
  /// </summary>
  /// <param name="item">The item to be formatted.</param>
  /// <returns>A string representation of the item.</returns>
  public delegate string FormatRequestHandler(LogItem item);

  /// <summary>
  /// A simple formatter that forwards formatting requests to
  /// its <see cref="FormatHandler"/> delegate.
  /// </summary>
  public class DelegateLogItemFormatter : ILogItemFormatter
  {
    private FormatRequestHandler formatHandler;

    /// <summary>
    /// The delegate which is being invoked in
    /// order to perform the actual formatting for <see cref="LogItem"/>
    /// instances.
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/>
    /// is a null reference.</exception>
    public FormatRequestHandler FormatHandler
    {
      get { return formatHandler; }
      set
      {
        Ensure.ArgumentNotNull(value, "value");
        formatHandler = value;
      }
    }


    /// <summary>
    /// Creates the formatter with the delegate that is used to forward
    /// formatting requests for <see cref="LogItem"/> instances.
    /// </summary>
    /// <param name="formatHandler">The delegate which is being invoked in
    /// order to perform the actual formatting.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="formatHandler"/>
    /// is a null reference.</exception>
    public DelegateLogItemFormatter(FormatRequestHandler formatHandler)
    {
      FormatHandler = formatHandler;
    }

    /// <summary>
    /// Creates a string representation of a given <see cref="LogItem"/>.
    /// </summary>
    /// <param name="item">The item to be processed.</param>
    /// <returns>A string representation of the submitted
    /// <paramref name="item"/>.</returns>
    public string FormatItem(LogItem item)
    {
      return FormatHandler(item);
    }
  }
}
