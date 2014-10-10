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
  /// Represents a logger declaration in an application configuration file.
  /// </summary>
  public class LoggerConfigurationElement : ConfigurationElement
  {
    private const string NameAttribute = "name";

    private const string FactoryNameAttribute = "factory";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationElement"/> class. 
    /// </summary>
    public LoggerConfigurationElement()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationElement"/> class. 
    /// </summary>
    public LoggerConfigurationElement(string loggerName, string factoryName)
    {
      LoggerName = loggerName;
      FactoryName = factoryName;
    }


    /// <summary>
    /// The name of this logger.
    /// </summary>
    [ConfigurationProperty(NameAttribute, IsRequired = false, DefaultValue = "", IsKey = true)]
    public string LoggerName
    {
      get
      {
        return (string)this[NameAttribute];
      }
      set
      {
        this[NameAttribute] = value;
      }
    }

    /// <summary>
    /// Refers to the factory to be used.
    /// </summary>
    [ConfigurationProperty(FactoryNameAttribute, IsRequired = false, DefaultValue = "", IsKey = false)]
    public string FactoryName
    {
      get
      {
        return (string)this[FactoryNameAttribute];
      }
      set
      {
        this[FactoryNameAttribute] = value;
      }
    }
  }
}
