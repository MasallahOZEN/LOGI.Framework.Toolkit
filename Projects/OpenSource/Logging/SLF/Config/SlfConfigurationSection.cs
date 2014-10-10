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
  public class SlfConfigurationSection : ConfigurationSection
  {
    private const string FactoryCollectionName = "factories";
    private const string LoggerCollectionName = "loggers";

    private const string FactoryElementName = "factory";
    private const string LoggerElementName = "logger";

    /// <summary>
    /// The factories specified within the application configuration.
    /// </summary>
    [ConfigurationProperty(FactoryCollectionName, IsDefaultCollection = true)]
    [ConfigurationCollection(typeof(FactoryConfigurationCollection), AddItemName = FactoryElementName)]
    public FactoryConfigurationCollection Factories
    {
      get { return (FactoryConfigurationCollection) this[FactoryCollectionName]; }
      set { this[FactoryCollectionName] = value; }
    }


    [ConfigurationProperty(LoggerCollectionName, IsDefaultCollection = true)]
    [ConfigurationCollection(typeof(LoggerConfigurationCollection), AddItemName = LoggerElementName)]
    public LoggerConfigurationCollection Loggers
    {
      get { return (LoggerConfigurationCollection) this[LoggerCollectionName]; }
      set { this[LoggerCollectionName] = value; }
    }
  }
}