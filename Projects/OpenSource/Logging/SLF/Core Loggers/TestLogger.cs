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


using System.Collections.Generic;

namespace Slf
{
  /// <summary>
  /// A <see cref="ILogger"/> implementation which is intended
  /// for the purposes of unit testing the messages
  /// logged by the unit under test. The <see cref="LoggedItems"/>
  /// property of this class contains all messages logged
  /// to this logger.
  /// </summary>
  public class TestLogger : LoggerBase
  {
    /// <summary>
    /// All the LogItems which have been sent to this
    /// test logger.
    /// </summary>
    private List<LogItem> logItems = new List<LogItem>();

    /// <summary>
    /// Constructs a TestLogger instance.
    /// </summary>
    public TestLogger()
      : base()
    {
    }


    /// <summary>
    /// Constructs a named TestLogger instance.
    /// </summary>
    public TestLogger(string name) : base(name)
    {
    }

    /// <summary>
    /// Returns an independent list of all messages logged to this
    /// logger.
    /// </summary>
    public List<LogItem> LoggedItems
    {
      get
      {
        return new List<LogItem>(logItems);
      }
    }

    /// <summary>
    /// Records the given log item.
    /// </summary>
    /// <param name="item">The item being logged.</param>
    public override void Log(LogItem item)
    {
      logItems.Add(item);
    }

    /// <summary>
    /// Clears all the items logged to this test 
    /// logger.
    /// </summary>
    public void Reset()
    {
      logItems.Clear();
    }
        
  }
}
