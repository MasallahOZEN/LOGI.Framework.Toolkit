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
  ///<summary>
  /// Defines available log levels (or logging "categories").
  /// Log levels can be used to organize and filter your
  /// logging output.
  ///</summary>
  public enum LogLevel
  {
    /// <summary>
    /// The logging level is undefined. This is regarded
    /// an invalid value.
    /// </summary>
    Undefined = 0,
    /// <summary>
    /// Logs debugging output.
    /// </summary>
    Debug = 1,
    /// <summary>
    /// Logs basic information.
    /// </summary>
    Info =2,
    /// <summary>
    /// Logs a warning.
    /// </summary>
    Warn = 3,
    /// <summary>
    /// Logs an error.
    /// </summary>
    Error = 4,
    /// <summary>
    /// Logs a fatal incident.
    /// </summary>
    Fatal = 5
  }
}
