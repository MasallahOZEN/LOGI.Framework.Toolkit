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


using System.Text;

namespace Slf.Formatters
{
  /// <summary>
  /// Simple formatter class, which represents the contents
  /// of a given <see cref="LogItem"/> as single-line string.
  /// </summary>
  public class SingleLineFormatter : ILogItemFormatter
  {
    #region singleton

    /// <summary>
    /// Singleton instance.
    /// </summary>
    private static readonly SingleLineFormatter instance = new SingleLineFormatter();

    /// <summary>
    /// Provides access to the singleton instance of
    /// the class.
    /// </summary>
    public static SingleLineFormatter Instance
    {
      get { return instance; }
    }

    /// <summary>
    /// Private constructor. A reference to the Singleton
    /// instance of this class is available through the
    /// static <see cref="Instance"/> property.
    /// </summary>
    private SingleLineFormatter()
    {
    }

    #endregion


    /// <summary>
    /// Creates a string representation of a given <see cref="LogItem"/>.
    /// </summary>
    /// <param name="item">The item to be processed.</param>
    /// <returns>A string representation of the submitted
    /// <paramref name="item"/>.</returns>
    public string FormatItem(LogItem item)
    {
      StringBuilder message = new StringBuilder();

      if (!string.IsNullOrEmpty(item.LoggerName))
      {
        message.Append(item.LoggerName + ": ");
      }

      // if the ILogger.Log() methods has been used, EventId and Title 
      // may have been set - if so, format these into a single string message.
      if (item.EventId != null)
      {
        message.Append(string.Format("[{0}] ", item.EventId.Value));
      }
      if (!string.IsNullOrEmpty(item.Title))
      {
        message.Append(item.Title)
            .Append(" - ");
      }

      message.Append(item.Message);
      return message.ToString();
    }
  }
}
