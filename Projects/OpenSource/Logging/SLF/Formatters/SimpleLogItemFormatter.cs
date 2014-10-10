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
using System.Text;

namespace Slf.Formatters
{
  /// <summary>
  /// Generates a simple, user-readable string-representation of a given
  /// <see cref="LogItem"/>.
  /// </summary>
  public class SimpleLogItemFormatter : ILogItemFormatter
  {
    #region singleton

    /// <summary>
    /// Singleton instance.
    /// </summary>
    private static readonly SimpleLogItemFormatter instance = new SimpleLogItemFormatter();

    /// <summary>
    /// Provides access to the singleton instance of
    /// the class.
    /// </summary>
    public static SimpleLogItemFormatter Instance
    {
      get { return instance; }
    }

    /// <summary>
    /// Private constructor. A reference to the Singleton
    /// instance of this class is available through the
    /// static <see cref="Instance"/> property.
    /// </summary>
    private SimpleLogItemFormatter()
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
      StringBuilder builder = new StringBuilder();

      //init entry
      builder.AppendLine();

      int titleLength = Math.Max((item.Title ?? "").Length, 20);
      builder.AppendLine("".PadRight(titleLength, '*'));


      if (!String.IsNullOrEmpty(item.LoggerName))
      {
        builder.Append(item.LoggerName + ": ");
      }
      if (!String.IsNullOrEmpty(item.Title))
      {
        builder.AppendLine(item.Title);
      }

      //write timestamp
      builder.AppendLine(item.Timestamp.ToString("G"));

      //write log level
      builder.AppendLine(String.Format("Log Level: {0}\n", item.LogLevel));

      //write IDs
      if (item.EventId.HasValue) builder.AppendLine(String.Format("Event ID = {0}", item.EventId));
    
      //write message
      builder.AppendLine(item.Message);

      //write error message, if any
      if (item.Exception != null)
      {
        builder.AppendLine("\n--\n");
        builder.AppendLine(item.Exception.ToString());
      }

      //close entry
      builder.AppendLine("".PadRight(titleLength, '*'));
      builder.AppendLine("");

      return builder.ToString();
    }
  }
}
