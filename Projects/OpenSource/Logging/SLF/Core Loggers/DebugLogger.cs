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
using System.Diagnostics;

namespace Slf
{
  /// <summary>
  /// A very simple implementation of <see cref="ILogger"/>
  /// that outputs all messages through <see cref="Debug.WriteLine(string)"/>.
  /// </summary>
  public class DebugLogger : FormattableLoggerBase
  {
    /// <summary>
    /// Creates a named logger.
    /// </summary>
    /// <param name="name">The logger name.</param>
    public DebugLogger(string name)
      : base(name)
    {
    }

    /// <summary>
    /// Creates an un-named logger.
    /// </summary>
    public DebugLogger()
      : base()
    {
    }

    /// <summary>
    /// Logs a given item to the debugger.
    /// </summary>
    /// <param name="item">The item to be logged.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="item"/>
    /// is a null reference.</exception>
    public override void Log(LogItem item)
    {
      Ensure.ArgumentNotNull(item, "item");
      System.Diagnostics.Debug.WriteLine(FormatItem(item));
    }
  }
}