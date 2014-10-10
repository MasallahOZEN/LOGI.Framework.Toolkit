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


using System.Configuration;

namespace Slf.Config
{
  /// <summary>
  /// Represents a collection of <see cref="LoggerConfigurationElement"/>
  /// instances in an application configuration file.
  /// </summary>
  public class LoggerConfigurationCollection : ConfigurationElementCollection
  {
    protected ILogger DiagnosticLogger
    {
      get { return LoggerService.GetDiagnosticLogger(GetType().FullName); }
    }

    /// <summary>
    /// Gets a <see cref="LoggerConfigurationElement"/> at a given index.
    /// </summary>
    public LoggerConfigurationElement this[int index]
    {
      get { return (LoggerConfigurationElement)BaseGet(index); }
    }

    /// <summary>
    /// Adds the given element to the collection.
    /// </summary>
    /// <param name="element"></param>
    protected override void BaseAdd(ConfigurationElement element)
    {
      try
      {
        // check whether a factory config of this name was already added.
        bool add = true;
        LoggerConfigurationElement loggerConfigElement = element as LoggerConfigurationElement;
        foreach (LoggerConfigurationElement existingElement in this)
        {
          if (existingElement.LoggerName.ToLowerInvariant() == loggerConfigElement.LoggerName.ToLowerInvariant())
          {
            DiagnosticLogger.Warn("An error occured while loading the Logger Configuration elements. Please ensure that all loggers have unique names.");
            add = false;
          }
        }

        if (add)
        {
          base.BaseAdd(element);
        }
      }
      catch (ConfigurationErrorsException e)
      {
        DiagnosticLogger.Warn(e, "An error occured while loading the Logger Configuration elements. Please ensure that all loggers have unique names.");
      }
    }


    /// <summary>
    /// Creates a new  <see cref="LoggerConfigurationElement"/> instance.
    /// </summary>
    protected override ConfigurationElement CreateNewElement()
    {
      return new LoggerConfigurationElement();
    }

    /// <summary>
    /// Gets the value of the property of the given <see cref="LoggerConfigurationElement"/>
    /// instance that has been designated as the key.
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((LoggerConfigurationElement) element).LoggerName;
    }
  }
}